using UnityEngine;

[CreateAssetMenu(fileName = "Nps", menuName = "NPC/New Nps")]
public class Npc : ScriptableObject
{
    public GameObject PeoplePrefab;
    public DialogueTree DialogueTree;
}
