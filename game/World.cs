using Godot;
using System;

public class World : Node2D
{
	[Export]
	public Godot.TileMap borderMap, roadMap, coneMap;
	[Export]
	public int BorderTile, RoadTile, ConeTile, Timer;

	[Export]
	public RigidBody2D cone;

	int width, height, oldheight, left_right, GameStart = 2;
	Random rnd = new Random();

	public override void _Ready()
	{
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
		width = 15;
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
					borderMap.SetCell(x + left_right, y + oldheight, BorderTile);
				}
				else
				{
					roadMap.SetCell(x + left_right, y + oldheight, RoadTile);
					if (x > 5 && x < 9)
					{
						if (placeCones() && GameStart <= 0)
						{
							coneMap.SetCell(x + left_right, y + oldheight, ConeTile);
						}

					}

				}
			}
		}
	}

	private int Left_Right()
	{
		int deplacement = rnd.Next(1, 5);
		if (deplacement == 1) return -1;
		else if (deplacement > 1 && deplacement < 5) return 0;
		else return 1;
	}

	private bool placeCones()
	{
		int place = rnd.Next(1, 20);
		if (place == 1) return true;
		else return false;
	}


}
