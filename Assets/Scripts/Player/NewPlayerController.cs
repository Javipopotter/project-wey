using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject SpawnPoint;
    public GameObject EasterEggSpawnpoint;
    public ParticleSystem Blood;
    public ParticleSystem Jparticles;
    public ParticleSystem DeathParticles;
    public SpriteRenderer sr;
    public ParticleSystem Dust;

    //floats
    public float AirSpeed;
    public float GroundSpeed;
    public float Speed;
    public float jumpForce = 500f;
    public float crouchSpeed;
    public float DeathTimer = 300;

    //int
    int cloneVidas;
    public int vidas = 4;
    public int animationTimer = 200; 


    //Booleans
    public bool canJump;
    public bool vivo = true;
    public bool IsGoingUp;
    public bool CanGoDown;
    public bool touchingGround;
    public bool AnimationActivation;
    bool Invulnerability;
    bool flagcrouch;
    bool QCrouched;
    public bool PlayerDirection;
    bool deathTimerActivator;
    bool PlayerBlockMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cloneVidas = vidas;
        sr = gameObject.GetComponent<SpriteRenderer>();
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
            if (Input.GetKey("d") && PlayerBlockMovement == false)
        {
            rb.AddForce(new Vector2(Speed * Time.deltaTime, 0));
            PlayerDirection = true;

            if (touchingGround == true)
            {
                Speed = GroundSpeed;
            }
        }
        if (Input.GetKey("a") && PlayerBlockMovement == false)
        {
            rb.AddForce(new Vector2(-Speed * Time.deltaTime, 0));
            PlayerDirection = false;

            if (touchingGround == true)
            {
                Speed = GroundSpeed;
            }
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (canJump == true) && PlayerBlockMovement == false)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            touchingGround = false;
            gameObject.GetComponent<Animator>().SetBool("HasJumped", true);
            JumpPlay();
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
        if(rb.velocity.y > 1)
        {
            IsGoingUp = true;
        }
        else if (rb.velocity.y <= 1)
        {
            IsGoingUp = false;
        }

        //Control de Vidas
        if (vivo == false)
        {
            PlayerBlockMovement = true;
            DeathParticles.Play();
            sr.enabled = false;
            deathTimerActivator = true;
            if(deathTimerActivator == true)
            { 
            DeathTimer--;
            }
            if(DeathTimer <= 0)
            {
                DeathPlay();
                deathTimerActivator = false;
                DeathTimer = 300;
            }
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
                    BloodPlay();
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

    void BloodPlay()
    {
        Blood.Play();
    }

    void JumpPlay()
    {
        Jparticles.Play();
    }

    void DeathPlay()
    {
        vidas = cloneVidas;
        vivo = true;
        this.gameObject.transform.position = SpawnPoint.transform.position;
        DeathParticles.Play();
        sr.enabled = true;
        PlayerBlockMovement = false;
    }
}
