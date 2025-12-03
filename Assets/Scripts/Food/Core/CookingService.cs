using System.Collections.Generic;
using UnityEngine;

public abstract class CookingService : MonoBehaviour, ICookingService
{
    [field: SerializeField] public List<IngredientType> TypeToCook { get; private set; }

    [Header("Cooking Settings")]
    [SerializeField] protected float _cookingTime = 5f;
    [SerializeField] protected float _cookingTemperature = 100f;

    [Header("Visual Feedback")]
    [SerializeField] protected ParticleSystem _cookingParticles;
    [SerializeField] protected AudioSource _cookingSound;

    public virtual void Cook(ICookable ingredient)
    {
        if (ingredient == null || !ingredient.CanBeCooked) return;

        Debug.Log($"{name} started cooking {ingredient}");

        ingredient.CookService?.StartCooking();

        // Визуальные эффекты
        if (_cookingParticles != null && !_cookingParticles.isPlaying)
            _cookingParticles.Play();

        if (_cookingSound != null && !_cookingSound.isPlaying)
            _cookingSound.Play();
    }

    public virtual void StopCooking(ICookable ingredient)
    {
        if (ingredient == null) return;

        ingredient.CookService?.StopCooking();

        // Останавливаем эффекты
        if (_cookingParticles != null && _cookingParticles.isPlaying)
            _cookingParticles.Stop();

        if (_cookingSound != null && _cookingSound.isPlaying)
            _cookingSound.Stop();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<CookableIngredient>(out var ingredient))
            return;

        if (TypeToCook.Contains(ingredient.Type))
        {
            Cook(ingredient);
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<CookableIngredient>(out var ingredient))
            return;

        if (TypeToCook.Contains(ingredient.Type))
        {
            StopCooking(ingredient);
        }
    }

    // Метод для проверки доступности (можно переопределить в CookingPlate)
    protected virtual bool IsServiceAvailable()
    {
        return true; // По умолчанию всегда доступен
    }
}