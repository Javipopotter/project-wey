using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed = 10.0f;
    public float JumpForce = 10.0f;
    private bool GettingKey;
    private Rigidbody2D rb;
    bool flashVelocity = false;
    bool CanJump;
    
    public bool leftWallJump;
    public bool rightWallJump;
    public int WallImpulse = 700;
    public int WallUpImpulse = 1000; // un comentario extra

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()

    {
        
        GettingKey = false;
       
        //Este es pa ir a la izquierda
        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-PlayerSpeed * Time.deltaTime, 0));
            GettingKey = true;
            flashVelocity = true;
        } 
       
        //Este es pa ir a la derecha
        if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(PlayerSpeed * Time.deltaTime, 0));
            GettingKey = true;
            flashVelocity = true;
        }
        //Este es pa saltar
        if ((Input.GetKeyDown(KeyCode.Space)) && (CanJump == true))
        {
           CanJump = false;
            rb.AddForce(new Vector2( 0, JumpForce));
        }
        

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
        }

        if ((((Input.GetKeyDown(KeyCode.Space) && (leftWallJump == true)))))
        {
            leftWallJump = false;
            rb.AddForce(new Vector2(WallImpulse, WallUpImpulse));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "rightWall")
        {
            rightWallJump = true;
        } 
        else
        {
            rightWallJump = false;
        }


        if (collision.gameObject.tag == "leftWall")
        {
            leftWallJump = true;
        }
        else
        {
            leftWallJump = false;
        }

        if (collision.transform.tag == "ground")
        {
            CanJump = true;
           
        }

        


        /* void OnCollisionEnter2D (Collision2D collison)
        {
            if (collison.transform.tag == "wall")
            {
                wallJump = true;
                Debug.Log("Puedes ejecutar el wallJump");
            }
            else
            {
                wallJump = false;
            }
        }  */

    }
}
