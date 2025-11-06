using System.Collections.Generic;

public interface ICookingService
{
    List<IngredientType> TypeToCook { get; }
    void Cook(ICookable ingredient);
}
