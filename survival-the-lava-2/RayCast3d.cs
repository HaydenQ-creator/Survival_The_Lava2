using Godot;
using System;

public partial class RayCast3d : RayCast3D
{
	// Signal to notify other nodes (like an Enemy parent) when the player is spotted
	[Signal] public delegate void PlayerDetectedEventHandler(Player player);
	[Signal] public delegate void PlayerLostEventHandler();

	private bool _wasPlayerDetected = false;

	public override void _PhysicsProcess(double delta)
	{
		// 'IsColliding()' and 'GetCollider()' are called directly because this script IS the RayCast3D
		if (IsColliding())
		{
			GodotObject collider = GetCollider();

			if (collider is Player player)
			{
				if (!_wasPlayerDetected)
				{
					_wasPlayerDetected = true;
					EmitSignal(SignalName.PlayerDetected, player);
					GD.Print("Player entered line of sight!");
				}
				
				// Put continuous detection logic here if needed
				return; 
			}
		}

		// If the ray is no longer colliding or no longer hitting the player
		if (_wasPlayerDetected)
		{
			_wasPlayerDetected = false;
			Player.invul = false;
			EmitSignal(SignalName.PlayerLost);
			GD.Print("Player lost from line of sight.");
		}
	}
}
