using UnityEngine;
using UnityEngine.AI;

public class ChaseState : INpsState
{
    public ChaseState(Transform target)
    {
        _targetPos = target;
    }

    private Transform _targetPos;
    private NavMeshAgent _agent;
    private Animator _animator;

    public void Enter(NpsBehaviorLogic controller)
    {
        _agent = controller.GetAgent();
        _animator = controller.GetAnimator();

        _animator.SetBool("Run", true);
        Debug.Log("ChaseState Started");
    }

    public void Exit(NpsBehaviorLogic controller)
    {
        _animator.SetBool("Run", false);
        Debug.Log("ChaseState Ended");

        _targetPos = null;
        _agent = null;
        _animator = null;
    }

    public void Update(NpsBehaviorLogic controller)
    {
        if (_agent == null) return;
        _agent.SetDestination(_targetPos.position);

        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                controller.ChangeState(new ExitState(NpsWalkPositions.NpsWalkInstance.PositionsToWalk[1]));
            }
        }
    }

}