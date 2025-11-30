using UnityEngine;
using UnityEngine.AI;

public class ChaseState : INpcState
{
    public ChaseState(Transform target)
    {
        _targetPos = target;
    }

    private Transform _targetPos;
    private NavMeshAgent _agent;
    private Animator _animator;

    public void Enter(NpcBehaviorLogic controller)
    {
        _agent = controller.GetAgent();
        _animator = controller.GetAnimator();

        _animator.SetBool("Run", true);
        Debug.Log("ChaseState Started");
    }

    public void Exit(NpcBehaviorLogic controller)
    {
        _animator.SetBool("Run", false);
        Debug.Log("ChaseState Ended");

        _targetPos = null;
        _agent = null;
        _animator = null;
    }

    public void Update(NpcBehaviorLogic controller)
    {
        if (_agent == null) return;
        _agent.SetDestination(_targetPos.position);

        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                // реализация "скримера"
            }
        }
    }

}