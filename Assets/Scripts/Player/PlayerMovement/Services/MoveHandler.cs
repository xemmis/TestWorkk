using UnityEngine;

public class MoveHandler : IMoveHandler
{
    private CharacterController _characterController;
    private Transform _playerTransform;
    public Character PlayerCharacter { get; private set; }

    public MoveHandler(Character character, CharacterController characterController, Transform playerTransform)
    {
        PlayerCharacter = character;
        _characterController = characterController;
        _playerTransform = playerTransform;
    }


    public void HandleMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = _playerTransform.right * horizontal + _playerTransform.forward * vertical;
        _characterController.Move(moveDirection * PlayerCharacter.CurrentSpeed * Time.deltaTime);
    }
}
