using Godot;
using System;

public class Player : KinematicBody2D
{
	[Export]
	public int speed = 300;

	public Vector2 Velocity = Vector2.Zero;
	
	public override void _Ready()
	{
	}
	
	public Vector2 GetInput()
	{
		var input_vector = Vector2.Zero;
		input_vector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		//input_vector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		input_vector = input_vector.Normalized();
		
		return input_vector;
	}
	
	public override void _PhysicsProcess(float delta)
	{
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
		Velocity = MoveAndSlide(Velocity);
	}

}
