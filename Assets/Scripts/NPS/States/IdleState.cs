using System;
using UnityEngine;

public class IdleState : INpsState
{
    private Animator Animator;

    public void Enter(NpsBehaviorLogic controller)
    {
        Animator = controller?.GetAnimator();
        Animator?.SetBool("Idle", true);

        Debug.Log("Idle Completed");
        controller.ChangeState(new WalkState(NpsWalkPositions.NpsWalkInstance.PositionsToWalk[0]));
    }

    public void Exit(NpsBehaviorLogic controller) => Animator = null;
    public void Update(NpsBehaviorLogic controller) => Animator?.SetBool("Idle", false);
}
