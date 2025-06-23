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
        // Run Input
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
            Debug.Log("Jumping");
            playerAnimator.Jump();
            rb.useGravity = true;
            rb.AddForce(Vector3.up * force_value, ForceMode.Impulse);
        }

        //Handle sliding 
        if (isGrounded && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Sliding");
            // Ensure the player is not already sliding
            playerAnimator.Slide();
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




// ///
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerController : MonoBehaviour
// {
//     [SerializeField] private InputAction movementAction;
//     [SerializeField] private float moveSpeed = 5f;
//     [SerializeField] private float force_value = 5f;
//     [SerializeField] private Transform groundCheck;
//     [SerializeField] private float groundCheckRadius = 0.2f;
//     [SerializeField] private LayerMask groundLayer;

//     private Vector2 movementInput;
//     private Rigidbody rb;
//     private PlayerAnimator playerAnimator;

//     private Vector2 startTouchPosition;
//     private Vector2 endTouchPosition;
//     private bool isSwipeUp = false;
//     private bool isSwipeDown = false;

//     private void Awake()
//     {
//         rb = GetComponent<Rigidbody>();
//         playerAnimator = GetComponent<PlayerAnimator>();
//     }

//     private void OnEnable()
//     {
//         movementAction.Enable();
//     }

//     private void OnDisable()
//     {
//         movementAction.Disable();
//     }

//     private void Update()
//     {
//         PlayerMovement();
//         DetectSwipe();
//     }

//     private void PlayerMovement()
//     {
//         movementInput = movementAction.ReadValue<Vector2>();
//         Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
//         rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

//         if (movementInput != Vector2.zero)
//             playerAnimator.SetForwardMovement(movementInput);
//         else
//             playerAnimator.SetForwardMovement(Vector2.zero);

//         bool isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
//         rb.useGravity = !isGrounded;

//         // Jump
//         if (isSwipeUp && isGrounded)
//         {
//             playerAnimator.Jump();
//             rb.useGravity = true;
//             rb.AddForce(Vector3.up * force_value, ForceMode.Impulse);
//             isSwipeUp = false;
//         }

//         // Slide
//         if (isSwipeDown && isGrounded)
//         {
//             playerAnimator.Slide();
//             isSwipeDown = false;
//         }
//     }

//     private void DetectSwipe()
//     {
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0);

//             switch (touch.phase)
//             {
//                 case TouchPhase.Began:
//                     startTouchPosition = touch.position;
//                     break;

//                 case TouchPhase.Ended:
//                     endTouchPosition = touch.position;
//                     Vector2 swipeDelta = endTouchPosition - startTouchPosition;

//                     if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
//                     {
//                         if (swipeDelta.y > 100f)
//                         {
//                             isSwipeUp = true;
//                         }
//                         else if (swipeDelta.y < -100f)
//                         {
//                             isSwipeDown = true;
//                         }
//                     }
//                     break;
//             }
//         }
//     }
// }
