using Godot;
using System;
using System.Collections.Generic;

public partial class DialogueBox : CanvasLayer
{
    [Signal] public delegate void AdvanceRequestedEventHandler();
    [Signal] public delegate void ChoiceSelectedEventHandler(string nextId);

    [Export] public float CharsPerSecond { get; set; } = 40f;

    private Label _nameLabel;
    private RichTextLabel _textLabel;
    private Label _indicator;
    private VBoxContainer _choicesBox;

    private Tween _tween;
    private bool _typing;
    private List<DialogueChoice> _choices = new();

    public override void _Ready()
    {
        // la UI deve funzionare anche con il gioco in pausa
        ProcessMode = ProcessModeEnum.Always;

        _nameLabel = GetNode<Label>("%NameLabel");
        _textLabel = GetNode<RichTextLabel>("%TextLabel");
        _indicator = GetNode<Label>("%ContinueIndicator");
        _choicesBox = GetNode<VBoxContainer>("%ChoicesContainer");
    }

    public void ShowLine(string speaker, string text, List<DialogueChoice> choices)
    {
        _nameLabel.Text = speaker;
        _nameLabel.Visible = speaker != "";
        ClearChoices();
        _indicator.Hide();

        _choices = choices;
        _textLabel.Text = text;
        _textLabel.VisibleRatio = 0f;
        _typing = true;

        double duration = Math.Max(_textLabel.GetTotalCharacterCount() / (double)CharsPerSecond, 0.01);
        _tween?.Kill();
        _tween = CreateTween();
        _tween.TweenProperty(_textLabel, "visible_ratio", 1.0, duration);
        _tween.Finished += OnTypingFinished;
    }

    private void OnTypingFinished()
    {
        _typing = false;
        if (_choices.Count == 0)
            _indicator.Show();
        else
            BuildChoices();
    }

    private void BuildChoices()
    {
        foreach (var choice in _choices)
        {
            var button = new Button { Text = choice.Text };
            string next = choice.Next;
            button.Pressed += () => EmitSignal(SignalName.ChoiceSelected, next);
            _choicesBox.AddChild(button);
        }
        _choicesBox.GetChild<Button>(0).GrabFocus();
    }

    private void ClearChoices()
    {
        foreach (Node child in _choicesBox.GetChildren())
            child.QueueFree();
    }

    public override void _Input(InputEvent @event)
    {
        if (!@event.IsActionPressed("ui_accept"))
            return;

        if (_typing)
        {
            // salta l'effetto macchina da scrivere
            GetViewport().SetInputAsHandled();
            _tween.Kill();
            _textLabel.VisibleRatio = 1f;
            OnTypingFinished();
        }
        else if (_choices.Count == 0)
        {
            GetViewport().SetInputAsHandled();
            EmitSignal(SignalName.AdvanceRequested);
        }
        // se ci sono scelte, è il Button con il focus a gestire Invio
    }
}