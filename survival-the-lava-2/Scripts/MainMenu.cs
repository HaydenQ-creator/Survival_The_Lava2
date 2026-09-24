using Godot;
using System;

public partial class MainMenu : Control
{
	private Button _startButton;
	private Button _quitButton;
	private Button _testButton;

	private Button _Level1Button;
	private Button _Level2Button;
	private Button _Level3Button;

	private Button _ChooseLevelButton;

	public static bool Level_buttons_visible { get; set; } = false;
	// Global variables are so cool bro


public override void _Ready()
{
	Level_buttons_visible = false;
	_startButton = GetNodeOrNull<Button>("StartButton");
	if (_startButton != null) _startButton.Pressed += OnStartButtonPressed;
	
	_quitButton = GetNodeOrNull<Button>("QuitButton");
	if (_quitButton != null) _quitButton.Pressed += OnQuitButtonPressed;

	_testButton = GetNodeOrNull<Button>("TestButton");
	if (_testButton != null) _testButton.Pressed += OnTestButtonPressed;

	// Use the relative path for nested level buttons safely
	_Level1Button = GetNodeOrNull<Button>("Level1");
	if (_Level1Button != null) _Level1Button.Pressed += OnLevel1ButtonPressed;

	_Level2Button = GetNodeOrNull<Button>("Level2");
	if (_Level2Button != null) _Level2Button.Pressed += OnLevel2ButtonPressed;

	_Level3Button = GetNodeOrNull<Button>("Level3");
	if (_Level3Button != null) _Level3Button.Pressed += OnLevel3ButtonPressed;

	_ChooseLevelButton = GetNodeOrNull<Button>("ChooseLevel");
	if (_ChooseLevelButton != null) _ChooseLevelButton.Pressed += OnChooseLevelButtonPressed;
}


	private void OnStartButtonPressed()
	{
		Level_buttons_visible = false;
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
	private void OnLevel1ButtonPressed()
	{
		Level_buttons_visible = false;
		Player.can_move = true;
		string gameScenePath = "res://Level1.tscn"; 
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetTree().ChangeSceneToFile(gameScenePath);
	}
	private void OnLevel2ButtonPressed()
	{
		Level_buttons_visible = false;
		Player.can_move = true;
		string gameScenePath = "res://Level2.tscn";
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetTree().ChangeSceneToFile(gameScenePath);
	}
	private void OnLevel3ButtonPressed()
	{
		Level_buttons_visible = false;
		Player.can_move = true;
		string gameScenePath = "res://Test.tscn";
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetTree().ChangeSceneToFile(gameScenePath);
	}
	private void OnChooseLevelButtonPressed()
	{
		Level_buttons_visible = true;
		
	}
}
