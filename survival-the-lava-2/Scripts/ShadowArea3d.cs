using Godot;
using System;

public partial class ShadowArea3d : Area3D
{
	public override void _Ready()
	{
		// Connect the signals using the correct Godot 4 Node3D signature
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}
	
	private void OnBodyEntered(Node3D body)
	{
		// Cast the incoming body to your 'Player' class instance
		if (body is Player playerInstance)
		{
			// Modify the property on the specific player instance that entered
			Player.invul = true;
			GD.Print("Player entered shadow: Invulnerable = true");
		}
	}

	private void OnBodyExited(Node3D body)
	{
		if (body is Player playerInstance)
		{
			// Modify the property on the specific player instance that left
			Player.invul = false;
			GD.Print("Player left shadow: Invulnerable = false");
		}
	}
}
