using UnityEngine;

public interface NpsFabric
{
    GameObject SpawnNps(Nps configurator);
}


public class PeopleFabric : MonoBehaviour, NpsFabric
{
    public static PeopleFabric PeopleFabricInstance;
    private INpsConfigurator _npsConfigurator;
    private Transform _playerPos;

    public void Initialize(Transform playerPos, INpsConfigurator npsConfigurator)
    {
        _playerPos = playerPos;
        _npsConfigurator = npsConfigurator;
    }

    public GameObject SpawnNps(Nps configurator)
    {
        GameObject newNps = Instantiate(configurator.PeoplePrefab, configurator.SpawnPoint.transform.position, Quaternion.identity);
        if (newNps.TryGetComponent<NpsBehaviorLogic>(out NpsBehaviorLogic component)) _npsConfigurator.ConfigureNps(component, _playerPos);
        return newNps;
    }
}

public interface INpsConfigurator
{
    void ConfigureNps(NpsBehaviorLogic npsBehaviorLogic, Transform PlayerPos);
}

public class NpsConfigurator : INpsConfigurator
{
    private Nps _currentNps;

    public void ConfigureNps(NpsBehaviorLogic npsBehaviorLogic, Transform PlayerPos)
    {
        npsBehaviorLogic.Initialize(_currentNps, PlayerPos);
    }
}
