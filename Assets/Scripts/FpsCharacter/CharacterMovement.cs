using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    [Header("Camera Sensitivity")]
    public float verticalCamSenstivity;
    public float horizontalCamSenstivity;
    public Vector2 camVerticalRange;

    [Header("Tilt Settings")]
    public float strafeTilt;
    public float tiltChangeDuration = 0.3f;
    private float currentTiltAmount = 0f;

    [Header("Movement Settings")]
    public float speed;
    public float gravityScale;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    [Header("Bobbing Settings")]
    public float bobbingStrength = 0.2f;
    public float bobbingSpeedMultiplier = 1f;
    private float defaultPosY;

    [Header("Slide Settings")]
    public float slideSpeed = 10f;
    public float slideDuration = 1f;
    public float slideTween = 0.2f;
    public float slideHeight = 0.5f;
    public float slideFOV = 100f;
    public float slideTimeoutTime = 2f;
    private float baseHeight;
    private float currentSlideSpeed;
    private float baseFOV;
    private bool isSliding;
    private bool canSlide;
    private Vector3 slideDirectionBefore;

    [Header("Components")]
    [SerializeField] private Camera head;
    [SerializeField] private CharacterController characterController;
    private float xRotation = 0f;
    private bool isGrounded;
    private Vector3 fallSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        baseHeight = characterController.height;
        defaultPosY = head.transform.localPosition.y;
        head = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        baseFOV = head.fieldOfView;
    }

    void Update()
    {
        MouseLook();
        CheckGroundStatus();
        HandleMovement();
        ApplyGravity();

        if (isGrounded)
        {
            HandleActions();
            if (!isSliding && IsMoving())
                CameraBobbing();
        }
    }

    private void FixedUpdate()
    {
        TiltHandler();
    }

    private void TiltHandler()
    {
        float newTiltAmount = strafeTilt * Input.GetAxis("Horizontal");

        DOTween.To(() => currentTiltAmount, x => currentTiltAmount = x, newTiltAmount, tiltChangeDuration)
            .SetEase(Ease.OutSine);

        head.transform.localRotation = Quaternion.Euler(xRotation, 0, currentTiltAmount);
    }

    void MouseLook()
    {
        float mousex = Input.GetAxis("Mouse X") * verticalCamSenstivity * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * verticalCamSenstivity * Time.deltaTime;
        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, camVerticalRange.x, camVerticalRange.y);

        transform.Rotate(Vector3.up * mousex);
    }

    void HandleActions()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("Slide") && !isSliding && IsMoving() && canSlide)
            Slide();
    }

    bool IsMoving()
    {
        return Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
    }

    void HandleMovement()
    {
        if (isSliding)
        {
            currentSlideSpeed = Mathf.Lerp(currentSlideSpeed, 0f, slideDuration * Time.deltaTime);
            Vector3 slideDirection = slideDirectionBefore.normalized * currentSlideSpeed;
            characterController.Move(slideDirection * Time.deltaTime);
        }
        else
        {
            Vector3 moveDirection = GetMoveDirection();
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
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
        {
            fallSpeed.y = Mathf.Sqrt(2 * jumpHeight * gravityScale);
            StopSlide();
        }
    }

    void Slide()
    {
        if (!isSliding)
        {
            isSliding = true;
            canSlide = false;
            currentSlideSpeed = slideSpeed;

            slideDirectionBefore = GetMoveDirection();

            DOTween.To(() => characterController.height, x => characterController.height = x, slideHeight, slideTween)
                .SetEase(Ease.OutSine);

            DOTween.To(() => head.transform.localPosition, pos => head.transform.localPosition = pos, new Vector3(head.transform.localPosition.x, 0f, head.transform.localPosition.z), slideTween)
                .SetEase(Ease.OutSine);

            DOTween.To(() => head.fieldOfView, x => head.fieldOfView = x, slideFOV, slideTween)
                .SetEase(Ease.OutSine);

            Invoke("StopSlide", slideDuration);
        }
    }

    void StopSlide()
    {
        DOTween.To(() => characterController.height, x => characterController.height = x, baseHeight, slideTween)
            .SetEase(Ease.OutSine);

        DOTween.To(() => head.transform.localPosition, pos => head.transform.localPosition = pos, new Vector3(head.transform.localPosition.x, defaultPosY, head.transform.localPosition.z), slideTween)
            .SetEase(Ease.OutSine);

        DOTween.To(() => head.fieldOfView, x => head.fieldOfView = x, baseFOV, slideTween)
            .SetEase(Ease.OutSine);

        isSliding = false;
        Invoke("resetSlide", slideTimeoutTime);

    }

    void CameraBobbing()
    {
        float bobbingAmount = Mathf.Sin(Time.time * speed * bobbingSpeedMultiplier) * bobbingStrength;
        Vector3 localPos = head.transform.localPosition;
        localPos.y = defaultPosY + bobbingAmount;
        head.transform.localPosition = localPos;
    }
    void resetSlide()
    {
        canSlide = true;
    }
}
