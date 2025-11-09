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
        _animator = controller.GetAnimator();
        _agent = controller.GetAgent();

        _animator.SetBool("Walk", true);
        _agent?.SetDestination(_positionToWalk.position);
        Debug.Log("WalkState Started");
    }

    public void Exit(NpsBehaviorLogic controller)
    {
        _animator.SetBool("Walk", false);

        _positionToWalk = null;
        _agent = null;
        _animator = null;
    }

    public void Update(NpsBehaviorLogic controller)
    {
        if (_agent == null) return;

       
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (_agent.velocity.sqrMagnitude == 0f) 
            {
                controller.ChangeState(new TalkState());
                Debug.Log("WalkState Completed");
            }
        }
    }
}

