using Godot;

public partial class Area3d : Area3D
{
	// Assign this in the Inspector by dragging your target node into the slot
	[Export] public FloatingLand TargetNode;
	[Export] public IslandEmber TargetNode2;
	[Export] public IslandEmber TargetNode3;
	[Export] public IslandEmber TargetNode4;
	[Export] public IslandEmber TargetNode5;

	public override void _Ready()
	{
		if (TargetNode != null)
		{
			// Connect the BodyEntered signal to the method in your target script
			BodyEntered += TargetNode.OnAreaTriggered;
			BodyEntered += TargetNode2.OnAreaTriggered;
			BodyEntered += TargetNode3.OnAreaTriggered;
			BodyEntered += TargetNode4.OnAreaTriggered;
			BodyEntered += TargetNode5.OnAreaTriggered;
		}
		else
		{
			GD.PrintErr("TargetNode is missing! Assign it in the Inspector.");
		}
	}
}
