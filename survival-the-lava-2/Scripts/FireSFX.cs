using Godot;
using System;

public partial class FireSFX : AudioStreamPlayer3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}


	public void OnAreaTriggered(Node3D body)
	{
		// Example: Only activate if a Player enters the area
		if (body.Name == "Player")
		{
			Play();
			if(!Playing)
			{
				Play();
			}
			
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
