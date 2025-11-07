using UnityEngine;

public class CookableIngredient : FoodIngredient, ICookable
{
    [field: SerializeField] public bool CanBeCooked { get; private set; }
    [field: SerializeField] public IngredientType CookedType { get; private set; }
    [SerializeField] private Material _cookedMaterial;
    [SerializeField] private Material _burnedMaterial;
    [SerializeField] private Renderer _renderer;

    public ISoundService SoundService { get; private set; }
    public ICookService CookService { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        if (CanBeCooked)
        {
            CookService = GetComponent<ICookService>();
            CookService?.OnCooked.AddListener(ChangeIngredientState);
            CookService?.OnBurned.AddListener(ChangeToBurnedState);
        }

        IsReadyToCombine = false;
    }

    public void ChangeIngredientState(bool condition)
    {
        IsReadyToCombine = condition;
        _renderer.material = _cookedMaterial;
        Type = CookedType;
    }

    public void ChangeToBurnedState()
    {
        _renderer.material = _burnedMaterial;
        Type = IngredientType.Burned;
    }

    private void OnDestroy()
    {
        CookService?.OnCooked.RemoveListener(ChangeIngredientState);
        CookService?.OnBurned.RemoveListener(ChangeToBurnedState);
    }
}