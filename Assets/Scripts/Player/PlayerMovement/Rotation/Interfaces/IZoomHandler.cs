using UnityEngine;

public interface IZoomHandler 
{
    InputSettingsSO InputSettingsSO { get; }
    void HandleZoom(bool condition);
    void Initialize(InputSettingsSO inputSettingsSO, Camera camera);

}
