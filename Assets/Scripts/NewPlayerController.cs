using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject SpawnPoint;
    public GameObject EasterEggSpawnpoint;

    //floats
    public float AirSpeed;
    public float GroundSpeed;
    float Speed;
    public float jumpForce = 500f;
    public float wallImpulse;
    public float wallUpImpulse;
    public float wallFriction;

    //int
    int cloneVidas;
    public int vidas = 4;
    public int animationTimer = 200; 


    //Booleans
    bool canJump;
    bool rightWallJump;
    bool leftWallJump;
    public bool vivo = true;
    public bool IsGoingUp;
    public bool CanGoDown;
    public bool touchingGround;
    public bool AnimationActivation;
    bool Invulnerability; 

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cloneVidas = vidas;
    }

    void keyboardInput()
    {       
        if(Input.GetKey("s"))
        {
            CanGoDown = true;
            IsGoingUp = false;
        } 
        else if(Input.GetKeyUp("s"))
        {
            CanGoDown = false;
        }
            //LateralMovement
            if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(Speed, 0));
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-Speed, 0));
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (canJump == true))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        //WallJump
        if (Input.GetKeyDown(KeyCode.Space) && (rightWallJump == true) && (touchingGround == false))
        {
            rightWallJump = false;
            rb.AddForce(new Vector2(-wallImpulse , wallUpImpulse));
        }
        if (Input.GetKeyDown(KeyCode.Space) && (leftWallJump == true) && (touchingGround == false))
        {
            leftWallJump = false;
            rb.AddForce(new Vector2(wallImpulse , wallUpImpulse));
        }
    }

    /*static int suma(int a, int b)
    {
        return a + b;
    }*/

    // Update is called once per frame
    void Update()
    {
        if(AnimationActivation == true)
        {
            animationTimer--;
        }

        if(animationTimer <= 0)
        {
            AnimationActivation = false;
            gameObject.GetComponent<Animator>().SetBool("GettingDamage", false);
            Invulnerability = false;
            animationTimer = 200;          
        }
        if(rb.velocity.y > 0)
        {
            IsGoingUp = true;
        }
        else if (rb.velocity.y <= 0)
        {
            IsGoingUp = false;
        }
        //Control de Vidas
        if (vivo == false)
        {
            vidas = cloneVidas;
            vivo = true;
            this.gameObject.transform.position = SpawnPoint.transform.position;
        }
        if (vidas <= 0)
        {
            vivo = false;
        }

       // print(suma(1, 4));

        keyboardInput();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        switch (collision.gameObject.tag) {
            case "Enemy":
                if (Invulnerability == false)
                {
                    gameObject.GetComponent<Animator>().SetBool("GettingDamage", true);
                    vidas--;
                    AnimationActivation = true;
                    rb.AddForce(new Vector2(0, 700));
                    Invulnerability = true;
                }
                break;
            case "ground":
                canJump = true;
                Speed = GroundSpeed;
                touchingGround = true;
                break;
            case "rightWall":
                rightWallJump = true;
            break;
            case "leftWall":
                leftWallJump = true;              
                break;
        }
        if (collision.gameObject.name == "KillZone")
        {
            vivo = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            canJump = false;
            Speed = AirSpeed;
            touchingGround = false;
        }

        if (collision.gameObject.tag == "rightWall")
        {
            rightWallJump = false;
            Speed = GroundSpeed;
        }

        if (collision.gameObject.tag == "leftWall")
        {
            leftWallJump = false;
            Speed = GroundSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EasterEgg Zone")
        {
            this.gameObject.transform.position = EasterEggSpawnpoint.transform.position;
        }
    }
}
