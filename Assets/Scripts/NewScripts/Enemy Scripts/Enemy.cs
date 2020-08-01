using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float lifes;
    public string ChildSaved;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(lifes <= 0)
        {
            transform.Find(ChildSaved).transform.SetParent(null);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Attack"))
        {
            float magnitude = 1500;

            Vector2 force = transform.position - collision.transform.position;

            force.Normalize();

            rb.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);

            rb.AddForce(force * magnitude);

            lifes--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InstaKiller"))
        {
            lifes = 0;
        }

        if (collision.gameObject.CompareTag("Attack"))
        {
            lifes--;
        }
    }
}
