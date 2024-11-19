using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicMovement : MonoBehaviour
{

    public float moveSpeed;
    Rigidbody2D rb;
    private void Awake()
    {


        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 5;
        moveSpeed = 15f;

    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rb.transform.eulerAngles = new Vector3(0, 0, 0);

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3((float)1, (float)1, 0);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3((float)-1, (float)1, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        }


    }

}
