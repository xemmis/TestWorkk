using UnityEngine;
using UnityEngine.AI;

public class WalkState : INpsState
{
    private Transform _positionToWalk;
    private NavMeshAgent _agent;
    private Animator _animator;

    public WalkState(Transform positionToWalk)
    {
        _positionToWalk = positionToWalk;
    }

    public void Enter(NpsBehaviorLogic controller)
    {
        controller.ChangeState(new IdleState());
        _animator = controller.GetAnimator();
        _agent = controller.GetAgent();

        _animator.SetBool("Walk", true);
        _agent?.SetDestination(_positionToWalk.position);
    }

    public void Exit(NpsBehaviorLogic controller)
    {
        _agent.isStopped = true;
        _animator.SetBool("Walk", false);

        _positionToWalk = null;
        _agent = null;
        _animator = null;
    }

    public void Update(NpsBehaviorLogic controller) { }
}

