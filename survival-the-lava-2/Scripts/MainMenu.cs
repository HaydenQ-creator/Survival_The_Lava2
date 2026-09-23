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
		Player.can_move = true;
		string gameScenePath = "res://Level1.tscn"; 
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetTree().ChangeSceneToFile(gameScenePath);
	}
	private void OnTestButtonPressed()
	{
		Player.can_move = true;
		string gameScenePath = "res://Test.tscn"; 
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetTree().ChangeSceneToFile(gameScenePath);
	}
	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
