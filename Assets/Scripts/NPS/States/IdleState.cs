using System;
using UnityEngine;

public class IdleState : INpcState
{
    private Animator Animator;

    public void Enter(NpcBehaviorLogic controller)
    {
        Animator = controller?.GetAnimator();
        Animator?.SetBool("Idle", true);
    }

    public void Exit(NpcBehaviorLogic controller) => Animator = null;
    public void Update(NpcBehaviorLogic controller) => Animator?.SetBool("Idle", false);
}
