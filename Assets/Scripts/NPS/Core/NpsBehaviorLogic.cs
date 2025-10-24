using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class NpsBehaviorLogic : MonoBehaviour
{
    private INpsState _currentState;
    private Rigidbody _rigidBody;
    private Animator _animator;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
    }
    
    public void ChangeState(INpsState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState?.Enter(this);
    }

    public INpsState GetCurrentState() => _currentState;
    public Animator GetAnimator() => _animator;
    public NavMeshAgent GetAgent() => _agent;
    public Rigidbody GetRigidbody() => _rigidBody;
}