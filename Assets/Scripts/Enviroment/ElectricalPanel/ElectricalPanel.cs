using System.Collections.Generic;
using UnityEngine;

public class ElectricalPanel : MonoBehaviour
{
    [System.Serializable]
    public class PanelSwitchConfig
    {
        [Tooltip("Выключатель на щитке")]
        public PanelSwitch PanelSwitch = null;
        
        [Tooltip("ID щитка (например: 'KitchenPanel', 'MainPanel')")]
        public string PanelID = "MainPanel";
        
        [Tooltip("Начальное состояние")]
        public bool IsEnabled = true;
    }

    [Header("Конфигурация щитков")]
    [SerializeField] private List<PanelSwitchConfig> _switches = new();

    private void Start()
    {
        InitializeSwitchConnections();
        PublishInitialStates();
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

    private void PublishInitialStates()
    {
        foreach (var config in _switches)
        {
            PublishPanelState(config.PanelID, config.IsEnabled);
        }
    }

    private void OnSwitchToggled(PanelSwitchConfig config)
    {
        config.IsEnabled = !config.IsEnabled;
        PublishPanelState(config.PanelID, config.IsEnabled);
    }

    private void PublishPanelState(string panelID, bool isEnabled)
    {
        EventBus.Publish(new PanelPowerEvent
        {
            PanelID = panelID,
            IsPowered = isEnabled,
            Timestamp = Time.time
        });

        Debug.Log($"Panel '{panelID}' power state: {isEnabled}");
    }

    private void OnDestroy()
    {
        foreach (var config in _switches)
        {
            if (config.PanelSwitch != null)
            {
                config.PanelSwitch.OnSwitchToggled.RemoveAllListeners();
            }
        }
    }

    // Вспомогательные методы
    public bool GetSwitchState(string panelID)
    {
        var config = _switches.Find(s => s.PanelID == panelID);
        return config?.IsEnabled ?? false;
    }

    public void SetSwitchState(string panelID, bool state)
    {
        var config = _switches.Find(s => s.PanelID == panelID);
        if (config != null)
        {
            config.IsEnabled = state;
            PublishPanelState(panelID, state);
        }
    }
}