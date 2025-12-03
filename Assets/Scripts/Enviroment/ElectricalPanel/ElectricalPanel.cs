using System.Collections.Generic;
using UnityEngine;

public class ElectricalPanel : MonoBehaviour
{
    [System.Serializable]
    public class PanelSwitchConfig
    {
        public PanelSwitch PanelSwitch = null;
        public bool IsEnabled = true;
        public float LightIntensity = 1f;

        // Список GameObject, на которых ищем IConnectable
        public List<GameObject> ConnectedObjects = new();

        // Динамически подключенные устройства
        [System.NonSerialized] public List<IConnectable> DynamicDevices = new();
    }

    [SerializeField] private List<PanelSwitchConfig> _switches = new();

    // Для динамической регистрации по ConnectionID
    private Dictionary<string, PanelSwitchConfig> _idToConfig = new();

    // Регистрация устройства по ID
    public void RegisterConnectable(IConnectable device)
    {
        if (device == null) return;

        string id = device.ConnectionID;

        // Ищем конфиг по ID
        foreach (var config in _switches)
        {
            // Если ID пустой или совпадает с именем выключателя
            if (string.IsNullOrEmpty(id) ||
                (config.PanelSwitch != null && config.PanelSwitch.name == id))
            {
                config.DynamicDevices.Add(device);
                _idToConfig[id] = config;

                // Сразу применяем текущее состояние
                device.OnPowerStateChanged(config.IsEnabled);
                return;
            }
        }

        Debug.LogWarning($"No switch found for device with ID: {id}");
    }

    public void UnregisterConnectable(IConnectable device)
    {
        if (device == null) return;

        foreach (var config in _switches)
        {
            config.DynamicDevices.Remove(device);
        }
        _idToConfig.Remove(device.ConnectionID);
    }

    private void Start()
    {
        InitializeSwitchConnections();
        ApplyInitialStates();
    }

    private void InitializeSwitchConnections()
    {
        foreach (var config in _switches)
        {
            if (config.PanelSwitch != null)
            {
                config.PanelSwitch.OnSwitchToggled.AddListener(() =>
                    OnSwitchToggled(config));
            }
        }
    }

    private void ApplyInitialStates()
    {
        foreach (var config in _switches)
        {
            UpdateSwitchState(config);
        }
    }

    private void OnSwitchToggled(PanelSwitchConfig config)
    {
        config.IsEnabled = !config.IsEnabled;
        UpdateSwitchState(config);

        // Публикуем событие для обратной совместимости
        PublishSwitchEvent(config);
    }

    private void UpdateSwitchState(PanelSwitchConfig config)
    {
        // 1. Обновляем статические объекты из ConnectedObjects
        foreach (var obj in config.ConnectedObjects)
        {
            if (obj == null) continue;

            var connectables = obj.GetComponents<IConnectable>();
            foreach (var device in connectables)
            {
                device.OnPowerStateChanged(config.IsEnabled);
            }
        }

        // 2. Обновляем динамически зарегистрированные устройства
        foreach (var device in config.DynamicDevices)
        {
            if (device != null)
            {
                device.OnPowerStateChanged(config.IsEnabled);
            }
        }
    }

    private void PublishSwitchEvent(PanelSwitchConfig config)
    {
        // Собираем все Light для обратной совместимости
        List<Light> lights = new List<Light>();

        foreach (var obj in config.ConnectedObjects)
        {
            if (obj != null)
            {
                lights.AddRange(obj.GetComponentsInChildren<Light>());
            }
        }

        EventBus.Publish(new LightSwitchEvent
        {
            ConnectedLights = lights,
            IsEnabled = config.IsEnabled,
            Intensity = config.LightIntensity
        });
    }

    private void OnDestroy()
    {
        // Отписываемся от всех выключателей
        foreach (var config in _switches)
        {
            if (config.PanelSwitch != null)
            {
                config.PanelSwitch.OnSwitchToggled.RemoveAllListeners();
            }
        }
    }

    // Вспомогательный метод для получения состояния выключателя
    public bool GetSwitchState(int switchIndex)
    {
        if (switchIndex >= 0 && switchIndex < _switches.Count)
        {
            return _switches[switchIndex].IsEnabled;
        }
        return false;
    }
}