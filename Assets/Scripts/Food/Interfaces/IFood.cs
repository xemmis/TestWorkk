public interface IFood : ICombine
{
    FoodStage Food { get; }
    string FoodName { get; }
    bool IsReadyToServe { get; }
}
