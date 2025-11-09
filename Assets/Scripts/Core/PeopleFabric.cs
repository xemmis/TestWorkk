using UnityEngine;

public interface INpsFabric
{
    GameObject SpawnNps(Nps configurator);
}

public class PeopleFabric : MonoBehaviour, INpsFabric
{
    public static PeopleFabric PeopleFabricInstance;
    private INpsConfigurator _npsConfigurator;
    private Transform _playerPos;
    [SerializeField] private GameObject _posTransform;

    public void Initialize(Transform playerPos, INpsConfigurator npsConfigurator)
    {
        _playerPos = playerPos;
        _npsConfigurator = npsConfigurator;
    }

    public GameObject SpawnNps(Nps configurator)
    {
        GameObject newNps = Instantiate(configurator.PeoplePrefab, _posTransform.transform.position, Quaternion.identity);
        if (newNps.TryGetComponent<NpsBehaviorLogic>(out NpsBehaviorLogic component)) _npsConfigurator.ConfigureNps(component, configurator, _playerPos);
        return newNps;
    }
}

public interface INpsConfigurator
{
    void ConfigureNps(NpsBehaviorLogic npsBehaviorLogic, Nps Nps, Transform PlayerPos);
}

public class NpsConfigurator : INpsConfigurator
{
    public void ConfigureNps(NpsBehaviorLogic npsBehaviorLogic, Nps Nps, Transform PlayerPos)
    {
        npsBehaviorLogic.Initialize(Nps, PlayerPos);
    }
}
