using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputAction movementAction;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float force_value = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 movementInput;
    private Rigidbody rb;
    private PlayerAnimator playerAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        movementAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        movementInput = movementAction.ReadValue<Vector2>();

        if (movementInput != null)
        {
            Debug.Log("Movement Input: " + movementInput);
        }

        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        if (movementInput != Vector2.zero)
        {
            playerAnimator.SetForwardMovement(movementInput);
        }
        else
        {
            playerAnimator.SetForwardMovement(Vector2.zero);
        }

        // Check if player is grounded
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Disable gravity if grounded
        rb.useGravity = !isGrounded;

        // Handle jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.Jump();
            rb.useGravity = true;
            rb.AddForce(Vector3.up * force_value, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
{
    if (groundCheck != null)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
}
