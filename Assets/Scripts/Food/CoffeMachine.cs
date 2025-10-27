using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CoffeeMachine : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private ICookingService _coffeeMachine;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void Initialize(ICookingService cookingService)
    {
        if (cookingService is not CoffeeCookingService) return;

        _coffeeMachine = cookingService;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_coffeeMachine == null && _coffeeMachine.IsCooking) return;

        if (other.TryGetComponent<IFood>(out var component))
        {
            if (component is not Coffee) return;
                       
            _coffeeMachine.CookAsync(component);
        }
    }
}