using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public class Player : KinematicBody2D
{
	[Signal]
	public delegate void HealthChanged();

	[Signal]
	public delegate void Died();

	[Signal]
	public delegate void ScoreChanged();

	[Export]
	private int speed = 300;
	private int tempBeforeOtherCollision = 0;
	private int timeBeforeUpdateScore = 50;
	private int timeBeforeShowGameOver = 3000;
	private int score = 0;
	private int highscore;
	private String highscore_file = System.IO.File.ReadAllText("./highscore.txt");
	private String playerscore_file = System.IO.File.ReadAllText("./playerscore.txt");



	private States states;

	private Vector2 Velocity = Vector2.Zero;

	public override void _Ready()
	{
		states = States.Alive;
		highscore = Int32.Parse(highscore_file);
	}

	public enum States
	{
		Alive,
		Dead
	};

	

	private int _health = 3;

	[Export]
	public int MaxHealth
	{
		get { return _health; }
		set { _health = value; }
	}
	
	public Vector2 GetInput()
	{
		var input_vector = Vector2.Zero;
		input_vector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		input_vector = input_vector.Normalized();
		
		return input_vector;
	}
	
	public override void _PhysicsProcess(float delta)
	{
		if(_health > 0)
		{
			if (timeBeforeUpdateScore > 0)
			{
				timeBeforeUpdateScore--;
			}
			else UpdateScore();
		}
		
		if(tempBeforeOtherCollision > 0)
		{
			tempBeforeOtherCollision--;
		}

		var input_vector = GetInput();

		input_vector.y += -1;

		if(input_vector != Vector2.Zero)
		{
			Velocity = input_vector * speed;
		}
		else
		{
			Velocity = Vector2.Zero;
		}
		
		if(GetSlideCount() > 0)
		{
			if (tempBeforeOtherCollision <= 0)
			{
				Collision();
			}
		}


		Velocity = MoveAndSlide(Velocity);
	}

	public void Collision()
	{
		if (_health > 0)
		{
			_health--;
		}
		
		if (_health == 0)
		{
			states = States.Dead;
			speed = 0;
			System.IO.File.WriteAllText("./playerscore.txt", score.ToString());
			if (score > highscore)
			{
				highscore = score;
				System.IO.File.WriteAllText("./highscore.txt", highscore.ToString());
			}
			GetTree().ChangeScene("res://game/GameOver.tscn");
		}
			

		EmitSignal("HealthChanged", _health);
		tempBeforeOtherCollision = 25;
	}

	public void UpdateScore()
	{
		score++;
		EmitSignal("ScoreChanged", score);
		timeBeforeUpdateScore = 50;
	}

	public int getHighScore()
	{
		return highscore;
	}

	public int getScore()
	{
		return score;
	}

}
