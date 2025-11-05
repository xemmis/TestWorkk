using UnityEngine;

public class CameraMoveInputHandler : MonoBehaviour, ICameraMoveInputHandler
{
    private IRotateHandler _rotateHandler;
    private IZoomHandler _zoomHandler;
    [SerializeField] private InputSettingsSO _inputSettingsSO;
    [SerializeField] private Camera _camera;

    private bool _initialized;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _zoomHandler = GetComponent<IZoomHandler>();

        RotateHandler rotateHandler = new(_camera.gameObject, gameObject, _inputSettingsSO);
        _zoomHandler.Initialize(_inputSettingsSO, _camera);
        ZoomHandler zoomHandler = _zoomHandler as ZoomHandler;
        Initialize(_inputSettingsSO, rotateHandler, zoomHandler);
    }

    public void Initialize(InputSettingsSO inputSettingsSO, IRotateHandler rotateHandler, ZoomHandler zoomHandler)
    {
        _inputSettingsSO = inputSettingsSO;
        _rotateHandler = rotateHandler;
        _zoomHandler = zoomHandler;
        _initialized = true;
    }

    private void Update()
    {
        RotateInput();
        ZoomInput();
    }

    public void RotateInput()
    {
        if (!_initialized) return;

        Vector3 mouseInput = new Vector3(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"),
            0f
        );

        if (mouseInput != Vector3.zero)
        {
            _rotateHandler?.HandleRotate(mouseInput);
        }
    }

    public void ZoomInput()
    {
        if (!_initialized) return;
        if (Input.GetKeyDown(KeyCode.C))
            _zoomHandler.HandleZoom(true);
        if (Input.GetKeyUp(KeyCode.C))
            _zoomHandler.HandleZoom(false);
    }
}