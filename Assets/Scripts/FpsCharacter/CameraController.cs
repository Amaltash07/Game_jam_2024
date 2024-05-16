using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public float verticalCamSenstivity;
    public float horizontalCamSenstivity;
    public float strafeTilt;
    public Vector2 camVerticalRange;
    public float tiltChangeDuration = 0.3f; 

    private Camera head;
    private float xRotation = 0f;
    private float currentTiltAmount = 0f;

    void Start()
    {
        head = Camera.main.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnLook(InputValue mouse)
    {
        float mousex = mouse.Get<Vector2>().x * horizontalCamSenstivity * Time.deltaTime;
        float mousey = mouse.Get<Vector2>().y * verticalCamSenstivity * Time.deltaTime;
        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, camVerticalRange.x, camVerticalRange.y);

        transform.Rotate(Vector3.up * mousex);
    }

    private void FixedUpdate()
    {
        float newTiltAmount = strafeTilt * Input.GetAxis("Horizontal");

        DOTween.To(() => currentTiltAmount, x => currentTiltAmount = x, newTiltAmount, tiltChangeDuration)
            .SetEase(Ease.OutSine);

        head.transform.localRotation = Quaternion.Euler(xRotation, 0, currentTiltAmount);
    }


}
