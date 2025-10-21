using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveInputHandler : MonoBehaviour, IMoveInputHandler
{
    private ISprintHandler _sprintHandler;
    private ICrouchHandler _crouchHandler;
    private IMoveHandler _moveHandler;
    private bool _initalized;
    private Vector3 _velocity;
    private bool _isGrounded;
    private float _gravity = -9.81f;
    private CharacterController _characterController;
    [SerializeField] private Character _character;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        SprintHandler sprintHandler = new SprintHandler(_character);
        CrouchHandler crouchHandler = new CrouchHandler(_character);
        MoveHandler moveHandler = new MoveHandler(_character, _characterController, transform);

        Initialize(sprintHandler, crouchHandler, moveHandler);
    }
    private void HandleGravity()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
    public void Initialize(ISprintHandler sprintHandler, ICrouchHandler crouchHandler, IMoveHandler moveHandler)
    {
        _initalized = true;
        _sprintHandler = sprintHandler;
        _crouchHandler = crouchHandler;
        _moveHandler = moveHandler;
    }

    public void CrouchInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            _crouchHandler.HandleCrouch(_characterController, true);
        if (Input.GetKeyUp(KeyCode.LeftControl))
            _crouchHandler.HandleCrouch(_characterController, false);
    }

    public void MoveInput()
    {
        _moveHandler.HandleMove();
    }

    public void SprintInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _sprintHandler.HandleSprint(true);
        if (Input.GetKeyUp(KeyCode.LeftShift))
            _sprintHandler.HandleSprint(false);
    }

    private void Update()
    {
        if (!_initalized) return;

        HandleGravity();
        CrouchInput();
        MoveInput();
        SprintInput();
    }
}
