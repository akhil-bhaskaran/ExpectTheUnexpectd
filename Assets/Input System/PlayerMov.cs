using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    PlayerMovement controls;
    Rigidbody2D rb;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float jumpForce = 10f; // Jump strength
    [SerializeField] private float jumpCutMultiplier = 0.5f;

    private float direction = 0;
    private float currentVelocity;
    private float velocitySmoothing = 0f;
    // Ground detection fields
    [SerializeField] private Transform groundCheck; // Ground check position
    [SerializeField] private float groundCheckRadius = 0.2f; // Ground check radius
    [SerializeField] private LayerMask groundLayer; // LayerMask for the ground
    private bool isGrounded;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerMovement();
        controls.Enable();

        // Movement input setup
        controls.Player.Movement.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        // Jump input setup
        controls.Player.Jump.performed += OnJump;       // Start jump
        controls.Player.Jump.canceled += OnJumpCancel; // Jump cut
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        SmoothHorizontalMovement();
    }

    // void SmoothHorizontalMovement()
    // {
    //     float targetVelocity = direction * speed;

    //     if (direction != 0)
    //     {
    //         currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, Time.deltaTime * acceleration);
    //         rb.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1); // Flip sprite based on direction
    //     }
    //     else
    //     {
    //         currentVelocity = Mathf.Lerp(currentVelocity, 0, Time.deltaTime * deceleration);
    //     }

    //     rb.velocity = new Vector2(currentVelocity, rb.velocity.y); // Apply horizontal velocity
    // }

    void SmoothHorizontalMovement()
    {
        float targetVelocity = direction * speed;
        currentVelocity = Mathf.SmoothDamp(currentVelocity, targetVelocity, ref velocitySmoothing, acceleration * Time.fixedDeltaTime);

        if (direction != 0)
        {
            rb.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1); // Flip sprite based on direction
        }

        rb.velocity = new Vector2(currentVelocity, rb.velocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump(); // Perform jump when button is pressed
        }
    }

    void OnJumpCancel(InputAction.CallbackContext context)
    {
        if (context.canceled && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier); // Cut jump height when button is released
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a visual for the ground check in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
