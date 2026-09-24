using Godot;
using System;

public partial class FloatingLand : AnimatableBody3D
{
	// The boolean that the Area3D will trigger
	public bool is_touched { get; set; } = false;

	[Export]
	public float DescentSpeed { get; set; } = 0.7f;
	
	

	public void OnAreaTriggered(Node body)
	{
		if (body is Player) 
		{
			is_touched = true;
		}
	}
	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		
		if (is_touched == true)
		{
			Vector3 movement = new Vector3(0, -DescentSpeed * (float)delta, 0);
			GlobalPosition += movement;
		}
	}
}
