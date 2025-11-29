using UnityEngine;

public interface INpcFabric
{
    GameObject SpawnNpc(Npc configurator, Vector3 pos);
}

public class PeopleFabric : MonoBehaviour, INpcFabric
{
    public static PeopleFabric PeopleFabricInstance = null;
    private INpcConfigurator _npsConfigurator = null;
    private Transform _playerPos = null;    

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

    public void Initialize(Transform playerPos, INpcConfigurator npsConfigurator)
    {
        _playerPos = playerPos;
        _npsConfigurator = npsConfigurator;
    }

    public GameObject SpawnNpc(Npc npc, Vector3 pos)
    {
        GameObject newNpc = Instantiate(npc.PeoplePrefab, pos, Quaternion.identity);
        if (newNpc.TryGetComponent<NpsBehaviorLogic>(out NpsBehaviorLogic component)) _npsConfigurator.ConfigureNpc(component, npc, _playerPos);
        return newNpc;
    }
}

public interface INpcConfigurator
{
    void ConfigureNpc(NpsBehaviorLogic npsBehaviorLogic, Npc Npc, Transform PlayerPos);
}

public class NpsConfigurator : INpcConfigurator
{
    public void ConfigureNpc(NpsBehaviorLogic npsBehaviorLogic, Npc Npc, Transform PlayerPos)
    {
        npsBehaviorLogic.Initialize(Npc, PlayerPos);
    }
}
