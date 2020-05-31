using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public float PlayerSpeed = 10.0f;
    public float JumpForce = 10.0f;
    private bool GettingKey;
    private Rigidbody2D rb;
    bool flashVelocity = false;
    bool CanJump;
    public int timer = 120;
    public int timerClone;
    bool timerStop;
    bool JumpStop;
    bool CanDoubleJump;
    public float ScJumpForce = 500;
    
    public bool leftWallJump;
    public bool rightWallJump;
    public int WallImpulse = 700;
    public int WallUpImpulse = 1000; // un comentario extra

    // Start is called before the first frame update
    void Start()
    {
        timerClone = timer;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()

    {
        
        GettingKey = false;

        if(Input.GetKey("s"))
        {
            rb.AddForce(new Vector2(0, -10));
        }
       
        //Este es pa ir a la izquierda
        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-PlayerSpeed * Time.deltaTime, 0));
            GettingKey = true;
            flashVelocity = true;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(new Vector2(-PlayerSpeed * Time.deltaTime, 0));
            }
        } 
       
        //Este es pa ir a la derecha
        if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(PlayerSpeed * Time.deltaTime, 0));
            GettingKey = true;
            flashVelocity = true;


            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(new Vector2(PlayerSpeed * Time.deltaTime, 0));
            }
        }

        //Este es pa doblesaltar
        if (Input.GetKeyDown(KeyCode.Space) && (CanDoubleJump == true))
        {
            CanDoubleJump = false;
            rb.AddForce(new Vector2(0, ScJumpForce));
        }

        //Este es pa saltar
        if ((Input.GetKeyDown(KeyCode.Space)) && (CanJump == true))
        {           
            CanJump = false;
            rb.AddForce(new Vector2(0, JumpForce));
            JumpStop = true;
            CanDoubleJump = true;
        }      

        if ((Input.GetKeyUp(KeyCode.Space)) && (timerStop != true) && (JumpStop == true))
        {
            JumpStop = false;
            rb.AddForce(new Vector2(0, -JumpForce));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            timer--;
        }
        if ((timer <= 0) && (CanJump == true))
        {
            timer = timerClone;
        }
        if(timer <= 0)
        {
            timerStop = true;
        } 
        else
        {
            timerStop = false;
        }

        // Este es para detener en seco al personaje
        if (((GettingKey == false) && (flashVelocity == true)) && (CanJump == true))
        {
            flashVelocity = false;
            rb.velocity = Vector2.zero;
        }

       // Este es pa hacer WallJump
        if ((((Input.GetKeyDown(KeyCode.Space) && (rightWallJump == true)))))
        {
            rightWallJump = false;
            rb.AddForce(new Vector2(-WallImpulse, WallUpImpulse));
            CanDoubleJump = true;
        }       
        if ((((Input.GetKeyDown(KeyCode.Space) && (leftWallJump == true)))))
        {
            leftWallJump = false;
            rb.AddForce(new Vector2(WallImpulse, WallUpImpulse));
            CanDoubleJump = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "rightWall")
        {
            rightWallJump = true;
            timer = timerClone;
        } 
        else
        {
            rightWallJump = false;
        }


        if (collision.gameObject.tag == "leftWall")
        {
            leftWallJump = true;
            timer = timerClone;
        }
        else
        {
            leftWallJump = false;
        }

        if (collision.transform.tag == "ground")
        {
            CanJump = true;
            timer = timerClone;
        }                             
    }
}
