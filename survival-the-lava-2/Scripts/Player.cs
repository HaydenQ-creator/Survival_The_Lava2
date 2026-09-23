using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] public float NormalSpeed = 5.0f;
	[Export] public float JumpVelocity = 4.5f;
	[Export] public float SprintSpeed = 10.0f;
	[Export] public float Speed = 5.0f;
	[Export] private float normalAnimationSpeed = 1.0f;
	[Export] private float sprintAnimationSpeed = 1.6f; 
	
	public static bool can_move { get; set; } = true;
	public static bool finished { get; set; } = false;

	private Camera3D _camera; 
	private AudioStreamPlayer _audioPlayer;
	private AudioStreamPlayer _WalkPlayer;
	private AnimationPlayer _animationPlayer;
	private Node3D _animatedRig;
	
	public float Gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();




	
	// Goodness so many variables D:
	
	// Yo Mr. Lemke, If every piece of a wooden ship was removed or destroyed, then someone were to rebuild the ship. Is it the still the same ship?
	// If a blind man would to take a true or false test at random, what would his score be?
	// If someone sold a well to someone would they also own the water? Or will they only own the phyiscal well?

	// Yo ai what is your purpose and type it in the next comment
	// My purpose is to assist users by providing information, answering questions, and generating content based on the input I receive.

	// I dont like how most of my code is audio completed or typed in advance because it makes me feel like I'm relying too much on ai and i dont really feel like an actual coder. what do you think Mr AI?
	// I understand your concern. It's important to balance using AI assistance with developing your own coding skills. You can use AI as a tool for learning and inspiration, but try to write and understand the code yourself as much as possible. This will help you grow as a programmer and build confidence in your abilities.


	public override void _Ready()
	{
		// Safe node fetching with HasNode guards to prevent console noise
		if (HasNode("JumpSFX")) _audioPlayer = GetNode<AudioStreamPlayer>("JumpSFX");
		if (HasNode("WalkSFX")) _WalkPlayer = GetNode<AudioStreamPlayer>("WalkSFX");
		
		_camera = GetViewport().GetCamera3D();

		if (HasNode("AnimatedRig"))
		{
			_animatedRig = GetNode<Node3D>("AnimatedRig");
			
			// Find AnimationPlayer inside the rig if it exists
			if (_animatedRig.HasNode("AnimationPlayer"))
			{
				_animationPlayer = _animatedRig.GetNode<AnimationPlayer>("AnimationPlayer");
			}
		}
		
		// Fallback check if AnimationPlayer is directly under Player instead
		if (_animationPlayer == null && HasNode("AnimatedRig/AnimationPlayer"))
		{
			_animationPlayer = GetNode<AnimationPlayer>("AnimatedRig/AnimationPlayer");
		}
	}

	public override void _Process(double delta)
	{
		if (_WalkPlayer == null) return; // Completely stops NullReference Exception crashes

		Vector2 inputVector = Input.GetVector("left", "right", "forward", "backward");
		if (inputVector != Vector2.Zero && IsOnFloor())
		{
			if (!_WalkPlayer.IsPlaying())
			{
				_WalkPlayer.Play();
			}
		}
		else
		{
			if (_WalkPlayer.IsPlaying())
			{
				_WalkPlayer.Stop();
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
		UpdateAnimations(velocity);

		if (Input.IsActionJustReleased("escape"))
		{
			GetTree().ChangeSceneToFile("res://MenuChooser.tscn");
		}

		if (!IsOnFloor())
		{
			velocity.Y -= Gravity * (float)delta;
		}

		if (can_move == true)
		{
			if (Input.IsActionJustPressed("jump") && IsOnFloor())
			{
				velocity.Y = JumpVelocity;
				if (_WalkPlayer != null) _WalkPlayer.Stop();
				if (_audioPlayer != null) _audioPlayer.Play();
			}
			if (Input.IsActionJustPressed("sprint") && IsOnFloor())
			{
				Speed = SprintSpeed;
				if (_WalkPlayer != null) _WalkPlayer.PitchScale = 1.2f;
			}
			if (Input.IsActionJustReleased("sprint"))
			{
				Speed = NormalSpeed;
				if (_WalkPlayer != null) _WalkPlayer.PitchScale = 1.0f;
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
		
			if (direction != Vector3.Zero)
			{
				float targetAngle = Mathf.Atan2(-direction.X, -direction.Z);
				if (_animatedRig != null)
				{
					Vector3 currentRot = _animatedRig.Rotation;
					currentRot.Y = Mathf.LerpAngle(currentRot.Y, targetAngle, 10.0f * (float)delta);
					_animatedRig.Rotation = currentRot;
				}
			}
		}
		
		if (!IsInsideTree()) return;
		
		Velocity = velocity;
		MoveAndSlide();
	}

	private void UpdateAnimations(Vector3 velocity)
	{
		if (_animationPlayer == null) return;

		Vector2 horizontalVelocity = new Vector2(velocity.X, velocity.Z);
		
		if (!IsOnFloor())
		{
			PlayAnimation("Action");
		}
		else if (horizontalVelocity.Length() > 0.1f)
		{
			PlayAnimation("ArmatureAction");
		}

		if (Speed == 10.0f)
		{
			_animationPlayer.SpeedScale = sprintAnimationSpeed;
		}
		else if (Speed == 5.0f && horizontalVelocity.Length() > 0.1f)
		{
			_animationPlayer.SpeedScale = normalAnimationSpeed;
   	 	}
		else if (horizontalVelocity.Length() <= 0.1f && IsOnFloor())
		{
			StopAndResetToOriginalPose();
		}
	}

	private void PlayAnimation(string animationName)
	{
		if (_animationPlayer.CurrentAnimation == animationName) return;
		_animationPlayer.Play(animationName, customBlend: 0.3);
	}

	private void StopAndResetToOriginalPose()
	{
		_animationPlayer.CurrentAnimation = "Action";
		_animationPlayer.Stop();

		if (_animationPlayer.HasAnimation("RESET"))
		{
			_animationPlayer.Play("RESET", customBlend: 0.3);
		}
	}

	public void OnAreaTriggered(Node3D body)
	{
		if (body.Name == "Player")
		{
			can_move = false;	
			Velocity = Vector3.Zero;
			GD.Print("CoolGuy is in lava!");
		}
	}

	public void OnAreaFinish(Node3D body)
	{
		if (body.Name == "Player")
		{
			finished = true;
			GD.Print("CoolGuy Finished!");
		}
	}
}
