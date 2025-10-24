using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PeopleFabric : MonoBehaviour
{
    public static PeopleFabric FabricInstance;

    private void Awake()
    {
        if (PeopleFabric.FabricInstance == null) FabricInstance = this;
        else Destroy(PeopleFabric.FabricInstance.gameObject);
    }

    public GameObject SpawnPeople(PeopleConfigurator peopleConfigurator, Transform spawnPos)
    {
        GameObject newNps = Instantiate(peopleConfigurator.PeoplePrefab, spawnPos.position, Quaternion.identity);
        return newNps;
    }
}


[CreateAssetMenu(fileName = "Nps", menuName = "NpsMenu")]
public class PeopleConfigurator : ScriptableObject
{
    public GameObject PeoplePrefab;
    public DialogueNode FirstNode;
}
