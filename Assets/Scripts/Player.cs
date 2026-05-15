using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float crouchSpeed = 2f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference clickAction;
    [SerializeField] private InputActionReference redLensAction;
    [SerializeField] private InputActionReference blueLensAction;

    [Header("Gravity/Physics")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = -12f;
    [SerializeField] private float initialFallVelocity = -2f;

    private CharacterController _characterController;
    private Vector2 _moveInput;
    private bool _isGrounded;
    private float _verticalVelocity;

    public bool RedOn = false;
    public bool BlueOn = false;

    public Animator LensAnim;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;

        _characterController.enabled = true;
        HandleGravity();
        HandleMovement();

        
    }

    private void StoreMovementInput(InputAction.CallbackContext context)
    {

        _moveInput = context.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _verticalVelocity = jumpForce;
        }
    }

    private void HandleGravity()
    {
        if (_isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = initialFallVelocity;
        }

        _verticalVelocity += gravity * Time.deltaTime;
    }

    private void HandleMovement()
    {

        var move = cameraTransform.TransformDirection(new Vector3(_moveInput.x, 0, _moveInput.y)).normalized;
        var currentSpeed = walkSpeed;
        var finalMove = move * currentSpeed;
        finalMove.y = _verticalVelocity;

        var collisions = _characterController.Move(finalMove * Time.deltaTime);

        if ((collisions & CollisionFlags.Above) != 0)
        {
            _verticalVelocity = initialFallVelocity;
        }
    }

    private void OnEnable()
    {
        moveAction.action.performed += StoreMovementInput;
        moveAction.action.canceled += StoreMovementInput;
        jumpAction.action.performed += Jump;
        clickAction.action.Enable();
        redLensAction.action.Enable();
        redLensAction.action.performed += ToggleRedLens;
        blueLensAction.action.Enable();
        blueLensAction.action.performed += ToggleBlueLens;
       
    }

    private void OnDisable()
    {
        moveAction.action.performed -= StoreMovementInput;
        moveAction.action.canceled -= StoreMovementInput;
        jumpAction.action.performed -= Jump;
        redLensAction.action.Disable();
        redLensAction.action.performed -= ToggleRedLens;
        blueLensAction.action.Disable();
        blueLensAction.action.performed -= ToggleBlueLens;
    }

    public void ToggleRedLens(InputAction.CallbackContext context)
    {
        //LensManager.Instance.ToggleRedLens();


        //LensAnim.SetBool("RedActive", RedOn);
        if (!BlueOn && LensManager.Instance.playing == false)
        {
            RedOn = !RedOn;
            if (RedOn)
                LensManager.Instance.ToggleRedOn();
            else
                LensManager.Instance.ToggleRedOff();
        }
        else
        {
            return;
        }

    }

    public void ToggleBlueLens(InputAction.CallbackContext context)
    {
        //LensManager.Instance.ToggleBlueLens();


        //LensAnim.SetBool("BlueActive", BlueOn);

        if (!RedOn && LensManager.Instance.playing == false)
        {
            BlueOn = !BlueOn;
            if (BlueOn)
                LensManager.Instance.ToggleBlueOn();
            else
                LensManager.Instance.ToggleBlueOff();
        }
        else
        {
            return;
        }

       
    }

}
