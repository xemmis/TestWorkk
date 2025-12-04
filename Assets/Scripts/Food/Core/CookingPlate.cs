using UnityEngine;

public class CookingPlate : CookingService
{
    [Header("Panel Connection")]
    [SerializeField] private string _connectedPanelID = "Cooking";
    [SerializeField] private GameObject _powerIndicator;

    public bool IsPowered { get; private set; }

    private void OnEnable()
    {
        EventBus.Subscribe<PanelPowerEvent>(OnPanelPowerChanged);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PanelPowerEvent>(OnPanelPowerChanged);
    }

    private void OnPanelPowerChanged(PanelPowerEvent evt)
    {
        if (evt.PanelID == _connectedPanelID)
        {
            IsPowered = evt.IsPowered;
            UpdateVisualIndicator();
        }
    }

    public override void Cook(ICookable ingredient)
    {
        if (!IsPowered)
        {
            Debug.Log($"{name} is not powered, cannot cook");
            return;
        }
        
        base.Cook(ingredient);
    }

    private void UpdateVisualIndicator()
    {
        if (_powerIndicator != null)
        {
            _powerIndicator.SetActive(IsPowered);
        }
    }
}