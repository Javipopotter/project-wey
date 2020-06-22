using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class NewFixedPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform feetPos;
    public LayerMask whatIsGround;

    //Floats
    public float speed;
    private float moveInput;
    public float checkRadius;
    public float JumpForce;

    //Booleans
    private bool isGrounded;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }


    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new UnityEngine.Vector2(moveInput * speed, rb.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = UnityEngine.Vector2.up * JumpForce;
        }
    }
}
