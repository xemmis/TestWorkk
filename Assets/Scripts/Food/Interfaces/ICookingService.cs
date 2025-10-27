using System.Threading;
using System.Threading.Tasks;

public interface ICookingService
{
    bool IsCooking { get; }
    Task CookAsync(IFood food, CancellationToken cancellationToken = default);
}
