using Godot;
using System.Collections.Generic;
using System.Text.Json;

public partial class DialogueManager : Node
{
    [Signal] public delegate void DialogueStartedEventHandler();
    [Signal] public delegate void DialogueEndedEventHandler();

    public static DialogueManager Instance { get; private set; }

    private const string DialogueBoxPath = "res://game/system/dialogue/dialogue_box.tscn";

    public bool IsActive { get; private set; }

    private Dictionary<string, DialogueNode> _data = new();
    private string _currentId = "";
    private DialogueBox _box;

    public override void _Ready()
    {
        Instance = this;
        ProcessMode = ProcessModeEnum.Always;
    }

    public async void Start(string path, string startId = "start")
    {
        if (IsActive)
            return;

        using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
        if (file == null)
        {
            GD.PushError($"Dialogo non trovato: {path}");
            return;
        }

        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _data = JsonSerializer.Deserialize<Dictionary<string, DialogueNode>>(file.GetAsText(), options);
        }
        catch (JsonException e)
        {
            GD.PushError($"JSON non valido in {path}: {e.Message}");
            return;
        }

        IsActive = true;

        var scene = GD.Load<PackedScene>(DialogueBoxPath);
        _box = scene.Instantiate<DialogueBox>();
        GetTree().Root.CallDeferred(Node.MethodName.AddChild, _box);

        // aspetta che la UI sia nell'albero e abbia eseguito il suo _Ready
        await ToSignal(_box, Node.SignalName.Ready);

        _box.AdvanceRequested += OnAdvance;
        _box.ChoiceSelected += ShowNode;

        GetTree().Paused = true;
        EmitSignal(SignalName.DialogueStarted);
        ShowNode(startId);
    }

    private void ShowNode(string id)
    {
        if (string.IsNullOrEmpty(id) || !_data.TryGetValue(id, out var node))
        {
            End();
            return;
        }
        _currentId = id;
        _box.ShowLine(node.Speaker, node.Text, node.Choices);
    }

    private void OnAdvance() => ShowNode(_data[_currentId].Next);

    private void End()
    {
        _box.QueueFree();
        IsActive = false;
        GetTree().Paused = false;
        EmitSignal(SignalName.DialogueEnded);
    }
}