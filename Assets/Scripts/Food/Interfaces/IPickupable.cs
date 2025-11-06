using UnityEngine;

public interface IPickupable
{
    bool CanPickup { get; }
    float ThrowForce { get; }
    void PickUp(Transform parent);
    void Drop();
}
