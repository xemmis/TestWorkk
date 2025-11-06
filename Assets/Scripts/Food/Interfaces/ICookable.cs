public interface ICookable
{
    bool IsCooked { get; }
    bool IsCooking { get; }
    float CookTime { get; }
    void StartCooking();
    void StopCooking();
}
