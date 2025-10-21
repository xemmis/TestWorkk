public class SprintHandler : ISprintHandler
{
    private Character _playerCharacter;
    private bool _isInitialized;

    public SprintHandler(Character playerCharacter)
    {
        _playerCharacter = playerCharacter;
    }

    public void HandleSprint(bool condition)
    {
        _playerCharacter.CurrentSpeed = condition ? _playerCharacter.SprintSpeed : _playerCharacter.WalkSpeed;
    }
}