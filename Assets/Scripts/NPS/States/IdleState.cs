using System;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : INpsState
{
    private Animator Animator;

    public void Enter(NpsBehaviorLogic controller)
    {
        Animator = controller?.GetAnimator();
        Animator?.SetBool("Idle", true);
    }

    public void Exit(NpsBehaviorLogic controller) => Animator = null;
    public void Update(NpsBehaviorLogic controller) => Animator?.SetBool("Idle", false);
}

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

        _dialogueSystem.StartDialogue(_dialogueTree);
    }

    private void EventHandler(DialogueNode dialogueNode)
    {
        switch (dialogueNode.NodeEventTheme)
        {
            case EventTheme.Chase:
                ChaseEventHandler chaseEventHandler = new ChaseEventHandler();
                chaseEventHandler.HandleEvent(_controller);
                break;
            case EventTheme.Leave:
                ExitEventHandler exitEventHandler = new ExitEventHandler();
                exitEventHandler.HandleEvent(_controller);
                break;
        }

    }

    public void Exit(NpsBehaviorLogic controller)
    {
        _npsData = null;
        _dialogueTree = null;
        _dialogueSystem = null;
        _controller = null;
    }

    public void Update(NpsBehaviorLogic controller) { }
}

public interface IEventHandler
{
    void HandleEvent(NpsBehaviorLogic controller);
    void Dispose();
}

public class ChaseEventHandler : IEventHandler
{
    private NavMeshAgent _agent;

    public void Dispose()
    {
        _agent = null;
    }

    public void HandleEvent(NpsBehaviorLogic controller)
    {
        _agent = controller.GetAgent();

        _agent.speed = _agent.speed * 1.6f;
        controller.ChangeState(new ChaseState());
        Dispose();
    }
}

public class ExitEventHandler : IEventHandler
{
    private Nps _npsData;

    public void Dispose()
    {
        _npsData = null;
    }

    public void HandleEvent(NpsBehaviorLogic controller)
    {
        _npsData = controller.GetNpsData();

        controller.ChangeState(new ExitState(_npsData.SpawnPoint.transform));
        Dispose();
    }
}

public class ChaseState : INpsState
{
    private Transform _targetPos;
    private NavMeshAgent _agent;
    private Animator _animator;

    public void Enter(NpsBehaviorLogic controller)
    {
        _agent = controller.GetAgent();
        _animator = controller.GetAnimator();
    }

    public void Exit(NpsBehaviorLogic controller)
    {
        _agent.isStopped = true;

        _targetPos = null;
        _agent = null;
        _animator = null;
    }

    public void Update(NpsBehaviorLogic controller)
    {
        _agent.SetDestination(_targetPos.position);
    }
}