using UnityEngine;

public class CookableIngredient : FoodIngredient, ICookable
{
    [field: SerializeField] public bool CanBeCooked { get; private set; }
    [SerializeField] private Material CookedMaterial;
    [field: SerializeField] public IngredientType CookedType { get; private set; }
    public ICookService CookService { get; private set; }
    [SerializeField] private Renderer _renderer;

    protected override void Awake()
    {
        base.Awake();

        if (CanBeCooked)
        {
            CookService = GetComponent<ICookService>();           
            CookService?.OnCooked.AddListener(ChangeIngredientState);
        }

        IsReadyToCombine = false;
    }

    public void ChangeIngredientState(bool condition)
    {
        IsReadyToCombine = condition;
        _renderer.material = CookedMaterial;
        Type = CookedType;
    }

    private void OnDestroy()
    {
        CookService?.OnCooked.RemoveListener(ChangeIngredientState);
    }
}