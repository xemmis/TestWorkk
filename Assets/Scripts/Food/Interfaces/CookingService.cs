using System.Collections.Generic;
using UnityEngine;

public abstract class CookingService : MonoBehaviour, ICookingService
{
    [field: SerializeField] public List<IngredientType> TypeToCook { get; private set; }
    public bool IsCooking { get; private set; } = false;

    public void Cook(IIngridient ingridient)
    {
        IsCooking = true;
        ingridient.StartCooking();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<IIngridient>(out var ingridient) || IsCooking) return;

        if (TypeToCook.Contains(ingridient.Type)) Cook(ingridient);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<IIngridient>(out var ingridient) || !IsCooking) return;

        ingridient.StopCooking();
        IsCooking = false;
    }
}