using UnityEngine;

public class IdleState : INpsState
{
    private Animator Animator;

    public void Enter(NpsBehaviorLogic controller)
    {
        Animator = controller?.GetAnimator();
        Animator?.SetBool("Idle", true);
    }

    public void Exit(NpsBehaviorLogic controller) { }

    public void Update(NpsBehaviorLogic controller)
    {
        Animator?.SetBool("Idle", false);
    }
}