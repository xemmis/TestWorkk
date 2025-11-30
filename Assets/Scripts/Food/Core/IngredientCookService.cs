using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class IngredientCookService : MonoBehaviour, ICookService
{
    [field: SerializeField] public bool IsCooked { get; private set; }
    [field: SerializeField] public float CookTime { get; private set; }
    [field: SerializeField] public bool IsCooking { get; private set; } = false;
    [field: SerializeField] public string SoundName = null;
    public UnityEvent<bool> OnCooked { get; private set; } = new UnityEvent<bool>();
    public UnityEvent OnBurned { get; private set; } = new UnityEvent();
    public ISoundService Sound { get; private set; }
    private Coroutine CookRoutine;
    private AudioSource _audioSource;
    private void Awake()
    {
        if (SoundService.SoundServiceInstance != null && Sound == null)
        {
            Sound = SoundService.SoundServiceInstance;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void Init(ISoundService soundService)
    {
        Sound = soundService;
    }

    public void StartCooking()
    {
        if (IsCooking) return;

        if (CookRoutine != null)
        {
            StopCoroutine(CookRoutine);
            CookRoutine = null;
        }

        CookRoutine = StartCoroutine(CookingCoroutine());
    }

    private IEnumerator CookingCoroutine()
    {
        Sound.PlaySound(SoundName, _audioSource);
        IsCooking = true;
        yield return new WaitForSeconds(CookTime);
        IsCooked = true;
        OnCooked?.Invoke(true);

        yield return new WaitForSeconds(CookTime);
        OnBurned?.Invoke();
    }

    public void StopCooking()
    {
        Sound.StopSound(_audioSource);
        IsCooking = false;
        StopCoroutine(CookRoutine);
    }
}