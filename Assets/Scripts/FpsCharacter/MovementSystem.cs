using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 fallSpeed;
    private bool isGrounded;

    public float Speed;
    public float gravityScale;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public float bobbingStrength = 0.2f;
    private float defaultPosY;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        defaultPosY = Camera.main.transform.localPosition.y;
    }

    void Update()
    {
        groundChecking();
        handleMovement();
        gravitySystem();
        if (isGrounded && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f))
        {
            CameraBobbing();
        }
       
    }

    void handleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        characterController.Move(moveDirection * Speed * Time.deltaTime);
    }

    void groundChecking()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && fallSpeed.y < 0)
        {
            fallSpeed.y = -2f;
        }
    }

    void gravitySystem()
    {
        fallSpeed.y -= gravityScale * Time.deltaTime;
        characterController.Move(fallSpeed * Time.deltaTime);
    }

    void CameraBobbing()
    {
        float bobbingAmount = Mathf.Sin(Time.time * Speed) * bobbingStrength;
        Vector3 localPos = Camera.main.transform.localPosition;
        localPos.y = defaultPosY + bobbingAmount;
        Camera.main.transform.localPosition = localPos;
    }

    
}
