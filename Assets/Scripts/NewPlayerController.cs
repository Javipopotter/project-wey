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
    public float Speed;
    public float jumpForce = 500f;
    public float crouchSpeed;

    //int
    int cloneVidas;
    public int vidas = 4;
    public int animationTimer = 200; 


    //Booleans
    bool canJump;
    public bool vivo = true;
    public bool IsGoingUp;
    public bool CanGoDown;
    public bool touchingGround;
    public bool AnimationActivation;
    bool Invulnerability;
    bool flagcrouch;
    bool QCrouched;
    public bool PlayerDirection;

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

            if (flagcrouch == true)
            {
                gameObject.GetComponent<Animator>().SetBool("IsCrouched", true);
                QCrouched = true;
            } 
        } 
        else if(Input.GetKeyUp("s"))
        {
            CanGoDown = false;
            gameObject.GetComponent<Animator>().SetBool("IsCrouched", false);
            QCrouched = false;
        }
            //LateralMovement
            if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(Speed * Time.deltaTime, 0));
            PlayerDirection = true;

            if (touchingGround == true)
            {
                Speed = GroundSpeed;
            }
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-Speed * Time.deltaTime, 0));
            PlayerDirection = false;

            if (touchingGround == true)
            {
                Speed = GroundSpeed;
            }
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (canJump == true))
        {
            rb.AddForce(new Vector2(0, jumpForce));
            touchingGround = false;
            gameObject.GetComponent<Animator>().SetBool("HasJumped", true);          
        }    
    }

    /*static int suma(int a, int b)
    {
        return a + b;
    }*/

    // Update is called once per frame
    void Update()
    {
        if(QCrouched == true)
        {
            Speed = crouchSpeed;
        }

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
                flagcrouch = true;
                gameObject.GetComponent<Animator>().SetBool("HasJumped", false);
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
            flagcrouch = false;
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
