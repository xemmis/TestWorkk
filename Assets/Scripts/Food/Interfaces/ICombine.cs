public interface ICombine
{
    bool CanCombine { get; }

    void Combine(IngredientType ingredientType);
}
