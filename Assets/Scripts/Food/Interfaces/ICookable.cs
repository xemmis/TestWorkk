using UnityEngine.Events;

public interface ICookService
{
    bool IsCooked { get; }
    bool IsCooking { get; }
    float CookTime { get; }
    UnityEvent<bool> OnCooked { get; }
    void StartCooking();
    void StopCooking();
}
