using Godot;
using System;

public partial class Finish_Level2 : Node3D
{
	private Timer _timer;
	private bool _timerStarted = false; 
	

	public override void _Ready()
	{
		
		_timer = GetNode<Timer>("Timer");
		_timer.WaitTime = 6.0f; 
		_timer.OneShot = true;  
		_timer.Timeout += OnTimerTimeout;

	}

	// _Process runs every single frame
	public override void _Process(double delta)
	{
		if (Player.finished && !_timerStarted)
		{	
			_timer.Start();
			_timerStarted = true;
		}
	}


	private void OnTimerTimeout()
	{
		Callable.From(() => GetTree().ChangeSceneToFile("res://Scenes/Level3.tscn")).CallDeferred();
	}
}
