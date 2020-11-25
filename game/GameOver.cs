using Godot;
using System;

public class GameOver : MarginContainer
{
	private Label highscore;
	private Label score;

	public override void _Ready()
	{
		String highscore_file = System.IO.File.ReadAllText("./highscore.txt");
		String playerscore_file = System.IO.File.ReadAllText("./playerscore.txt");
		GD.Print(this);
		highscore = (Label)GetNode("Container/Stats/HighScore/Count/Score");
		score = (Label)GetNode("Container/Stats/Score/Count/Score");
		score.Text = playerscore_file;
		highscore.Text = highscore_file;
	}
	
	private void onRestartPressed()
	{
		GetTree().ChangeScene("res://game/Play.tscn");
	}
	
	private void onMainMenuPressed()
	{
	   GetTree().ChangeScene("res://TitleScreen.tscn");
	}

}






