using System.Collections;
using UnityEngine;

public class ZoomHandler : MonoBehaviour ,IZoomHandler
{
    public InputSettingsSO InputSettingsSO { get; private set; }
    private Camera _camera;
    private float _targetZoom;
    private float _currentZoom;
    private float _zoomVelocity;
    private bool _initialized;

    public void Initialize(InputSettingsSO inputSettingsSO, Camera camera)
    {
        InputSettingsSO = inputSettingsSO;
        _camera = camera;
        _initialized = true;
        _currentZoom = inputSettingsSO.MaxZoom;
    }

    public void HandleZoom(bool zoomInput)
    {
        if (!_initialized) return;

        if (zoomInput)
        {
            _targetZoom = InputSettingsSO.MinZoom;
            StartCoroutine(ZoomCoroutine());
        }
        else
        {
            _targetZoom = InputSettingsSO.MaxZoom;
            StartCoroutine(ZoomCoroutine());
        }
    }

    private IEnumerator ZoomCoroutine()
    {
        while (Mathf.Abs(_currentZoom - _targetZoom) > 0.01f)
        {
            _currentZoom = Mathf.SmoothDamp(_currentZoom, _targetZoom, ref _zoomVelocity, InputSettingsSO.SmoothTime);
            _camera.fieldOfView = _currentZoom;
            yield return null; // Ждем следующий кадр
        }

        // Финальное значение для точности
        _currentZoom = _targetZoom;
        _camera.fieldOfView = _currentZoom;
    }
}
