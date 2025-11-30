using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class NpcBehaviorLogic : MonoBehaviour, INpcBehaviorLogic
{
    [SerializeField] private Transform _playerPos;
    [SerializeField] private Npc _npcData;
    private INpcState _currentState;
    private Rigidbody _rigidBody;
    private Animator _animator;
    private NavMeshAgent _agent;
    private UnityEvent _dialogueEvent;

    public void Initialize(Npc npc, Transform playerPos)
    {
        _playerPos = playerPos;
        _npcData = npc;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();

        ChangeState(new WalkState(WalkPathManager.WalkPathInstance.GetPathByName("TalkPos").Position));
    }

    public void ChangeState(INpcState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState?.Enter(this);
    }

    private void Update()
    {
        _currentState?.Update(this);
        _currentState.GetType();
    }

    public void HandleChaseEvent(float tick)
    {
        StartCoroutine(EventActivationTick(tick));
    }

    private IEnumerator EventActivationTick(float tick)
    {
        yield return new WaitForSeconds(tick);
        ChangeState(new ChaseState(_playerPos));
        DialogueSystem.DialogueSystemInstance.EndDialogue();
    }

    public UnityEvent GetUnityEvent() => _dialogueEvent;
    public void SetUnityEvent(UnityEvent unityEvent) => _dialogueEvent = unityEvent;
    public INpcState GetCurrentState() => _currentState;
    public Animator GetAnimator() => _animator;
    public NavMeshAgent GetAgent() => _agent;
    public Rigidbody GetRigidbody() => _rigidBody;
    public Npc GetNpcData() => _npcData;
    public Transform GetPlayerPos() => _playerPos;
}