using System.Linq;
using UnityEngine;

public class DialogueInputHandler : MonoBehaviour, IDialogueInputHandler
{
    public bool IsWaiting;
    private DialogueSystem _dialogueSystem;
    private readonly KeyCode[] _nextDialogueKeys = {
    KeyCode.Space,
    KeyCode.Return,
    KeyCode.KeypadEnter
};

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
        if (_nextDialogueKeys.Any(Input.GetKeyDown) ||
            Input.GetMouseButtonDown(0))
        {
            _dialogueSystem.NextNode();
        }
    }

    private void Update()
    {
        if (IsWaiting) CheckInput();
    }
}
