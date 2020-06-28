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
    public Rigidbody2D rb;
    bool flashVelocity = false;
    bool CanJump;
    bool CanDoubleJump;
    public float ScJumpForce = 500;
    bool InTheAir;
    public float AirSpeed = 700f;
    float PlayerSpeedClone;
    public int vidas = 4;

    public GameObject KillZone;

    public float DashUpForce = 1000f;
    public float DashLateralForce = 1000f;
    public int NumberOfDashes = 1;
    
    public bool vivo = true;
    public GameObject SpawnPoint;
    int cloneVidas;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    bool playerDirection;

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
            playerDirection = false;

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
            playerDirection = true;


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

        //dash
        if(Input.GetKeyDown("w") && (playerDirection == true) && (NumberOfDashes > 0))
        {
            NumberOfDashes--;
            rb.AddForce(new Vector2(DashLateralForce, DashUpForce));
        }
        if (Input.GetKeyDown("w") && (playerDirection == false) && (NumberOfDashes > 0))
        {
            rb.AddForce(new Vector2(-DashLateralForce, DashUpForce));
            NumberOfDashes--;
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

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
       /* else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        */
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
        if(collision.gameObject.name == "KillZone")
        {
            vivo = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            vidas--;
        }

        if (collision.gameObject.tag == "rightWall")
        {
            rightWallJump = true;
            NumberOfDashes = 1;
            playerDirection = false;
        } 


        if (collision.gameObject.tag == "leftWall")
        {
            leftWallJump = true;
            NumberOfDashes = 1;
            playerDirection = true;
        }
       

        if (collision.transform.tag == "ground")
        {
            CanJump = true;
            NumberOfDashes = 1;
        }
        if (collision.transform.tag == "ground")
        {
            InTheAir = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "rightWall")
        {
            playerDirection = false;
        }

        if (collision.gameObject.tag == "leftWall")
        {
            playerDirection = true;
        }
        if (collision.gameObject.tag == "leftWall")
        {
            playerDirection = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            InTheAir = true;
        }
        if (collision.gameObject.tag == "rightWall")
        {
            rightWallJump = false;
        }
        if (collision.gameObject.tag == "leftWall")
        {
            leftWallJump = false;          
        }
    }
}
