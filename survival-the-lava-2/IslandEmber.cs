using Godot;
using System;

public partial class IslandEmber : GpuParticles3D
{

	[Export] public bool Is_Emitting = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Emitting = false; 
	}


	public void OnAreaTriggered(Node3D body)
	{
		// Example: Only activate if a Player enters the area
		if (body.Name == "Player")
		{
			Is_Emitting = true;
			GD.Print("Boolean activated via signal!");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Is_Emitting == true)
		{
			Emitting = true;
		}
		else
		{
			Emitting = false;
		}
	}
}
