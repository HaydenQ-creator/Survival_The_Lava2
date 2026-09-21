using Godot;
using System;

public partial class MainMenu : Control
{
	private Button _startButton;
	private Button _quitButton;
	private Button _testButton;

	public override void _Ready()
	{
		_startButton = GetNode<Button>("StartButton");
		_startButton.Pressed += OnStartButtonPressed;
		
		_quitButton = GetNode<Button>("QuitButton");
		_quitButton.Pressed += OnQuitButtonPressed;

		_testButton = GetNode<Button>("TestButton");
		_testButton.Pressed += OnTestButtonPressed;
	}

	private void OnStartButtonPressed()
	{
		string gameScenePath = "res://Level1.tscn"; 

		GetTree().ChangeSceneToFile(gameScenePath);
	}
	private void OnTestButtonPressed()
	{
		string gameScenePath = "res://Test.tscn"; 

		GetTree().ChangeSceneToFile(gameScenePath);
	}
	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
