using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    bool CanGetCaught;
    bool IsCaught;
    public float GravityScale;
    public GameObject InteractionZone;
    Rigidbody2D rb;
    public bool BlockScale;
    Vector3 ScaleSaver;
    public float[] Scale;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = GravityScale;
        ScaleSaver = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && IsCaught == true || GameObject.Find("Player").GetComponent<NewPlayerController>().vivo == false)
        {
            IsCaught = false;
            gameObject.transform.parent = null;
            PropertyManage();
        }
        else if (Input.GetMouseButtonDown(1) && CanGetCaught == true)
        {
            gameObject.transform.position = GameObject.Find("MouseChaser").transform.position;
         //   Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
            IsCaught = true;
            gameObject.transform.parent = InteractionZone.transform;
            PropertyManage();
        }

        if (Input.GetMouseButtonDown(0) && IsCaught == true)
        {
            Throw();
        }

        ScaleController();      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 Player;
            Player = transform.position - GameObject.Find("Player").transform.position;
            float DasForce = 5500;
            Player.Normalize();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Player * DasForce);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CanGetCaught = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CanGetCaught = false;
        }
    }

    void PropertyManage()
    {
        if (gameObject.transform.parent == null)
        {
         //   Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = GravityScale;
        }
        else
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
         //   Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void Throw()
    {
        IsCaught = false;

        gameObject.transform.parent = null;

        PropertyManage();

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float magnitude = 3500;

        Vector2 force = transform.position - mouse;

        force.Normalize();

        gameObject.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);

        gameObject.GetComponent<Rigidbody2D>().gravityScale = GravityScale;
    }

    void ScaleController()
    {
        if (BlockScale == false)
        {
            if (gameObject.transform.localScale.x > Scale[0])
            {
                transform.localScale = new Vector2(Scale[0], gameObject.transform.localScale.y);
            }
            else if (gameObject.transform.localScale.x < Scale[1])
            {
                transform.localScale = new Vector2(Scale[1], gameObject.transform.localScale.y);
            }

            if (gameObject.transform.localScale.y > Scale[0])
            {
                transform.localScale = new Vector2(gameObject.transform.localScale.x, Scale[0]);
            }
            else if (gameObject.transform.localScale.y < Scale[1])
            {
                transform.localScale = new Vector2(gameObject.transform.localScale.x, Scale[1]);
            }
        }
        else
        {
            transform.localScale = ScaleSaver;
        }
    }

}
