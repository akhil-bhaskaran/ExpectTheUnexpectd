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
    private float jumpVelocity;

    // Ground detection fields
    [SerializeField] private Transform groundCheck; // Ground check position
    [SerializeField] private float groundCheckRadius = 0.2f; // Ground check radius
    

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
        controls.Player.Jump.performed += ctx => OnJump(ctx);
        controls.Player.Jump.canceled += ctx => OnJump(ctx);  // Handling jump cut on button release
    }

    void Update()
    {
        SmoothHorizontalMovement();
    }

    void SmoothHorizontalMovement()
    {
        float targetVelocity = direction * speed;
        if (direction != 0)
        {
            currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, Time.deltaTime * acceleration);
            rb.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1); // Flip sprite based on direction
        }
        else
        {
            currentVelocity = Mathf.Lerp(currentVelocity, 0, Time.deltaTime * deceleration);
        }
        rb.velocity = new Vector2(currentVelocity, rb.velocity.y); // Apply horizontal velocity
    }

    void Jump()
    {
       
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump(); // Perform jump when pressed
        }
        else if (context.canceled)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier); // Jump cut when released
        }
    }

   

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }
}
