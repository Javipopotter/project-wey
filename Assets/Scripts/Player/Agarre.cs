using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarre : MonoBehaviour
{
    public Animator an;
    public NewPlayerController newPlayerController;
    Rigidbody2D rb;
    public float XWallJumpForce;
    public float NXWallJumpForce;
    public float YWallJumpForce;
    public bool CanGrab;
    bool flagWJump;

    // Start is called before the first frame update
    void Start()
    {
        NXWallJumpForce = -XWallJumpForce;
        an = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        WJumpDirection();


        if(Input.GetKeyDown(KeyCode.Space) && CanGrab == true && newPlayerController.canJump == false)
        {
            CanGrab = false;
            rb.AddForce(new Vector2(XWallJumpForce, YWallJumpForce));
            flagWJump = true;

            switch(newPlayerController.PlayerDirection)
            {
                case true:
                    newPlayerController.PlayerDirection = false;
                    break;
                case false:
                    newPlayerController.PlayerDirection = true;
                    break;
            }
        }

        if((CanGrab == true) && Input.GetMouseButton(1) && flagWJump == false)
        {
            an.SetBool("IsGraving", true);
            Grab();                      
        }
        else
        {
            an.SetBool("IsGraving", false);
        }
    }

    void Grab()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
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
            flagWJump = false;
            newPlayerController.canJump = false;   
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
