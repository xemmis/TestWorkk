public interface IIngredient
{
    IngredientType Type { get; }
    bool IsReadyToCombine { get; }
    int InteractionAmount {get;}
}

public interface ICookable
{
    bool CanBeCooked { get; }
    public ICookService CookService { get; }
    public IngredientType CookedType { get; }
}