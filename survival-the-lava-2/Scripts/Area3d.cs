using Godot;
using System;

public partial class Area3d : Area3D
{
	[Export] public FloatingLand TargetNode;
	[Export] public IslandEmber TargetNode2;
	[Export] public IslandEmber TargetNode3;
	[Export] public IslandEmber TargetNode4;
	[Export] public IslandEmber TargetNode5;

	public override void _Ready()
	{
		// Wire up each target safely, only if they were dragged into the Inspector slot
		if (TargetNode != null)  BodyEntered += TargetNode.OnAreaTriggered;
		if (TargetNode2 != null) BodyEntered += TargetNode2.OnAreaTriggered;
		if (TargetNode3 != null) BodyEntered += TargetNode3.OnAreaTriggered;
		if (TargetNode4 != null) BodyEntered += TargetNode4.OnAreaTriggered;
		if (TargetNode5 != null) BodyEntered += TargetNode5.OnAreaTriggered;
	}
}
