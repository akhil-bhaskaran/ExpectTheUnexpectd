using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMov : MonoBehaviour
{
    // Start is called before the first frame update

    PlayerMovement controls;

     Rigidbody2D rb;
    float speed =300;
    public float direction=0;
    float jumpspeed = 6f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not assigned or found on the GameObject.");
        }
        controls = new PlayerMovement();
        controls.Enable();

        controls.Player.Movement.performed += ctx => {
            direction = ctx.ReadValue<float>();
        };
        controls.Player.Jump.performed += ctx => Jump();
    }
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2( speed * direction * Time.deltaTime,rb.velocity.y);
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
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
