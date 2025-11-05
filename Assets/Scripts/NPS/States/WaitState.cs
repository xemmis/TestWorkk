using UnityEngine;

public class WaitState : INpsState
{
    public WaitState(string requiredFood)
    {
        _requiredFood = requiredFood;
    }

    private string _requiredFood;
    private Animator _animator;
    private NpsBehaviorLogic _behaviorLogic;
    public void Enter(NpsBehaviorLogic controller)
    {
        _behaviorLogic = controller;
        _animator = controller.GetAnimator();
        _animator.SetTrigger("Wait");
    }

    public void AcceptFood(IFood requiredFood)
    {
        if (_requiredFood == requiredFood.FoodName && requiredFood.IsReadyToServe)
        {
            Debug.Log("Succses");
            _behaviorLogic.ChangeState(new IdleState());
        }

    }
    public void Exit(NpsBehaviorLogic controller) { }

    public void Update(NpsBehaviorLogic controller) { }
}
