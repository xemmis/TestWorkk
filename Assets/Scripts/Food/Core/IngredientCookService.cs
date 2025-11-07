using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class IngredientCookService : MonoBehaviour, ICookService
{
    [field: SerializeField] public bool IsCooked { get; private set; }
    [field: SerializeField] public float CookTime { get; private set; }
    [field: SerializeField] public bool IsCooking { get; private set; } = false;
    public UnityEvent<bool> OnCooked { get; private set; } = new UnityEvent<bool>();
    public UnityEvent OnBurned { get; private set; } = new UnityEvent();
    private Coroutine CookRoutine;

    public void StartCooking()
    {
        if (IsCooking) return;

        if (CookRoutine != null)
        {
            StopCoroutine(CookRoutine);
            CookRoutine = null;
        }

        CookRoutine = StartCoroutine(CookingCoroutine());
        print("Start");
    }

    private IEnumerator CookingCoroutine()
    {
        print("StartCoroutine");
        IsCooking = true;
        yield return new WaitForSeconds(CookTime);
        IsCooked = true;
        OnCooked?.Invoke(true);

        yield return new WaitForSeconds(CookTime);
        OnBurned?.Invoke();
    }

    public void StopCooking()
    {
        print("STOP");
        IsCooking = false;
        StopCoroutine(CookRoutine);
    }
}