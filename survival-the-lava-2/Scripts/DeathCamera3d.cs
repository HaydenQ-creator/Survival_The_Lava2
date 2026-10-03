using Godot;
using System;

public partial class DeathCamera3d : Camera3D
{
	[Export] public CharacterBody3D TargetPlayer;


	private float _currentAngle = 0.0f;
	private bool _isActive = false;

	public override void _Ready()
	{
		Current = false; 
	}


	public void OnAreaTriggered(Node3D body)
	{
		if (Player.can_move == false)
		{
			
			Current = true;
		}
	}




}
