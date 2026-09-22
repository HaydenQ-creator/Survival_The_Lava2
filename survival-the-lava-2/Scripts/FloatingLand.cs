using Godot;
using System;

public partial class FloatingLand : AnimatableBody3D
{
	// The boolean that the Area3D will trigger
	public bool is_touched { get; set; } = false;

	[Export]
	public float DescentSpeed { get; set; } = 0.7f;
	private AudioStreamPlayer3D _FirePlayer;
	
	

	public void OnAreaTriggered(Node body)
	{
		// Example: Only activate if a Player enters the area
		if (body is Player) 
		{
			is_touched = true;
			if (!_FirePlayer.IsPlaying())
			{
				_FirePlayer.Play();
			}
		}
	}
	public override void _Ready()
	{
		_FirePlayer = GetNode<AudioStreamPlayer3D>("FireSFX");
	}

	public override void _PhysicsProcess(double delta)
	{
		// Only move down if the Area3D has detected the player
		if (is_touched)
		{
			Vector3 movement = new Vector3(0, -DescentSpeed * (float)delta, 0);
			GlobalPosition += movement;
		}
	}
}
