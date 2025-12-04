using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Panel Connection")]
    [SerializeField] private string _connectedPanelID = "Light";
    
    [Header("Light Settings")]
    [SerializeField] private Light _light;
    [SerializeField] private float _maxIntensity = 1f;

    private void Awake()
    {
        if (_light == null) _light = GetComponent<Light>();
    }

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
        // Реагируем только на события нашей панели
        if (evt.PanelID == _connectedPanelID)
        {
            UpdateLightState(evt.IsPowered);
        }
    }

    private void UpdateLightState(bool isPowered)
    {
        if (_light != null)
        {
            _light.enabled = isPowered;
            _light.intensity = isPowered ? _maxIntensity : 0f;
        }
    }
}