using System.Collections.Generic;
using UnityEngine;

public abstract class CookingService : MonoBehaviour, ICookingService
{
    [field: SerializeField] public List<IngredientType> TypeToCook { get; private set; }

    public void Cook(ICookable ingredient)
    {
        if (ingredient.CanBeCooked) ingredient.CookService?.StartCooking();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<CookableIngredient>(out var ingridient)) return;

        if (TypeToCook.Contains(ingridient.Type)) Cook(ingridient);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<CookableIngredient>(out var ingridient)) return;

        if (TypeToCook.Contains(ingridient.Type)) ingridient.CookService?.StopCooking();
    }
}