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
    bool flipActivator;
    bool HasRb;
    
    bool TimerActivator;
    float ClickTimer = 0.2f;

    private void Start()
    {
        RotationSaver = transform.rotation;
    }
    void Update()
    {
        if(HasRb == false && transform.parent == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
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

        if (Input.GetMouseButtonDown(0) && canGetTheHelmet == true)
        {
            Destroy(GetComponent<Rigidbody2D>());
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            transform.parent = player.transform;
            gameObject.transform.rotation = RotationSaver;
            gameObject.transform.position = fixedPos;
            transform.GetChild(0).gameObject.SetActive(false);
            flipActivator = true;
        }

        if(Input.GetMouseButtonDown(0) && HasRb == false && ClickTimer < 0.2f && ClickTimer > 0)
        {
            Throw();
        }

        if(flipActivator == true)
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canGetTheHelmet = false;
        }
    }

    void Throw()
    {
        gameObject.transform.parent = null;

        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;

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
    }
}
