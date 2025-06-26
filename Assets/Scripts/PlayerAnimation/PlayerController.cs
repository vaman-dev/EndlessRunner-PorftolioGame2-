using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputAction movementAction;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float force_value = 5f;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 movementInput;
    private Rigidbody rb;
    private PlayerAnimator playerAnimator;
    private BoxCollider playerCollider;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("PlayerController: Rigidbody not found!");

        playerAnimator = GetComponent<PlayerAnimator>();
        if (playerAnimator == null)
            Debug.LogError("PlayerController: PlayerAnimator not found!");

        playerCollider = GetComponent<BoxCollider>();
        if (playerCollider == null)
            Debug.LogError("PlayerController: BoxCollider not found!");
    }

    private void OnEnable()
    {
        movementAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (rb == null || playerAnimator == null)
            return;

        // Read input
        movementInput = movementAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        // Animate movement
        if (movementInput != Vector2.zero)
            playerAnimator.SetForwardMovement(movementInput);
        else
            playerAnimator.SetForwardMovement(Vector2.zero);

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumping");
            playerAnimator.Jump();
            rb.AddForce(Vector3.up * force_value, ForceMode.Impulse);
        }

        // Slide
        if (isGrounded && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Sliding");
            playerAnimator.Slide();
        }

        rb.useGravity = true;
    }

    private bool IsGrounded()
    {
        if (playerCollider == null)
            return false;

        Vector3 boxCenter = playerCollider.bounds.center;
        Vector3 boxHalfExtents = playerCollider.bounds.extents;
        Vector3 groundCheckCenter = new Vector3(boxCenter.x, playerCollider.bounds.min.y - 0.05f, boxCenter.z);
        Vector3 checkBoxSize = new Vector3(boxHalfExtents.x * 0.9f, 0.05f, boxHalfExtents.z * 0.9f);

        return Physics.CheckBox(groundCheckCenter, checkBoxSize, Quaternion.identity, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (playerCollider != null)
        {
            Gizmos.color = Color.green;
            Vector3 boxCenter = playerCollider.bounds.center;
            Vector3 boxHalfExtents = playerCollider.bounds.extents;
            Vector3 groundCheckCenter = new Vector3(boxCenter.x, playerCollider.bounds.min.y - 0.05f, boxCenter.z);
            Vector3 checkBoxSize = new Vector3(boxHalfExtents.x * 0.9f, 0.05f, boxHalfExtents.z * 0.9f);

            Gizmos.DrawWireCube(groundCheckCenter, checkBoxSize * 2);
        }
    }
}