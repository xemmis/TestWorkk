using UnityEngine;

public class CrouchHandler : ICrouchHandler
{
    private Character _playerCharacter;

    public CrouchHandler(Character playerCharacter)
    {
        _playerCharacter = playerCharacter;
    }

    public void HandleCrouch(CharacterController characterController, bool condition)
    {
        characterController.height = condition ? _playerCharacter.CrouchHeight : _playerCharacter.StandHeight;

    }
}
