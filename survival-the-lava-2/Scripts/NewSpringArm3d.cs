using Godot;
using System;

public partial class NewSpringArm3d : SpringArm3D
{
	
	[Export] public float mouse_sensitivity = 0.002f;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		// 1. Cast the event to InputEventMouseMotion using pattern matching
		if (@event is InputEventMouseMotion mouseMotionEvent)
		{
			// 2. Use Vector3 properties instead of direct .x/.y modifying on the Rotation variable
			Vector3 currentRotation = Rotation;

			// Godot 4 uses radians for rotation. Use Mathf.DegToRad if your sensitivity is in degrees.
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
