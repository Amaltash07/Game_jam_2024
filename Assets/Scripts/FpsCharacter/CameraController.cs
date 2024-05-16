using UnityEngine;
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

    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * verticalCamSenstivity * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * verticalCamSenstivity * Time.deltaTime;
        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, camVerticalRange.x, camVerticalRange.y);

        transform.Rotate(Vector3.up * mousex);
    }
    private void FixedUpdate()
    {
        tiltHandler();
    }
    private void tiltHandler()
    {
        float newTiltAmount = strafeTilt * Input.GetAxis("Horizontal");

        DOTween.To(() => currentTiltAmount, x => currentTiltAmount = x, newTiltAmount, tiltChangeDuration)
            .SetEase(Ease.OutSine);

        head.transform.localRotation = Quaternion.Euler(xRotation, 0, currentTiltAmount);
    }


}
