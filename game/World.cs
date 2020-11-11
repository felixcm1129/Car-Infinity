using Godot;
using System;

public class World : Node2D
{
	[Export]
	public Godot.TileMap borderMap, roadMap, coneMap;
	[Export]
	public int BorderTile, RoadTile,  ConeTile;

	[Export]
	public RigidBody2D cone;

	[Export(PropertyHint.Range, "0,100,5")]
	int conesChance;

	int width, height, oldheight, left_right, GameStart = 2;
	Random rnd;

	int[,] terrainMap;

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
		width = 14;
		height = 3;
		int x_cone;
		int y_cone;

		if (terrainMap == null)
		{
			terrainMap = new int[width, height];
		}

		if(GameStart <= 0)
		{
			left_right = Left_Right();
		}

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (x < 4 || x > 9)
				{
					borderMap.SetCell(x + left_right, y + oldheight, BorderTile);
				}
				else
				{
					roadMap.SetCell(x + left_right, y + oldheight, RoadTile);
					if (placeCones()) coneMap.SetCell(x + left_right, y + oldheight, ConeTile);
				} 
			}
		}
		

	}

	private int Left_Right()
	{
		int deplacement = rnd.Next(1, 100);
		if (deplacement < 33) return -1;
		else if (deplacement > 33 && deplacement < 66) return 0;
		else return 1;
	}

	private bool placeCones()
	{
		int place = rnd.Next(1, 35);
		if (place == 1) return true;
		else return false;
	}
}
