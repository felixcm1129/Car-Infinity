using Godot;
using System;

public class TitleScreen : MarginContainer
{

	private void onPlaypressed()
	{
		GetTree().ChangeScene("res://PreGame.tscn");
	}
	private void onOptionspressed()
	{
		GetTree().ChangeScene("res://game/Options.tscn");
	}
}



