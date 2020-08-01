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
    public float SlipperVel;
    public bool SlipperFlag;

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

        if (GameObject.Find("Player").GetComponent<NewPlayerController>().canJump == true)
        {
            CanGrab = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && CanGrab == true && newPlayerController.canJump == false)
        {
            CanGrab = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(XWallJumpForce, YWallJumpForce));
            SlipperFlag = false;

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("AnotherBrickInTheWall") && rb.velocity.y <= 0)
        {
        //   an.SetBool("IsGraving", true);
            CanGrab = true;
            if (SlipperFlag == true && CanGrab == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, -SlipperVel);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("AnotherBrickInTheWall"))
        {
            //   an.SetBool("IsGraving", false);
            SlipperFlag = true;
            CanGrab = false;
        }
    }
}
