using UnityEngine;

public class WaitState : INpcState
{
    public WaitState(string requiredFood)
    {
        _requiredFood = requiredFood;
    }

    private string _requiredFood;
    private Animator _animator;
    private NpcBehaviorLogic _behaviorLogic;
    public void Enter(NpcBehaviorLogic controller)
    {
        _behaviorLogic = controller;
        _animator = controller.GetAnimator();
        _animator.SetTrigger("Wait");
    }

    public void AcceptFood(IFood requiredFood)
    {
        if (_requiredFood == requiredFood.FoodName && requiredFood.IsReadyToServe)
        {
            //реализация получения
        }

    }
    public void Exit(NpcBehaviorLogic controller) { }

    public void Update(NpcBehaviorLogic controller) { }
}
