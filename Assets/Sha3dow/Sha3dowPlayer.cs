using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Sha3dowPlayer : MonoBehaviour
{
    public static Sha3dowPlayer instance;

    public float walkSpd = 5f;
    public float sprintSpd = 8f;
    public float jumpForce = 5f;

    float fwdMovement;
    float strafeMovement;
    float vertVel;

    float vertRotation = 0f;
    public float vertRotationLmt = 80f;
    public float mouseSensitivity = 5f;

    CharacterController theCC;

    private void Awake()
    {
        instance = this;
        theCC = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {

    }

    void Update()
    {
        PlayerMovement();
        PlayerCam();
    }

    void PlayerMovement()
    {
        //Move 
        if (theCC.isGrounded)
        {
            fwdMovement = Input.GetAxis("Vertical") * walkSpd;
            strafeMovement = Input.GetAxis("Horizontal") * walkSpd;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                fwdMovement = Input.GetAxis("Vertical") * sprintSpd;
                strafeMovement = Input.GetAxis("Horizontal") * sprintSpd;
            }
        }

        //Set gravity
        vertVel += Physics.gravity.y * Time.deltaTime;

        //Jump
        if (Input.GetKey(KeyCode.Space) && theCC.isGrounded)
        {
            vertVel = jumpForce;
        }

        //Move vector
        Vector3 playerMovement = new Vector3(strafeMovement, vertVel, fwdMovement);
        theCC.Move(transform.rotation * playerMovement * Time.deltaTime);
    }

    void PlayerCam()
    {
        float horiRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horiRotation, 0);

        vertRotation = Mathf.Clamp(vertRotation, -vertRotationLmt, vertRotationLmt);
        vertRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        Camera.main.transform.localRotation = Quaternion.Euler(vertRotation, 0, 0);
    }
}
