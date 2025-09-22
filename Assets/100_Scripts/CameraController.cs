using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    
    public Transform cameraTransform;
    public float mouseSensitivity = 20f;
    private float xRotation = 0f;

    private PlayerInput playerInput;
    private InputAction lookAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.currentActionMap.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseInput = lookAction.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseInput.x);
        xRotation -= mouseInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
