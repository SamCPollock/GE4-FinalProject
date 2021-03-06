/// THIS IS A DOXYGEN TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// DOXYGEN TEST
/// </summary>
public class scr_MovementComponent : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 5;

    [SerializeField]
    float runSpeed = 10;

    [SerializeField]
    float jumpForce = 5;

    // Components 
    private scr_PlayerController playerController;
    Rigidbody rb;
    Animator playerAnimator;

    // References
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    public GameObject followTarget;
    public SaveSystem saveSystem;


    Vector2 lookInput = Vector2.zero;

    public float aimSensitivity = 1;

    // animator hashes
    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");
    public readonly int aimVerticalHash = Animator.StringToHash("AimVertical");




    private void Awake()
    {

        playerController = GetComponent<scr_PlayerController>();
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        if (GameManager.instance.cursorActive)
        {
            AppEvents.InvokeMouseCursorEnable(false);
        }

        Cursor.lockState = CursorLockMode.Locked; 

    }

    private void Update()
    {
        // Movement
        if(playerController.isJumping == false)
        {
            if (!(inputVector.magnitude > 0))
            {
                moveDirection = Vector3.zero;
            }

            moveDirection = (transform.forward * inputVector.y) + (transform.right * inputVector.x);
            float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

            Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

            transform.position += movementDirection;
        }

       

        // aiming / looking
        // Horizontal rotation
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x * aimSensitivity, Vector3.up);

        // Vertical rotation
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y * aimSensitivity, Vector3.left);


        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if(angle < 180 && angle > 70)
        {
            angles.x = 70;

        }

        followTarget.transform.transform.localEulerAngles = angles;


        // rotate the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);

        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        playerAnimator.SetFloat(movementXHash, inputVector.x);
        playerAnimator.SetFloat(movementYHash, inputVector.y);

    }
    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, playerController.isRunning);

    }

    public void OnJump(InputValue value)
    {
        if (!playerController.isJumping)
        {
            playerController.isJumping = value.isPressed;

            rb.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
            playerAnimator.SetBool(isJumpingHash, playerController.isJumping);

        }
    }

    public void OnAim(InputValue value)
    {

        playerController.isAiming = value.isPressed;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        playerAnimator.SetFloat(aimVerticalHash, lookInput.y);

        // if we aim up/down, adjust animations to have a mask to properly animate aim. 
    }

    public void OnSave(InputValue value)
    {
        saveSystem.SaveGame();
    }

    public void OnLoad(InputValue value)
    {
        saveSystem.LoadGame();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);

    }







}
