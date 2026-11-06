using System.Collections.Generic;

public class DialogueChoice
{
    public string Text { get; set; } = "";
    public string Next { get; set; } = "";
}

public class DialogueNode
{
    public string Speaker { get; set; } = "";
    public string Text { get; set; } = "";
    public string Next { get; set; } = "";
    public List<DialogueChoice> Choices { get; set; } = new();
}