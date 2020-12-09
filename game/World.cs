using Godot;
using System;

public class World : Node2D
{
	[Export]
	public Godot.TileMap borderMap, roadMap, coneMap, delete;

	[Export]
	public RigidBody2D cone;

	private OpenSimplexNoise Noise = new OpenSimplexNoise();

	int width, height, oldheight, left_right = 0, GameStart = 2;
	Random rnd = new Random();

	private int old_random = 0;
	private int maxConesByRow = 4;
	private int ConesBuildRow = 0;
	private int oldY = 0;

	public override void _Ready()
	{
		Noise.Period = 32;
		oldheight = 0;
		rnd = new Random();
		borderMap = GetChild(0) as TileMap;
		roadMap = GetChild(1) as TileMap;
		coneMap = GetChild(2) as TileMap;
	}

	public override void _Process(float delta)
	{
		doMap();
		oldheight -= 3;
		GameStart--;
	}

	public void doMap()
	{
		width = 20;
		height = 3;

		if (GameStart <= 0)
		{
			left_right = Left_Right();
		}


		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (x < 5 || x > 9)
				{
					if (x < 5)
					{
						borderMap.SetCell((x * -1) + left_right, y + oldheight, 0);
						borderMap.SetCell(x + left_right, y + oldheight, 0);
					}
					else 
					{
						borderMap.SetCell(x + left_right, y + oldheight, 0);
					}
					
				}
				else
				{
					roadMap.SetCell(x + left_right, y + oldheight, 0);
					if (x > 5 && x < 9)
					{
						if (placeConesNoise(x, y) && GameStart <= 0)
						{
							coneMap.SetCell(x + left_right, y + oldheight, 0);
						}
					}
				}
			}
		}
	}

	private int Left_Right()
	{
		int deplacement = rnd.Next(1, 100);
		if (deplacement < 41 + old_random)
		{
			old_random = 10;
			return -1;
		}
		else if (deplacement > 59 + old_random)
		{
			old_random = -10;
			return 1;
		} 
		else return 0;
	}

	private bool placeConesNoise(int x, int y)
	{
		if (oldY != y)
		{
			ConesBuildRow = 0;
			oldY = y;
		}

		if (left_right != 0)
		{
			maxConesByRow = 1;
		}
		else maxConesByRow = 3;

		Noise.Seed = rnd.Next(1, 5);
		float place = Noise.GetNoise2d(x, y);
		if (place < 0 && ConesBuildRow < maxConesByRow) 
		{
			ConesBuildRow++;
			return true;
		}
		
		else return false;
	}

}
