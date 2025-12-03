using UnityEngine;

public class LightController : MonoBehaviour, IConnectable
{
    [SerializeField] private Light _light;
    [SerializeField] private float _maxIntensity = 1f;

    [Tooltip("ID для привязки к выключателю. Если пусто - будет использоваться имя GameObject")]
    [SerializeField] private string _connectionID = "";

    [Tooltip("Автоматически искать ElectricalPanel при старте")]
    [SerializeField] private bool _autoRegister = true;

    public string ConnectionID => string.IsNullOrEmpty(_connectionID) ? name : _connectionID;
    public bool IsPowered { get; protected set; }

    private void Awake()
    {
        if (_light == null) _light = GetComponent<Light>();
    }

    private void Start()
    {
        if (_autoRegister)
        {
            RegisterToPanel();
        }
    }

    private void OnEnable()
    {
        if (!_autoRegister)
        {
            RegisterToPanel();
        }
    }

    private void OnDisable()
    {
        UnregisterFromPanel();
    }

    private void OnDestroy()
    {
        UnregisterFromPanel();
    }

    private void RegisterToPanel()
    {
        var panel = FindObjectOfType<ElectricalPanel>();
        if (panel != null)
        {
            panel.RegisterConnectable(this);
        }
        else
        {
            Debug.LogWarning($"ElectricalPanel not found for {name}");
        }
    }

    private void UnregisterFromPanel()
    {
        var panel = FindObjectOfType<ElectricalPanel>();
        if (panel != null)
        {
            panel.UnregisterConnectable(this);
        }
    }

    public void OnPowerStateChanged(bool isPowered)
    {
        IsPowered = isPowered;

        if (_light != null)
        {
            _light.enabled = isPowered;
            _light.intensity = isPowered ? _maxIntensity : 0f;
        }
    }
}
