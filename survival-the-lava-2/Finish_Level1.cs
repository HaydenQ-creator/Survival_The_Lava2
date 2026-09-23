using Godot;
using System;

public partial class Finish_Level1 : Node3D
{
	private Timer _timer;
	private bool _timerStarted = false; // Prevents the timer from restarting every frame

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_timer.WaitTime = 7.0f; 
		_timer.OneShot = true;  
		_timer.Timeout += OnTimerTimeout;

		// REMOVED: _timer.Start() is no longer called here automatically
	}

	// _Process runs every single frame
	public override void _Process(double delta)
	{
		// 1. Check if the global condition is met, and ensure we haven't already started the timer
		if (Player.finished && !_timerStarted)
		{
			GD.Print("Player finished! Starting the 4-second countdown.");
			
			_timer.Start();
			_timerStarted = true; // Mark as started so this block doesn't run again
		}
	}

	private void OnTimerTimeout()
	{
		GD.Print("Timer finished ticking! Switching to Level 2 safely.");
		Callable.From(() => GetTree().ChangeSceneToFile("res://Level2.tscn")).CallDeferred();
	}
}
