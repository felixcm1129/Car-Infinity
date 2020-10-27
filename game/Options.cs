using Godot;
using System;

public class Options : MarginContainer
{
	public override void _Ready()
	{
		
	}

	private void onBackpressed()
	{
		GetTree().ChangeScene("res://TitleScreen.tscn");
	}
	
	

}



