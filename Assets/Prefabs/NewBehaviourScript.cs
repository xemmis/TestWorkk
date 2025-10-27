using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Nps _nps;
    [SerializeField] private DialogueSystem _dialogueSystem;

    private void Start()
    {
        _dialogueSystem.StartDialogue(_nps.DialogueTree);
    }
}
