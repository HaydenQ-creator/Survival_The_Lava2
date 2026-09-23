using Godot;
using System;

public partial class Level1 : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Player.can_move == false)
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
			if (Input.IsActionPressed("Restart"))
			{
				Player.can_move = true;
				Input.MouseMode = Input.MouseModeEnum.Captured;
				GetTree().ReloadCurrentScene();
			}
			else if (Input.IsActionPressed("exitToMenu"))
			{
				GetTree().ChangeSceneToFile("res://MenuChooser.tscn");
			}
			else if (Input.IsActionPressed("QuitToDesktop"))
			{
				GetTree().Quit();
			}
		}
	}
}
