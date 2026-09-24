using Godot;
using System;

public partial class LevelButtons : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (MainMenu.Level_buttons_visible == true)
		{
			Visible = true;
		}
	}
}
