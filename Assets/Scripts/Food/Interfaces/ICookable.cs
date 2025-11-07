using UnityEngine.Events;

public interface ICookService
{
    bool IsCooked { get; }
    bool IsCooking { get; }
    float CookTime { get; }
    UnityEvent<bool> OnCooked { get; }
    UnityEvent OnBurned { get; }
    void StartCooking();
    void StopCooking();
}
