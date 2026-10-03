using Godot;
using System;

public partial class WhereIsFlight : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string imagePath = "res://Models/pngs/FlightBurping.jpg";
		if (!FileAccess.FileExists(imagePath))
		{
			GD.Print("Where is the image of flight burping bro :sob:");
			System.Environment.FailFast("Im crashing the game");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
