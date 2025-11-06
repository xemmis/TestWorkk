using System.Collections;
using UnityEngine;

public abstract class FoodIngridient : Pickupable, IIngridient
{
    [field: SerializeField] public IngredientType Type { get; private set; }
    [field: SerializeField] public bool IsCooked { get; private set; }
    [field: SerializeField] public float CookTime { get; private set; }

    public bool IsCooking { get; private set; } = false;

    private Coroutine CookRoutine;
    public void StartCooking()
    {
        if (IsCooking || IsCooked) return;
        CookRoutine = StartCoroutine(CookingCoroutine());
    }

    private IEnumerator CookingCoroutine()
    {
        IsCooking = true;
        yield return new WaitForSeconds(CookTime);
        IsCooked = true;
        IsCooking = false;
    }

    public void StopCooking()
    {
        if (!IsCooking || IsCooked) return;
        StopCoroutine(CookRoutine);
    }
}
