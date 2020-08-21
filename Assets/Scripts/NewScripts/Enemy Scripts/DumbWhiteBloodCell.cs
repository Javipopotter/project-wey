using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbWhiteBloodCell : MonoBehaviour
{
    public float lifes;
    float PlayerDirection;
    Rigidbody2D rb;
    public GameObject Player;
    public NewPlayerController newPlayerController;
    public float EnemyVel;
    public bool lootsAnything;
    public string ChildSaved;
    public float RayLenght;
    bool canJump;
    public float XScaleSaver;
    float DamageTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        XScaleSaver = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDirection = gameObject.transform.position.x - Player.transform.position.x;
        
        if(VectorNormalizer(PlayerDirection) == 1)
        {
            rb.AddForce(new Vector2(EnemyVel * Time.deltaTime, 0));
            transform.localScale = new Vector2(-XScaleSaver, transform.localScale.y);
        }
        else
        {
            rb.AddForce(new Vector2(-EnemyVel * Time.deltaTime, 0));
            transform.localScale = new Vector2(XScaleSaver, transform.localScale.y);
        }

        if(lifes <= 0)
        {
            Death();
        }

        Vector2 PlayerXDirection;
        if(PlayerDirection > 0)
        {
            PlayerXDirection = Vector2.left;
        }
        else
        {
            PlayerXDirection = Vector2.right;
        }
     
        RaycastHit2D hit = Physics2D.Raycast(transform.position, PlayerXDirection, RayLenght);

        if((!hit.collider == gameObject.CompareTag("ground") || !hit.collider == gameObject.CompareTag("AnotherBrickInTheWall")) && canJump == true)
        {
            rb.AddForce(new Vector2( 0, 1700));
            canJump = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && newPlayerController.Invulnerability == false)
        {
            newPlayerController.enemyCollision = Player.transform.position - transform.position;
            newPlayerController.LooseLife();
            newPlayerController.vidas--;
        }
        DamageTimer -= Time.deltaTime;
        if (DamageTimer <= 0)
        {
            if (collision.gameObject.CompareTag("Attack"))
            {
                DamageTimer = 0.3f;

                float magnitude = 1500;

                Vector2 force = transform.position - collision.transform.position;

                force.Normalize();

                rb.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);

                rb.AddForce(force * magnitude);

                lifes--;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("InstaKiller"))
        {
            lifes = 0;
        }

        if(collision.gameObject.CompareTag("ground"))
        {
            canJump = true;
        }

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

    void Death()
    {
        if(lootsAnything)
        {
            transform.Find(ChildSaved).transform.SetParent(null);
        }
        Destroy(gameObject);
    }
}
