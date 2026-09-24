using Godot;
using System;

public partial class AudioStreamPlayerFinish : AudioStreamPlayer
{
	private bool hasPlayed = false;

	public override void _Process(double delta)
	{
		if (Player.finished == true && !hasPlayed)
		{
			Play();
			hasPlayed = true;
			Player.finished = false;
		}
		else if (!Player.finished)
		{
			hasPlayed = false; 
		}
	}
}
