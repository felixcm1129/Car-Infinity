using Godot;
using System;

public class Player : KinematicBody2D
{
	[Export]
	public int speed = 200;

	public Vector2 Velocity = Vector2.Zero;
	
	public override void _Ready()
	{
		
	}
	
	public Vector2 GetInput()
	{
		var input_vector = Vector2.Zero;
		input_vector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		input_vector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		
		return input_vector;
	}
	
	public override void _PhysicsProcess(float delta)
	{
		var input_vector = GetInput();
		if(input_vector != Vector2.Zero)
		{
			Velocity = input_vector;
		}
		else
		{
			Velocity = Vector2.Zero;
		}
		MoveAndCollide(Velocity);
	}

}
