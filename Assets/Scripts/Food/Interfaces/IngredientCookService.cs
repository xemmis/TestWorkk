using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class IngredientCookService : MonoBehaviour, ICookService
{
    [field: SerializeField] public bool IsCooked { get; private set; }
    [field: SerializeField] public float CookTime { get; private set; }
    [field: SerializeField] public bool IsCooking { get; private set; } = false;
    public UnityEvent<bool> OnCooked { get; private set; } = new UnityEvent<bool>();
    [SerializeField] private ParticleSystem CookParticls;
    private Coroutine CookRoutine;

    private void Start()
    {
        CookParticls.Stop();
    }

    public void StartCooking()
    {
        if (IsCooking) return;
        CookRoutine = StartCoroutine(CookingCoroutine());
        print("Stard");
    }

    private IEnumerator CookingCoroutine()
    {
        print("StardCoroutine");
        CookParticls.Play();
        IsCooking = true;
        yield return new WaitForSeconds(CookTime);
        IsCooked = true;

        OnCooked?.Invoke(true);
    }

    public void StopCooking()
    {
        print("STOP");
        CookParticls.Stop();
        IsCooking = false;
        StopCoroutine(CookRoutine);
    }
}