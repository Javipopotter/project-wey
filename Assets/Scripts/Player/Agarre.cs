using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarre : MonoBehaviour
{
    public Animator an;
    public NewPlayerController newPlayerController;
    Rigidbody2D rb;
    public float Ymovement;
    public float XWallJumpForce;
    float NXWallJumpForce;
    public float YWallJumpForce;
    public bool IsGraving;
    public bool CanGrab;

    // Start is called before the first frame update
    void Start()
    {
        NXWallJumpForce = -XWallJumpForce;
        an = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WJumpDirection();

        if(Input.GetKeyDown(KeyCode.Space) && CanGrab == true)
        {
            CanGrab = false;
            rb.AddForce(new Vector2(XWallJumpForce * Time.deltaTime, YWallJumpForce));
            print("WallJump");
        }

        if(CanGrab == true && Input.GetMouseButton(1))
        {
            an.SetBool("IsGraving", true);
            IsGraving = true;
            Grab();                      
        }
        else
        {
            IsGraving = false;
            an.SetBool("IsGraving", false);
        }

        if(IsGraving == true && Input.GetKey("w"))
        {
            Upmovement();
        }

        if(IsGraving == true && Input.GetKey("s"))
        {
            Downmovement();
        } 
    }

    void Upmovement()
    {
        rb.MovePosition (rb.position + new Vector2(0, Ymovement));
    }

    void Downmovement()
    {
        rb.MovePosition(rb.position + new Vector2(0, -Ymovement));
    }

    void Grab()
    {
        rb.velocity = new Vector2(0, 0);
    }

    void WJumpDirection()
    {
        switch (newPlayerController.PlayerDirection)
        {
            case true:
                XWallJumpForce = NXWallJumpForce;
                break;

            case false:
                XWallJumpForce = -NXWallJumpForce;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.tag == "AnotherBrickInTheWall"))
        {        
            CanGrab = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if((collision.gameObject.tag == "AnotherBrickInTheWall"))
        {
            CanGrab = false;
        }
    }
}
