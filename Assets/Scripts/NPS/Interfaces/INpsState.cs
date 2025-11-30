public interface INpcState
{
    void Enter(NpcBehaviorLogic controller);
    void Update(NpcBehaviorLogic controller);
    void Exit(NpcBehaviorLogic controller);
}
