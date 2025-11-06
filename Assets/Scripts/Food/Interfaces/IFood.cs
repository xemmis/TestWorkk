using UnityEngine;

public interface IFood : IPickupable, ICombine
{
    FoodStage Food { get; }
    string FoodName { get; }
    bool IsReadyToServe { get; }
}

public interface IIngridient : IPickupable, ICookable
{
    IngredientType Type { get; }
    bool IsReady { get; }
}

public abstract class FoodIngridient : MonoBehaviour, IIngridient
{
    [field: SerializeField] public IngredientType Type { get; private set; }

    [field: SerializeField] public bool IsReady { get; private set; }

    [field: SerializeField] public bool CanPickup { get; private set; }

    [field: SerializeField] public float ThrowForce { get; private set; }

    [field: SerializeField] public bool IsCooked { get; private set; }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void PickUp(Transform parent)
    {
        throw new System.NotImplementedException();
    }

    public void StartCooking()
    {
        throw new System.NotImplementedException();
    }
}