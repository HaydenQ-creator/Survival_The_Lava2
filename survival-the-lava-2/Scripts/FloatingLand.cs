using Godot;
using System;

public partial class FloatingLand : StaticBody3D
{
	// The boolean that the Area3D will trigger
	public bool is_touched { get; set; } = false;

	[Export]
	public float DescentSpeed { get; set; } = 0.3f;

	public void OnAreaTriggered(Node3D body)
	{
		// Example: Only activate if a Player enters the area
		if (body.Name == "Player")
		{
			is_touched = true;
			GD.Print("Boolean activated via signal!");
		}
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
