using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] public float NormalSpeed = 5.0f;
	[Export] public float JumpVelocity = 4.5f;
	[Export] public float SprintSpeed = 10.0f;
	[Export] public float Speed = 5.0f;
	[Export] private float _normalAnimSpeed = 1.0f;
	[Export] private float _sprintAnimSpeed = 1.6f; // Adjust this to match your sprint feel

	private Camera3D _camera; 
	private AudioStreamPlayer _audioPlayer;
	private AudioStreamPlayer _WalkPlayer;
	private AnimationPlayer _animationPlayer;
	public float Gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		_audioPlayer = GetNode<AudioStreamPlayer>("JumpSFX");
		_WalkPlayer = GetNode<AudioStreamPlayer>("WalkSFX");
		
		_camera = GetViewport().GetCamera3D();
		if (_animationPlayer == null)
		{
			_animationPlayer = GetNode<AnimationPlayer>("AnimatedRig/AnimationPlayer");
		}
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("left") && IsOnFloor())
		{
			if (!_WalkPlayer.IsPlaying())
			{
				_WalkPlayer.Play();
			}
		}
		if (Input.IsActionJustReleased("left"))
		{
			_WalkPlayer.Stop();
		}
	}


	
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
		UpdateAnimations(velocity);




		if (!IsOnFloor())
		{
			velocity.Y -= Gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			_WalkPlayer.Stop();
			_audioPlayer.Play();
		}
		if (Input.IsActionJustPressed("sprint") && IsOnFloor())
		{
			Speed = SprintSpeed;
		}
		if (Input.IsActionJustReleased("sprint"))
		{
			Speed = NormalSpeed;
		}
		
		Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		
		Vector3 rawDirection = new Vector3(inputDir.X, 0, inputDir.Y);
		Vector3 direction = Vector3.Zero;

		if (_camera != null && rawDirection != Vector3.Zero)
		{
			direction = rawDirection.Rotated(Vector3.Up, _camera.GlobalRotation.Y).Normalized();
		}
		else if (rawDirection != Vector3.Zero)
		{
			direction = rawDirection.Normalized();
		}

		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		// ADDED: Mesh turning logic wrapped inside a comment block as requested.
		// To activate this, remove the /* and */ symbols and ensure "Visuals" matches your mesh node name.
	
		if (direction != Vector3.Zero)
		{
			// 1. Calculate the target angle based on the movement direction
			float targetAngle = Mathf.Atan2(-direction.X, -direction.Z);

			// 2. Smoothly rotate the visual node on the Y axis toward the target angle
			// (Change "Visuals" to match the exact name of your 3D mesh node)
			Node3D visualMesh = GetNode<Node3D>("AnimatedRig");
			if (visualMesh != null)
			{
				Vector3 currentRot = visualMesh.Rotation;
				currentRot.Y = Mathf.LerpAngle(currentRot.Y, targetAngle, 10.0f * (float)delta);
				visualMesh.Rotation = currentRot;
			}
		}
		

		Velocity = velocity;
		MoveAndSlide();
	}

	private void UpdateAnimations(Vector3 velocity)
	{

		Vector2 horizontalVelocity = new Vector2(velocity.X, velocity.Z);
		
		if (!IsOnFloor())
		{
			PlayAnimation("Action");
		}
		else if (horizontalVelocity.Length() > 0.1f)
		{
			// Play running/walking animation if moving on the ground
			PlayAnimation("ArmatureAction");
		}


		if (Speed == 10.0f)
		{
			// Play sprinting animation if moving and sprinting
			_animationPlayer.SpeedScale = _sprintAnimSpeed;
			
		}
		else if (Speed == 5.0f && horizontalVelocity.Length() > 0.1f)
		{
		// Reset back to normal speed for walking/jogging
			_animationPlayer.SpeedScale = _normalAnimSpeed;
			
   	 	}


		else if (horizontalVelocity.Length() <= 0.1f && IsOnFloor())
		{
			StopAndResetToOriginalPose();
		}
	}
	private void PlayAnimation(string animationName)
	{
		// Avoid restarting the animation if it's already playing
		if (_animationPlayer.CurrentAnimation == animationName)
		{
			return;
		}

		// Play the animation. The second argument (customBlend) 
		// smoothly blends transitions between animations (e.g., 0.3 seconds)
		_animationPlayer.Play(animationName, customBlend: 0.3);
	}

	private void StopAndResetToOriginalPose()
	{
	// 1. Stop the active animation player tracking
	_animationPlayer.CurrentAnimation = "Action";
	_animationPlayer.Stop();

	// 2. Play the built-in Godot RESET animation to restore the original bone transforms
	if (_animationPlayer.HasAnimation("RESET"))
	{
		_animationPlayer.Play("RESET", customBlend: 0.3);
	}
	
	}
}
