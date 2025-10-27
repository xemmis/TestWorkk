using UnityEngine.Events;

[System.Serializable]
public class DialogueNode
{
    public string nodeId;
    public string nextNodeId;
    public string npcText;    
    public float EventActivationTime = 3;
    public EventTheme NodeEventTheme;
    public UnityEvent onNodeSelected;
}

public enum EventTheme
{
    None,
    Chase,
    Leave,
}
