using System.Collections.Generic;

public interface ICookingService
{
    List<IngredientType> TypeToCook { get; }
    bool IsCooking { get; }
    void Cook(IIngridient ingridient);
}
