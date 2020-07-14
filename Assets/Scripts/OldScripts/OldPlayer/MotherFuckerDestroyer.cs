using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherFuckerDestroyer : MonoBehaviour
{
    public Attack attack;
    public float DestroyerTimer = 0.1f;
    Rigidbody2D rb;
    public float force;
    private void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody2D>();    
    }

    void Update()
    {
        if(attack.CodeActivator == false)
        {
            DestroyerTimer -= Time.deltaTime;
        }

        if(DestroyerTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            float magnitude = 750;

            Vector2 force = transform.position - collision.transform.position;

            force.Normalize();

            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(force * magnitude);
        }
    }
}
