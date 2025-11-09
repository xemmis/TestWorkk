using System.Collections;
using UnityEngine;

public class TalkState : INpsState
{
    [SerializeField] private Nps _npsData;
    [SerializeField] private DialogueTree _dialogueTree;
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private NpsBehaviorLogic _controller;

    public void Enter(NpsBehaviorLogic controller)
    {
        _npsData = controller.GetNpsData();
        _dialogueTree = _npsData.DialogueTree;
        _dialogueSystem = DialogueSystem.DialogueSystemInstance;
        _dialogueSystem.OnNodeChanged.AddListener(EventHandler);
        _controller = controller;
        _dialogueSystem.StartDialogue(_dialogueTree);
        Debug.Log("TalkState Started");
    }

    private void EventHandler(DialogueNode dialogueNode)
    {
        Debug.LogWarning(_controller != null);

        switch (dialogueNode.NodeEventTheme)
        {
            case EventTheme.Chase:
                _controller.HandleChaseEvent(dialogueNode.EventActivationTime);
                _dialogueSystem.OnNodeChanged.RemoveListener(EventHandler);
                break;
            case EventTheme.Leave:
                _controller.ChangeState(new ExitState(NpsWalkPositions.NpsWalkInstance.PositionsToWalk[1]));
                break;
        }
    }

    public void Exit(NpsBehaviorLogic controller)
    {
        Debug.Log("TalkState Completed");
        _npsData = null;
        _dialogueTree = null;
        _dialogueSystem = null;
        _controller = null;
    }

    public void Update(NpsBehaviorLogic controller) { }
}
