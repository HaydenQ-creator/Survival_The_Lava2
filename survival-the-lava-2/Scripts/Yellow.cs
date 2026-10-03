using Godot;
using System;

public partial class Yellow : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public const float ViewAngle = 190.0f;
	
	public enum EnemyState
	{
		Search,
		Run,
		Leave,
		None
	}
	public EnemyState CurrentState {get; set; } = EnemyState.None;
	

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
		
		
	}
}
