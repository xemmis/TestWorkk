using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CoffeeCookingService : ICookingService
{
    private readonly float _cookTime;

    // ✅ Убрать timer из конструктора - он должен быть локальным
    public CoffeeCookingService(float cookTime = 5f)
    {
        _cookTime = cookTime;
    }

    public async Task CookAsync(IFood food, CancellationToken cancellationToken = default)
    {
        float timer = 0f; // ✅ Timer должен быть локальной переменной!

        while (timer < _cookTime && !cancellationToken.IsCancellationRequested)
        {
            timer += Time.deltaTime;
            await Task.Yield();
        }

        if (!cancellationToken.IsCancellationRequested)
        {
            food.SetReady(true);
        }
    }
}