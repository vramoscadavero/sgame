using Godot;

public partial class TestDialogue : Node
{
    public override void _Ready()
    {
        DialogueManager.Instance.Start("res://game/system/dialogue/vecchio.json");
    }
}