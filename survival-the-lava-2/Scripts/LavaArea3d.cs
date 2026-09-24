using Godot;
using System;

public partial class LavaArea3d : Area3D
{
	[Export] public Player TargetNode;
	[Export] public DeathCamera3d TargetNode2;

	public override void _Ready()
	{
		// Wire up each target safely, only if they were dragged into the Inspector slot
		if (TargetNode != null)  BodyEntered += TargetNode.OnAreaTriggered;
		if (TargetNode2 != null)  BodyEntered += TargetNode2.OnAreaTriggered;

	}
}
