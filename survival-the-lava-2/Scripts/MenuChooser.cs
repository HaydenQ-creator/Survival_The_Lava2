using Godot;
using System;

public partial class MenuChooser : Node3D
{
	
	public int Random(int min, int max)
	{
		return GD.RandRange(min, max);
	}
	
	public override void _Ready()
	{
		int MenuChooser = Random(1,2);
		
		if (MenuChooser == 1)
		{
			string gameScenePath = "res://MainMenu.tscn";
			GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, gameScenePath);
		}
		else if (MenuChooser == 2)
		{
			string gameScenePath = "res://MainMenu2.tscn";
			GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, gameScenePath);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
