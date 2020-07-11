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

    //floats
    public float AirSpeed;
    public float GroundSpeed;
    public float Speed;
    public float jumpForce = 500f;
    public float crouchSpeed;
    public float DeathTimer = 300;
    public float animationTimer = 2;
    float CAnimationTimer;

    //int
    int cloneVidas;
    public int vidas = 4;


    //Booleans
    public bool canJump;
    public bool vivo = true;
    public bool IsGoingUp;
    public bool CanGoDown;
    public bool AnimationActivation;
    bool Invulnerability;
    bool flagcrouch;
    public bool PlayerDirection;
    bool deathTimerActivator;
    bool PlayerBlockMovement;

    // Start is called before the first frame update
    void Start()
    {
        Blood.Stop();
        Jparticles.Stop();
        rb = gameObject.GetComponent<Rigidbody2D>();
        cloneVidas = vidas;
        CAnimationTimer = animationTimer;
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
                Speed = crouchSpeed;
            } 
        } 
        else if(Input.GetKeyUp("s"))
        {
            CanGoDown = false;
            gameObject.GetComponent<Animator>().SetBool("IsCrouched", false);
            Speed = GroundSpeed;
        }

            //LateralMovement
            if (Input.GetKey("d") && PlayerBlockMovement == false)
        {
            rb.AddForce(new Vector2(Speed * Time.deltaTime, 0));
            PlayerDirection = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey("a") && PlayerBlockMovement == false)
        {
            rb.AddForce(new Vector2(-Speed * Time.deltaTime, 0));
            PlayerDirection = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (canJump == true) && PlayerBlockMovement == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce));
            canJump = false;
            Speed = AirSpeed;
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
        if(AnimationActivation == true)
        {
            animationTimer -= Time.deltaTime;
        }

        if(animationTimer <= 0)
        {
            AnimationActivation = false;
            gameObject.GetComponent<Animator>().SetBool("GettingDamage", false);
            Invulnerability = false;
            animationTimer = CAnimationTimer;          
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
            DeathTimer -= Time.deltaTime;
            }
            if(DeathTimer <= 0)
            {
                DeathPlay();
                deathTimerActivator = false;
                DeathTimer = 1;
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
                flagcrouch = true;
                gameObject.GetComponent<Animator>().SetBool("HasJumped", false);
                break;           
        }
        if (collision.gameObject.tag == "InstaKiller")
        {
            vidas = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
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
