using Unity.Mathematics;
using UnityEngine;


public abstract class FoodItem : Pickupable, IFood
{
    [field: SerializeField] public string FoodName { get; private set; }
    [field: SerializeField] public bool IsReadyToServe { get; private set; }
    [field: SerializeField] public bool CanCombine { get; private set; }
    [field: SerializeField] public FoodStage Food { get; private set; }

    public void Combine(IngredientType ingredientType)
    {
        if (Food.NextIngridient == ingredientType) SpawnNextStage();
    }

    private void SpawnNextStage()
    {
        Instantiate(Food.NextStagePrefab, transform.position, quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Ketcup>(out var component))
        {
            Combine(Food.NextIngridient);
        }
    }
}


