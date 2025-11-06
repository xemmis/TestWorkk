using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Pickupable : MonoBehaviour, IPickupable
{
    [field: SerializeField] public float ThrowForce { get; private set; }
    [field: SerializeField] public bool CanPickup { get; private set; }

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Drop()
    {
        if (CanPickup) return;
        HandleDrop();
    }

    public void PickUp(Transform parent)
    {
        HandlePickUp(parent);
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