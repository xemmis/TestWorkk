using UnityEngine;

[CreateAssetMenu(fileName = "Nps", menuName = "Nps Menu/New Nps")]
public class Npc : ScriptableObject
{
    public GameObject PeoplePrefab;
    public DialogueTree DialogueTree;
}
