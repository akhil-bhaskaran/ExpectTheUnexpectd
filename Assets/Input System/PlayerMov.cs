using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    // Start is called before the first frame update

    PlayerMovement controls;
    public Rigidbody2D rb;
    float speed =800;
    public float direction=0;
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
         if (rb == null)
         {
              Debug.LogError("Rigidbody2D not assigned or found on the GameObject.");
        }

        controls = new PlayerMovement();
        controls.Enable();

        controls.Player.Movement.performed += context => {
            direction= context.ReadValue<float>();
        };
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(direction);
        rb.velocity = new Vector2( speed * direction * Time.deltaTime,rb.velocity.y);
    }
}
