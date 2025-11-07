using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Pickupable : MonoBehaviour, IPickupable
{
    [SerializeField] private Rigidbody _rigidbody;
    [field: SerializeField] public float ThrowForce { get; private set; }
    [field: SerializeField] public bool CanPickup { get; private set; }

    protected virtual void Awake()
    {
        InitializePickupable();
    }

    protected void InitializePickupable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Drop()
    {
        if (CanPickup) return;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Pickupable"), LayerMask.NameToLayer("Player"), false);
        HandleDrop();
    }

    public void PickUp(Transform parent)
    {
        HandlePickUp(parent);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Pickupable"), LayerMask.NameToLayer("Player"), true);
    }

    private void HandlePickUp(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        CanPickup = false;
        HandlePhysics(true);
    }

    private void HandleDrop()
    {
        transform.parent = null;
        CanPickup = true;
        HandlePhysics(false);
        _rigidbody.AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
    }

    private void HandlePhysics(bool condition)
    {
        _rigidbody.useGravity = !condition;
        _rigidbody.isKinematic = condition;
    }
}