using UnityEngine;
public interface IPickupable
{
    bool CanPickUp { get; }
    void PickUp(Transform HoldPoint);
    void Drop();
}
