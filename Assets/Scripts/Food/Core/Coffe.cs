using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Coffee : MonoBehaviour, IFood, IPickupable
{
    [field: SerializeField] public bool IsReady { get; private set; }

    [field: SerializeField] public string Name { get; private set; }

    public bool CanPickUp { get; private set; }

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Drop()
    {
        transform.parent = null;
        HandleRigidBody(false);
        _rb.AddForce(transform.forward * 5, ForceMode.Impulse);
    }

    public void PickUp(Transform holdPoint)
    {
        transform.parent = holdPoint;
        transform.localPosition = holdPoint.localPosition;
        HandleRigidBody(true);
    }

    private void HandleRigidBody(bool condititon)
    {
        _rb.isKinematic = condititon;
    }

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
