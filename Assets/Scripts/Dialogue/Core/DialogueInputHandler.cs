using UnityEngine;

public class DialogueInputHandler : MonoBehaviour, IDialogueInputHandler
{
    public bool IsWaiting;
    private DialogueSystem _dialogueSystem;

    private void Start()
    {
        if (DialogueSystem.DialogueSystemInstance != null) _dialogueSystem = DialogueSystem.DialogueSystemInstance;
    }

    public void HandleInput(bool condition)
    {
        IsWaiting = condition;
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown) _dialogueSystem.NextNode();
    }

    private void Update()
    {
        if (IsWaiting) CheckInput();
    }
}
