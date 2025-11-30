using UnityEngine;
using UnityEngine.AI;

public class WalkState : INpcState
{
    private Vector3 _positionToWalk;
    private NavMeshAgent _agent;
    private Animator _animator;

    public WalkState(Vector3 positionToWalk)
    {
        _positionToWalk = positionToWalk;
    }

    public void Enter(NpcBehaviorLogic controller)
    {
        _animator = controller.GetAnimator();
        _agent = controller.GetAgent();

        _animator.SetBool("Walk", true);
        _agent?.SetDestination(_positionToWalk);
    }

    public void Exit(NpcBehaviorLogic controller)
    {
        _positionToWalk = Vector3.zero;
        _agent = null;
        _animator = null;
    }

    public void Update(NpcBehaviorLogic controller)
    {
        if (_agent == null) return;


        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (_agent.velocity.sqrMagnitude == 0f)
            {
               // реализации когда дошёл
            }
        }
    }
}

