using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class FoodIngredient : Pickupable, IIngredient
{
    [field: SerializeField] public IngredientType Type { get; protected set; }
    public bool IsReadyToCombine { get; protected set; } = true;

    [field: SerializeField] public int InteractionAmount { get; set; } = 1;
}
