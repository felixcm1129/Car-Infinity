using Godot;
using System;

public class GUI : MarginContainer
{
	private Label lifes;
	private Label score;

	public override void _Ready()
	{
		lifes = (Label)GetNode("Container/Stats/Life/Count/Number");
		score = (Label)GetNode("Container/Stats/Score/Count/Number");
		var player = (Player)GetNode("/root/World/Player");
		lifes.Text = player.MaxHealth.ToString();

		UpdateHealth(3);
	}
	
	private void OnPlayerHealthChanged(int playerHealth)
	{
		UpdateHealth(playerHealth);
	}
	public void UpdateHealth(int health)
	{
		lifes.Text = health.ToString();
	}

	private void OnPlayerScoreChanged(int playerScore)
	{
		UpdateScore(playerScore);
	}

	public void UpdateScore(int number)
	{
		score.Text = number.ToString();
	}

}
