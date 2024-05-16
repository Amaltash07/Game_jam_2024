using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MovementSystem : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 fallSpeed;
    private bool isGrounded;
    private bool isLanded;

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
            if (Input.GetButtonDown("Jump"))
                Jump();

            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
                CameraBobbing();
        }
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        characterController.Move(moveDirection * Speed * Time.deltaTime);
    }

    void CheckGroundStatus()
    {
        isLanded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isLanded && !isGrounded)
        {
            BounceCamera();
            isGrounded = true;
        }
        else if (!isLanded)
        {
            isGrounded = isLanded;
        }

        if (isGrounded && fallSpeed.y < 0)
        {
            fallSpeed.y = -2f;
        }
    }

    void ApplyGravity()
    {
        fallSpeed.y -= gravityScale * Time.deltaTime;
        characterController.Move(fallSpeed * Time.deltaTime);
    }

    void Jump()
    {
        fallSpeed.y = Mathf.Sqrt(2 * jumpHeight * gravityScale);
    }

    void CameraBobbing()
    {
        float bobbingAmount = Mathf.Sin(Time.time * Speed * bobbingSpeedMultiplier) * bobbingStrength;
        Vector3 localPos = Camera.main.transform.localPosition;
        localPos.y = defaultPosY + bobbingAmount;
        Camera.main.transform.localPosition = localPos;
    }

    void BounceCamera()
    {
        float originalPosY = Camera.main.transform.localPosition.y;

        Camera.main.transform.DOLocalMoveY(originalPosY - bounceHeight, bounceDuration * 0.5f).SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                Camera.main.transform.DOLocalMoveY(originalPosY, bounceDuration * 0.5f).SetEase(Ease.InOutSine);
            });
    }

}
