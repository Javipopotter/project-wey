using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarre : MonoBehaviour
{
    public NewPlayerController newPlayerController;
    Rigidbody2D rb;
    public float Ymovement;
    public float WallJumpForce;
    bool IsGraving;
    bool CanGrab;
    bool CanWallJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(Input.GetKeyDown(KeyCode.Space) && CanWallJump == true)
        {
            CanGrab = false;
            rb.AddForce(new Vector2(-WallJumpForce, 0));
        }

        if(CanGrab == true && Input.GetMouseButton(1))
        {
            IsGraving = true;
            Grab();                      
        }
        else
        {
            IsGraving = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.tag == "AnotherBrickInTheWall"))
        {
            CanGrab = true;
            CanWallJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if((collision.gameObject.tag == "AnotherBrickInTheWall"))
        {
            CanGrab = false;
            CanWallJump = false;
        }
    }
}
