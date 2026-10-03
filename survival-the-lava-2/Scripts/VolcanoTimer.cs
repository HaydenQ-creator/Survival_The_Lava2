using Godot;
using System;

public partial class VolcanoTimer : Node3D
{
	private Timer timer;
	private bool timerStarted = false;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.WaitTime = 20.0f; 
		timer.OneShot = true;  
		timer.Timeout += OnTimerTimeout;

	}

	// _Process runs every single frame
	public override void _Process(double delta)
	{

		if (Player.finished && !timerStarted)
		{	
			timer.Start();
			timerStarted = true; // Mark as started so this block doesn't run again
		}
	}

	private void OnTimerTimeout()
	{
		VolcanoLight.VolcanoActive = true;
	}
}
