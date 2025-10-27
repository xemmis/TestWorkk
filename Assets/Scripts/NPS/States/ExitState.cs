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
    }

    public void Exit(NpsBehaviorLogic controller)
    {
        _agent.isStopped = true;
        _animator.SetBool("Walk", false);

        _agent = null;
        _animator = null;
        _posToExit = null;
    }

    public void Update(NpsBehaviorLogic controller) { }
}
