using System.Threading;
using System.Threading.Tasks;

public interface ICookingService
{
    Task CookAsync(IFood food, CancellationToken cancellationToken = default);
}
