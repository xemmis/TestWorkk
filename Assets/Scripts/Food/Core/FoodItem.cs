using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class FoodItem : Pickupable, IFood
{
    [field: SerializeField] public string FoodName { get; private set; }
    [field: SerializeField] public bool IsReadyToServe { get; private set; }
    [field: SerializeField] public bool CanCombine { get; private set; }
    [field: SerializeField] public List<FoodStage> Food { get; private set; }

    public bool Combine(IngredientType ingredientType)
    {
        print("TryCombine");
        foreach (FoodStage foodStage in Food)
        {
            if (foodStage.NextIngridient == ingredientType)
            {
                SpawnNextStage(foodStage.NextStagePrefab);
                return true;
            }
        }
        return false;
    }

    private void SpawnNextStage(GameObject prefab)
    {
        Instantiate(prefab, transform.position, quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<FoodIngredient>(out var component))
        {
            if (!component.IsReadyToCombine || component.InteractionAmount <= 0) return;

            if (Combine(component.Type))
            {
                component.InteractionAmount -= 1;
                if (component.InteractionAmount == 0) Destroy(component.gameObject);
            }
        }
    }
}


