using UnityEngine;

public interface INpcFabric
{
    GameObject SpawnNpc(Npc configurator, Vector3 pos);
}

public class PeopleFabric : MonoBehaviour, INpcFabric
{
    public static PeopleFabric PeopleFabricInstance = null;
    private INpcConfigurator _npcConfigurator = null;
    private Transform _playerPos;

    private void Awake()
    {
        if (PeopleFabric.PeopleFabricInstance == null)
        {
            PeopleFabric.PeopleFabricInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(INpcConfigurator npsConfigurator)
    {
        _playerPos = PlayerService.PlayerInstance.GetPlayerPos();
        _npcConfigurator = npsConfigurator;
    }

    public GameObject SpawnNpc(Npc npc, Vector3 pos)
    {
        GameObject newNpc = Instantiate(npc.PeoplePrefab, pos, Quaternion.identity);

        if (newNpc.TryGetComponent<NpcBehaviorLogic>(out NpcBehaviorLogic component)) _npcConfigurator.ConfigureNpc(component, npc, _playerPos);
        return newNpc;
    }
}

public interface INpcConfigurator
{
    void ConfigureNpc(NpcBehaviorLogic npsBehaviorLogic, Npc Npc, Transform PlayerPos);
}

public class NpcConfigurator : INpcConfigurator
{
    public void ConfigureNpc(NpcBehaviorLogic npsBehaviorLogic, Npc Npc, Transform PlayerPos)
    {
        npsBehaviorLogic.Initialize(Npc, PlayerPos);
    }
}
