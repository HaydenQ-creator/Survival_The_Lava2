using Godot;
using System;

public partial class ShadowArea3d : Area3D
{
	// Might use later 
	// [Export] public ShadowArea3d TheeePlayer;
	

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		
		
		// Might use later awell
		// if (TheeePlayer != null) BodyEntered += TheeePlayer.OnAreaTriggered;
	}
	
	public void OnBodyEntered(Node Body)
	{
		if (Body is Player)
		{
			Player.invul = true;
		}
	}

	public void OnAreaTriggered()
	{
		Player.invul = true;
	}
	

	public override void _Process(double delta)
	{
		
		GD.Print(Player.invul);
	}
}
