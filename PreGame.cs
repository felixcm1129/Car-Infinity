using Godot;
using System;

public class PreGame : Label
{
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("Space"))
		{
			GetTree().ChangeScene("res://game/Play.tscn");
		}
	}
}
