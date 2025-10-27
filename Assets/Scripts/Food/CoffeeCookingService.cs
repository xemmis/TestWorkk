using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CoffeeCookingService : ICookingService
{
    private readonly float _cookTime;

    public CoffeeCookingService(float cookTime = 5f)
    {
        _cookTime = cookTime;
    }

    public bool IsCooking { get; private set; }

    public async Task CookAsync(IFood food, CancellationToken cancellationToken = default)
    {
        if (IsCooking) return;
        Debug.Log("Da");
        float timer = 0f;
        IsCooking = true;
        while (timer < _cookTime && !cancellationToken.IsCancellationRequested)
        {
            timer += Time.deltaTime;
            await Task.Yield();
        }

        if (!cancellationToken.IsCancellationRequested)
        {
            food.SetReady(true);
            IsCooking = false;
        }
    }
}
