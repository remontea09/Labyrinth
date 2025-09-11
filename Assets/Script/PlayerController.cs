using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;

    private float speedVelocity = 5f;
    private float gravity = -9.81f;
    private InputAction move;
    private CharacterController characterController;
    private Vector3 moveVelocity;
    private bool canMove = true;

    private int wallBreakCount = 3;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        var input = GetComponent<PlayerInput>();
        input.currentActionMap.Enable();
        move = input.currentActionMap.FindAction("Move");

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (canMove)
        {
            if (characterController.isGrounded)
            {
                moveVelocity.y = -2f;
            }

            var moveValue = move.ReadValue<Vector2>();
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();
            Vector3 moveDirection = camForward * moveValue.y + camRight * moveValue.x;
            moveVelocity.x = moveDirection.x * speedVelocity;
            moveVelocity.z = moveDirection.z * speedVelocity;

            moveVelocity.y += gravity * Time.deltaTime;
            characterController.Move(moveVelocity * Time.deltaTime);
        }
    }

    public void ChangeCanMove()
    {
        canMove = !canMove;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            Destroy(other.gameObject);
            GameManager.instance.OnHitHammerObject();
        }
    }
}