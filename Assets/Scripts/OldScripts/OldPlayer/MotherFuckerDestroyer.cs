using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherFuckerDestroyer : MonoBehaviour
{
    public Attack attack;
    public float DestroyerTimer = 0.1f;
    Rigidbody2D rb;
    public float force;
    public float FromAboveAttack;
    public float NormalMagnitude;
    private void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody2D>();    
    }

    void Update()
    {

        DestroyerTimer -= Time.deltaTime;


        if(DestroyerTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float magnitude;

            if(transform.position.y > collision.transform.position.y && transform.position.x < collision.transform.position.x + 5 && transform.position.x > collision.transform.position.x - 5)
            {
                magnitude = FromAboveAttack;
            }
            else
            {
                magnitude = NormalMagnitude;
            }

            Vector2 force = transform.position - collision.transform.position;

            force.Normalize();

            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(force * magnitude);
        }
    }
}
