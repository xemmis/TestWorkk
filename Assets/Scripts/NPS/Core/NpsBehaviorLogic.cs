using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class NpsBehaviorLogic : MonoBehaviour, INpsBehaviorLogic
{
    [SerializeField] private Transform _playerPos;
    private INpsState _currentState;
    private Rigidbody _rigidBody;
    private Animator _animator;
    private NavMeshAgent _agent;
    [SerializeField] private Nps _npsData;
    private UnityEvent _dialogueEvent;

    public void Initialize(Nps nps, Transform playerPos)
    {
        _playerPos = playerPos;
        _npsData = nps;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();

        ChangeState(new WaitState("Coffee"));
    }

    public void ChangeState(INpsState newState)
    {
        print("меняекмяч");

        _currentState?.Exit(this);
        _currentState = newState;
        _currentState?.Enter(this);
    }

    public UnityEvent GetUnityEvent() => _dialogueEvent;
    public void SetUnityEvent(UnityEvent unityEvent) => _dialogueEvent = unityEvent;
    public INpsState GetCurrentState() => _currentState;
    public Animator GetAnimator() => _animator;
    public NavMeshAgent GetAgent() => _agent;
    public Rigidbody GetRigidbody() => _rigidBody;
    public Nps GetNpsData() => _npsData;
    public Transform GetPlayerPos() => _playerPos;
}