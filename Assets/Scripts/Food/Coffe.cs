using UnityEngine;

public class Coffee : MonoBehaviour, IFood
{
    [field: SerializeField] public bool IsReady { get; private set; }

    [field: SerializeField] public string Name { get; private set; }


    public void SetReady(bool conditon)
    {
        IsReady = conditon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsReady) return;

        if (!other.TryGetComponent<NpsBehaviorLogic>(out var npcBehavior)) return;

        if (npcBehavior.GetCurrentState() is not WaitState) return;

        if (npcBehavior.GetCurrentState() is WaitState waitState)
        {
            waitState.AcceptFood(this);
        }
    }
}
