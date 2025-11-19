using System.Collections.Generic;

public interface IFood : ICombine
{
    List<FoodStage> Food { get; }
    string FoodName { get; }
    bool IsReadyToServe { get; }
}
