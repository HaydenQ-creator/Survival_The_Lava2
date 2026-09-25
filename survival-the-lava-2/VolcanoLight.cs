using Godot;
using System;

public partial class VolcanoLight : DirectionalLight3D
{
	public static bool VolcanoActive { get; set; } = false;
	
	private Tween _tween;

	public override void _Ready()
	{
		
	}

	public void FadeLightEnergy(float targetEnergy, float duration)
	{
		if (_tween != null && _tween.IsValid())
		{
			_tween.Kill(); 
		}

		// 2. Create a new tween instance
		_tween = CreateTween();

		_tween.TweenProperty(this, nameof(LightEnergy), targetEnergy, duration)
			  .SetTrans(Tween.TransitionType.Cubic) // Smooth acceleration/deceleration
			  .SetEase(Tween.EaseType.InOut);       // Apply transition to both ends
	}
	
	private void VolcanoLightOn()
	{
		FadeLightEnergy(16.0f, 3.0f);
	}
	
	private void VolcanoLightOff()
	{
		FadeLightEnergy(0.0f, 3.0f);
	}
	
	public override void _Process(double delta)
	{
		if (VolcanoActive == false)
		{
			VolcanoLightOff();
		}
		else if (VolcanoActive == true)
		{
			VolcanoLightOn();
		}
		
		if (Input.IsActionJustPressed("TestButton"))
		{
			VolcanoActive = !VolcanoActive;
		}
	}
}
