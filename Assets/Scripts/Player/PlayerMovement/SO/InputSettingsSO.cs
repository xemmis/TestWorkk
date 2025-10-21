using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "InputSettings")]

public class InputSettingsSO : ScriptableObject
{
    public float mouseSensitivity = 2f;
    public float rotationSpeed = 5f;
    public bool invertY = false;

    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;

    [Header("Zoom Settings")]
    public float MinZoom = 20f;
    public float MaxZoom = 60f;
    public float ZoomSpeed = 10f;
    public float SmoothTime = 0.1f;
}