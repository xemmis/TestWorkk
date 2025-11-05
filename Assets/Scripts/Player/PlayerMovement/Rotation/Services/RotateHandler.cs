using UnityEngine;

public class RotateHandler : IRotateHandler
{
    private InputSettingsSO _inputSettingsSO;
    private GameObject _gameObject;
    private GameObject _cameraObject;
    private float horizontalRotation;
    private float verticalRotation;
    public RotateHandler(GameObject CameraObject, GameObject gameObject, InputSettingsSO inputSettingsSO)
    {
        _gameObject = gameObject;
        _cameraObject = CameraObject;
        _inputSettingsSO = inputSettingsSO;
    }

    public void HandleRotate(Vector3 mouseInput)
    {
        if (_gameObject == null || _inputSettingsSO == null) return;

        // Применяем чувствительность и инверсию
        Vector3 processedInput = mouseInput * _inputSettingsSO.mouseSensitivity;
        if (_inputSettingsSO.invertY)
            processedInput.y *= -1;

        // Обновляем вращение
        horizontalRotation += processedInput.x;
        verticalRotation -= processedInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, _inputSettingsSO.minVerticalAngle, _inputSettingsSO.maxVerticalAngle);

        _gameObject.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        _cameraObject.transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }
}
