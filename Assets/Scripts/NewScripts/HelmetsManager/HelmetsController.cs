using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetsController : MonoBehaviour
{
    public GameObject player;
    public float fixX;
    public float fixY;
    Vector2 fixedPos;
    bool canGetTheHelmet;
    Quaternion RotationSaver;
    bool HasRb;
    bool HasHelmet;
    public bool GetFromBottom;
    Vector2 ScaleSaver;
    public float ScaleFixer;
    
    bool TimerActivator;
    float ClickTimer = 0.2f;

    bool IgnoreTimerActivator;
    float IgnoreTimer = 0.5f;

    private void Start()
    {
        RotationSaver = transform.rotation;
        ScaleSaver = gameObject.transform.localScale * ScaleFixer;
    }
    void Update()
    {
        if(HasRb == false && transform.parent == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if(gameObject.GetComponent<Rigidbody2D>() == null)
        {
            HasRb = false;
        }
        else
        {
            HasRb = true;
        }

        fixedPos = new Vector2(player.transform.position.x, player.transform.position.y) + new Vector2(fixX, fixY);

        if ((Input.GetMouseButtonDown(0) && canGetTheHelmet == true) || GetFromBottom == true)
        {
            GetFromBottom = false;
            Destroy(GetComponent<Rigidbody2D>());
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.parent = player.transform;
            gameObject.transform.rotation = RotationSaver;
            gameObject.transform.position = fixedPos;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.localScale = ScaleSaver;
        }

        if(Input.GetMouseButtonDown(0) && HasHelmet == true && ClickTimer < 0.2f && ClickTimer > 0)
        {
            Throw();
        }

        if(HasHelmet == true)
        {
            if (GameObject.Find("Player").GetComponent<NewPlayerController>().PlayerDirection == false)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if(gameObject.transform.parent == player.transform)
        {
            HasHelmet = true;
        }
        else
        {
            HasHelmet = false;
        }

        Timer();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canGetTheHelmet = true;
        }

        if(collision.gameObject.tag == "InstaKiller")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            canGetTheHelmet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            canGetTheHelmet = false;
        }
    }

    void Throw()
    {
        gameObject.transform.parent = null;

        transform.GetChild(1).gameObject.SetActive(false);
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        IgnoreTimerActivator = true;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float magnitude = 3500;

        Vector2 force = transform.position - mouse;

        force.Normalize();

        gameObject.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
    }

    void Timer()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TimerActivator = true;
        }

        if(TimerActivator == true)
        {
            ClickTimer -= Time.deltaTime;
            if(ClickTimer <= 0)
            {
                TimerActivator = false;
                ClickTimer = 0.2f;
            }
        }

        if(IgnoreTimerActivator == true)
        {
            IgnoreTimer -= Time.deltaTime;
            if (IgnoreTimer <= 0)
            {
                IgnoreTimerActivator = false;
                IgnoreTimer = 0.5f;
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
