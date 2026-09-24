using Godot;
using System;

public partial class DeathCamera3d : Camera3D
{
	[Export] public Node3D TargetPlayer;


	private float _currentAngle = 0.0f;
	private bool _isActive = false;

	public override void _Ready()
	{
		// Start deactivated so the main player camera is used first
		Current = false; 
	}


	public void OnAreaTriggered(Node3D body)
	{
		// Example: Only activate if a Player enters the area
		if (body.Name == "Player")
		{
			
			Current = true;
		}
	}




}
