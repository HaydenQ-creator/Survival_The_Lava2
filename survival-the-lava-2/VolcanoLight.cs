using Godot;
using System;

public partial class VolcanoLight : DirectionalLight3D
{
	public static bool VolcanoActive { get; set; } = false;
	
	private Tween _tween;

	public override void _Ready()
	{
		
	}
	
	public override void _Process(double delta)
	{
		if (VolcanoActive == false)
		{
			LightEnergy = 0.0f;
		}
		else if (VolcanoActive == true)
		{
			LightEnergy = 16.0f;
		}
		
		if (Input.IsActionPressed("TestButton"))
		{
			VolcanoActive = !VolcanoActive;
		}
	}
}
