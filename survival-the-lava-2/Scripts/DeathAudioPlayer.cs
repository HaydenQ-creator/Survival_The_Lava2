using Godot;
using System;

public partial class DeathAudioPlayer : AudioStreamPlayer
{
	private bool hasPlayed = false;

	public override void _Process(double delta)
	{
		if (!Player.can_move && !hasPlayed)
		{
			Play();
			hasPlayed = true;
		}
		else if (Player.can_move)
		{
			hasPlayed = false; 
		}
	}
}
