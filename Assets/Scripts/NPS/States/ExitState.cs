using UnityEngine;
using UnityEngine.AI;

public class ExitState : INpsState
{
    public ExitState(Transform PosToExit)
    {
        _posToExit = PosToExit;
    }

    private Transform _posToExit;
    private NavMeshAgent _agent;
    private Animator _animator;

    public void Enter(NpsBehaviorLogic controller)
    {
        _agent = controller.GetAgent();
        _animator = controller.GetAnimator();

        _agent.SetDestination(_posToExit.position);
        _animator.SetBool("Walk", true);
        Debug.Log("Exit Started");
    }

    public void Exit(NpsBehaviorLogic controller)
    {
        _animator.SetBool("Walk", false);

        _agent = null;
        _animator = null;
        _posToExit = null;
        Debug.Log("Exit Completed");
    }

    public void Update(NpsBehaviorLogic controller)
    {
        if (_agent == null) return;


        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                controller.ChangeState(new IdleState());
            }
        }
    }
}
