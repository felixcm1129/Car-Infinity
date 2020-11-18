using Godot;
using System;
using System.Collections.Generic;

public class Player : KinematicBody2D
{
	[Signal]
	public delegate void HealthChanged();

	[Signal]
	public delegate void Died();

	[Signal]
	public delegate void ScoreChanged();

	[Export]
	public int speed = 300;
	public int maxspeed = 1000;
	public int tempBeforeOtherCollision = 0;
	public int timeBeforeUpdateScore = 50;
	public int score = 0;

	private States states;

	public Vector2 Velocity = Vector2.Zero;

	public override void _Ready()
	{
		states = States.Alive;
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
		if (timeBeforeUpdateScore > 0)
		{
			timeBeforeUpdateScore--;
		}
		else UpdateScore();

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

		//if(speed < maxspeed) speed++;
	}

	public void Collision()
	{
		_health--;
		if (_health == 0)
			states = States.Dead;

		EmitSignal("HealthChanged", _health);
		tempBeforeOtherCollision = 50;
	}

	public void UpdateScore()
	{
		score++;
		EmitSignal("ScoreChanged", score);
		timeBeforeUpdateScore = 50;
	}

}
