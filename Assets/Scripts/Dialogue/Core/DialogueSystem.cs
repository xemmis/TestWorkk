using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private DialogueTree _currentTree;
    [SerializeField] private float _dialogueSkipTick;
    [SerializeField] private bool _canSkip;
    private IDialogueVisualizer _dialogueVisualizer;
    private IDialogueInputHandler _dialogueInputHandler;
    private DialogueNode _currentNode;
    public UnityEvent<DialogueNode> OnNodeChanged;
    public static DialogueSystem DialogueSystemInstance;

    private void Awake()
    {
        if (DialogueSystem.DialogueSystemInstance == null) DialogueSystem.DialogueSystemInstance = this;
        else Destroy(gameObject);
    }

    public void Initialize(IDialogueVisualizer dialogueVisualizer, IDialogueInputHandler dialogueInputHandler)
    {
        _dialogueInputHandler = dialogueInputHandler;
        _dialogueVisualizer = dialogueVisualizer;
    }

    public void StartDialogue(DialogueTree dialogueTree)
    {
        _currentTree = dialogueTree;
        _currentNode = _currentTree?.GetCurrentNode(_currentTree.FirstNodeID);
        _dialogueInputHandler.HandleInput(true);
        StartCoroutine(SkipTick());
        UIServices();
        OnNodeChanged?.Invoke(_currentNode);
    }

    public void NextNode()
    {
        if (!_canSkip) return;

        if (_currentNode.nextNodeId == "")
        {
            EndDialogue();
            return;
        }
        _dialogueInputHandler.HandleInput(true);
        _currentNode = _currentTree?.GetCurrentNode(_currentNode.nextNodeId);
        OnNodeChanged?.Invoke(_currentNode);
        StartCoroutine(SkipTick());
        UIServices();
    }

    private IEnumerator SkipTick()
    {
        _canSkip = false;
        yield return new WaitForSeconds(_dialogueSkipTick);

        _canSkip = true;
    }

    private void UIServices()
    {
        _dialogueVisualizer.CleanUpUI();

        _dialogueVisualizer.Visualize(_currentNode);
    }

    public void EndDialogue()
    {
        _dialogueVisualizer.CleanUpUI();
        _dialogueInputHandler.HandleInput(false);

        _currentTree = null;
        _currentNode = null;
    }
}
