using Godot;
using System;

public partial class NewSpringArm3d : SpringArm3D
{
	
	[Export] public float mouse_sensitivity = 0.002f;

	
	
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
	
		if (@event is InputEventMouseMotion mouseMotionEvent)
		{
			Vector3 currentRotation = Rotation;
			currentRotation.Y -= mouseMotionEvent.Relative.X * mouse_sensitivity;
			currentRotation.X -= mouseMotionEvent.Relative.Y * mouse_sensitivity;

			// 3. Clamp the X rotation (pitch) so the camera doesn't flip upside down
			currentRotation.X = Mathf.Clamp(currentRotation.X, Mathf.DegToRad(-89), Mathf.DegToRad(89));

			Rotation = currentRotation;
		}
	}

	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("escape"))
		{
			Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured 
				? Input.MouseModeEnum.Visible 
				: Input.MouseModeEnum.Captured;
		}
	}
}
