using UnityEngine;

public class SpawnEventHandler : IEventHandler
{
    public void Execute(EventParameters parameters)
    {
        Npc npc = parameters.objectParam as Npc;
        Vector3 spawnPos = parameters.vectorParam;

        if (PeopleFabric.PeopleFabricInstance != null && npc != null)
        {
            PeopleFabric.PeopleFabricInstance.SpawnNpc(npc, spawnPos);
        }
    }
}
