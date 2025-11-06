using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class FoodItem : MonoBehaviour, IFood
{
    [field: SerializeField] public string FoodName { get; private set; }

    [field: SerializeField] public bool IsReadyToServe { get; private set; }

    [field: SerializeField] public bool CanPickup { get; private set; }

    [field: SerializeField] public bool CanCombine { get; private set; }

    [field: SerializeField] public FoodStage Food { get; private set; }

    [field: SerializeField] public float ThrowForce { get; private set; }

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Combine(IngredientType ingredientType)
    {
        if (Food.NextIngridient == ingredientType) SpawnNextStage();
    }

    private void SpawnNextStage()
    {
        Instantiate(Food.NextStagePrefab, transform.position, quaternion.identity);
        Destroy(gameObject);
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
        transform.position = Vector3.zero;

        CanPickup = false;
        HandlePhysics(true);
    }

    private void HandleDrop()
    {
        transform.parent = null;
        CanPickup = true;
        _rigidbody.AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
        HandlePhysics(false);
    }

    private void HandlePhysics(bool condition)
    {
        _rigidbody.useGravity = !condition;

        _rigidbody.isKinematic = !condition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Ketcup>(out var component))
        {
            Combine(Food.NextIngridient);
        }
    }
}


