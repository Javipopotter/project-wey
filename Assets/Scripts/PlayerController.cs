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
    bool CanDoubleJump;
    public float ScJumpForce = 500;
    bool InTheAir;
    public float AirSpeed = 700f;
    float PlayerSpeedClone;
    public int vidas = 4;
    bool vivo = true;
    public GameObject SpawnPoint;
    int cloneVidas;
    
    public bool leftWallJump;
    public bool rightWallJump;
    public int WallImpulse = 700;
    public int WallUpImpulse = 1000; // un comentario extra

    // Start is called before the first frame update
    void Start()
    {
        cloneVidas = vidas;
        PlayerSpeedClone = PlayerSpeed;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()

    {
        if(vivo == false)
        {
            vidas = cloneVidas;
            vivo = true;
            this.gameObject.transform.position = SpawnPoint.transform.position;
        }
        if(vidas <= 0)
        {
            vivo = false;
        }
        
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

        if(InTheAir == true)
        {
            PlayerSpeed = AirSpeed;
        }
        else
        {
            PlayerSpeed = PlayerSpeedClone;
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
            CanDoubleJump = true;
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
        if (collision.gameObject.tag == "Enemy")
        {
            vidas--;
            rb.AddForce(new Vector2(0,700));
        }

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
        if (collision.transform.tag == "ground")
        {
            InTheAir = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            InTheAir = true;
        }      
    }
}
