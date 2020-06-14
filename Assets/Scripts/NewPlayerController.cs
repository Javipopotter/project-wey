using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject SpawnPoint;

    //floats
    public float AirSpeed;
    public float GroundSpeed;
    float Speed;
    public float jumpForce = 500f;
    public float wallImpulse;
    public float wallUpImpulse;
    public float wallFriction;

    //int
    int cloneVidas;
    public int vidas = 4;


    //Booleans
    bool canJump;
    bool PlayerDirection;
    bool rightWallJump;
    bool leftWallJump;
    public bool vivo = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cloneVidas = vidas;
    }

    void keyboardInput()
    {
        //LateralMovement
        if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(Speed, 0));
            PlayerDirection = true;
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-Speed, 0));
            PlayerDirection = false;
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (canJump == true))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        //WallJump
        if (Input.GetKeyDown(KeyCode.Space) && (rightWallJump == true))
        {
            rightWallJump = false;
            rb.AddForce(new Vector2(-wallImpulse, wallUpImpulse));
        }
        if (Input.GetKeyDown(KeyCode.Space) && (leftWallJump == true))
        {
            leftWallJump = false;
            rb.AddForce(new Vector2(wallImpulse, wallUpImpulse));
        }
    }

    static int suma(int a, int b)
    {
        return a + b;
    }

    // Update is called once per frame
    void Update()
    {
        //Control de Vidas
        if (vivo == false)
        {
            vidas = cloneVidas;
            vivo = true;
            this.gameObject.transform.position = SpawnPoint.transform.position;
        }
        if (vidas <= 0)
        {
            vivo = false;
        }

        print(suma(1, 4));

        keyboardInput();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) {
            case "Enemy":
                vidas--;
                rb.AddForce(new Vector2(0, 700));
            break;
            case "ground":
                canJump = true;
                Speed = GroundSpeed;
            break;
            case "rightWall":
                rightWallJump = true;
                PlayerDirection = false;
                rb.velocity = new Vector2(0f, 0f);
            break;
            case "leftWall":
                leftWallJump = true;
                PlayerDirection = true;
                rb.velocity = new Vector2(0f, 0f);
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
        }

        if (collision.gameObject.tag == "rightWall")
        {
            rightWallJump = false;
            Speed = GroundSpeed;
        }

        if (collision.gameObject.tag == "leftWall")
        {
            leftWallJump = false;
            Speed = GroundSpeed;
        }
    }
}
