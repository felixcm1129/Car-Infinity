using Godot;
using System;

public class PlayerStats : Node
{
	[Signal]
	public delegate void HealthChanged(int health);
	
	[Signal]
	public delegate void Died();
	
	[Export]
	public int MaxHealth
	{
		get { return _health; }
		set { _health = value; }
	}
	
	public enum States {
		Alive,
		Dead
	};
	
	private States _state = States.Alive;
	
	private int _health = 3;
	
	public void TakeDamage(int count)
	{
		if (_state == States.Dead) {
			return;
		}

		_health -= count;
		if (_health <= 0) {
			_health = 0;
			_state = States.Dead;
			EmitSignal("Died");
		}

		EmitSignal("HealthChanged", _health);
	}

}
