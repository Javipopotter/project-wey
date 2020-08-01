using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbWhiteBloodCell : MonoBehaviour
{
    public float lifes;
    float PlayerDirection;
    Rigidbody2D rb;
    public GameObject Player;
    public float EnemyVel;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDirection = gameObject.transform.position.x - Player.transform.position.x;
        
        if(VectorNormalizer(PlayerDirection) == 1)
        {
            rb.AddForce(new Vector2(EnemyVel * Time.deltaTime, 0));
        }
        else
        {
            rb.AddForce(new Vector2(-EnemyVel * Time.deltaTime, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
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
    float VectorNormalizer(float Vector)
    {
        if(Vector <= 0)
        {
            Vector = 1;
        }
        else
        {
            Vector = -1;
        }
        return Vector;
    }
}
