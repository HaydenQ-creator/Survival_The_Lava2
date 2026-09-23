using Godot;
using System;

public partial class Level1_Finish : Area3D
{
	
	[Export] public Player TargetNode;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (TargetNode != null)  BodyEntered += TargetNode.OnAreaFinish;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
