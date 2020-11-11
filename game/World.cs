using Godot;
using System;

public class World : Node2D
{
	[Export]
	public Godot.TileMap borderMap, roadMap;
	[Export]
	public int BorderTile, RoadTile;

	[Export]
	public RigidBody2D cone;

	[Export(PropertyHint.Range, "0,100,5")]
	int conesChance;

	int width, height, oldheight;
	Random rnd;

	int numberOfCreation;

	int[,] terrainMap;

	public override void _Ready()
	{
		numberOfCreation = 10;
		oldheight = 0;
		rnd = new Random();
		borderMap = GetChild(0) as TileMap;
		roadMap = GetChild(1) as TileMap;
	}

	public override void _Process(float delta)
	{
		doMap();
		oldheight -= 5;
	}

	public void doMap()
	{
		width = 14;
		height = 6;

		if (terrainMap == null)
		{
			terrainMap = new int[width, height];
		}

		for (int i = 0; i < numberOfCreation; i++)
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					if (x < 5 || x > 8)
					{
						borderMap.SetCell(x + 0, y + oldheight, BorderTile);
					}
					else roadMap.SetCell(x + 0, y + oldheight, RoadTile);
				}
			}
		}
		

	}

	private bool placeCones()
	{
		bool place;
		place = rnd.Next(1, 101) < conesChance ? true : false;
		return place;
	}
}
