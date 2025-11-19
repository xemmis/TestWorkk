using Unity.Mathematics;
using UnityEngine;

public class BaseUnpackableFood : Pickupable, IInteractable
{
    [field: SerializeField] public bool CanInteract { get; private set; }
    [SerializeField] private int _interactionAmount;
    [SerializeField] private float _interactionForce;
    [SerializeField] private GameObject _spawningFood;

    public void Interact()
    {
        if (_interactionAmount > 0)
        {
            GameObject spawnedFood = Instantiate(_spawningFood, transform.position, quaternion.identity);
            Rigidbody rigidbody = spawnedFood.GetComponent<Rigidbody>();
            rigidbody.AddForce(new Vector3(0, 1, 0) * _interactionForce, ForceMode.Impulse);
        }
    }
}
