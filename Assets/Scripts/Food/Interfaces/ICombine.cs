public interface ICombine
{
    bool CanCombine { get; }

    bool Combine(IngredientType ingredientType);
}
