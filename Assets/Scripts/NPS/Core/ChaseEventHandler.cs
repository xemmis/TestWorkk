using System.Diagnostics;
using UnityEngine.AI;

public class ChaseEventHandler : IEventHandler
{
    private NavMeshAgent _agent;

    public ChaseEventHandler(NavMeshAgent navMeshAgent)
    {
        _agent = navMeshAgent;
    }
    public void Dispose()
    {
        _agent = null;
    }

    public void HandleEvent(NpsBehaviorLogic controller)
    {
        if (_agent == null) throw new System.NullReferenceException("Agent is Null");
        _agent.speed = _agent.speed * 1.6f;
        controller.ChangeState(new ChaseState(controller.GetPlayerPos()));
        Dispose();
    }
}
