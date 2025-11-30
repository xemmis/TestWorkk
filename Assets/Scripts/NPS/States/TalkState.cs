using System.Collections;
using UnityEngine;

public class TalkState : INpcState
{
    [SerializeField] private Npc _npsData;
    [SerializeField] private DialogueTree _dialogueTree;
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private NpcBehaviorLogic _controller;

    public void Enter(NpcBehaviorLogic controller)
    {
        _npsData = controller.GetNpcData();
        _dialogueTree = _npsData.DialogueTree;
        _dialogueSystem = DialogueSystem.DialogueSystemInstance;
        _dialogueSystem.OnNodeChanged.AddListener(EventHandler);
        _controller = controller;
        _dialogueSystem.StartDialogue(_dialogueTree);
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
                //_controller.ChangeState(new ExitState(NpsWalkPositions.NpsWalkInstance.PositionsToWalk[1]));
                break;
        }
    }

    public void Exit(NpcBehaviorLogic controller)
    {
        _npsData = null;
        _dialogueTree = null;
        _dialogueSystem = null;
        _controller = null;
    }

    public void Update(NpcBehaviorLogic controller) { }
}
