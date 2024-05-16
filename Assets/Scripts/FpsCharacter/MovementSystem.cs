using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MovementSystem : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 fallSpeed;
    private bool isGrounded;
    private bool isSliding;
    private bool isCrouching;

    public float Speed;
    public float gravityScale;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    [Header("Head Bobbing")]
    public float bobbingStrength = 0.2f;
    public float bobbingSpeedMultiplier = 1f;
    private float defaultPosY;

    public float bounceDuration = 1f;
    public float bounceHeight = 0.2f;

    public float slideSpeed = 10f;
    public float slideDuration = 1f;
    public float crouchHeight = 0.5f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        defaultPosY = Camera.main.transform.localPosition.y;
    }

    void Update()
    {
        CheckGroundStatus();
        HandleMovement();
        ApplyGravity();

        if (isGrounded)
        {
            HandleActions();
            if (!isSliding && !isCrouching && IsMoving())
                CameraBobbing();
        }
    }

    void HandleActions()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftControl))
            Slide();

        if (Input.GetKeyDown(KeyCode.C))
            ToggleCrouch();
    }

    bool IsMoving()
    {
        return Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
    }

    void HandleMovement()
    {
        Vector3 moveDirection = GetMoveDirection();

        if (isSliding)
            moveDirection += Vector3.down * slideSpeed;

        characterController.Move(moveDirection * Speed * Time.deltaTime);
    }

    Vector3 GetMoveDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return transform.right * x + transform.forward * z;
    }

    void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && fallSpeed.y < 0)
            fallSpeed.y = -2f;
    }

    void ApplyGravity()
    {
        fallSpeed.y -= gravityScale * Time.deltaTime;
        characterController.Move(fallSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (isGrounded)
            fallSpeed.y = Mathf.Sqrt(2 * jumpHeight * gravityScale);
    }

    void Slide()
    {
        if (!isSliding)
        {
            isSliding = true;
            StartCoroutine(StopSlide());
        }
    }

    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideDuration);
        isSliding = false;
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;
        characterController.height = isCrouching ? crouchHeight : 2f;
    }

    void CameraBobbing()
    {
        float bobbingAmount = Mathf.Sin(Time.time * Speed * bobbingSpeedMultiplier) * bobbingStrength;
        Vector3 localPos = Camera.main.transform.localPosition;
        localPos.y = defaultPosY + bobbingAmount;
        Camera.main.transform.localPosition = localPos;
    }
}
