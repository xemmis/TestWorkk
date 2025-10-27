using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Tree")]
public class DialogueTree : ScriptableObject
{
    public string FirstNodeID;
    public List<DialogueNode> nodes = new List<DialogueNode>();

    public DialogueNode GetCurrentNode(string Id)
    {
        if (string.IsNullOrEmpty(Id) || nodes == null || nodes.Count == 0)
            return null;

        return nodes.FirstOrDefault(node => node.nodeId == Id);
    }
}