using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public NewPlayerController newPlayerController;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(newPlayerController.IsGoingUp == true || newPlayerController.CanGoDown == true || Input.GetKey("w"))
        {
            ignoreCollisionWithPlayer();
        } 
        else if(newPlayerController.CanGoDown == false)
        {
            enableCollisionWithPlayer();
        } 
    }
    public void enableCollisionWithPlayer()
    {
        Physics2D.IgnoreCollision(newPlayerController.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }
    public void ignoreCollisionWithPlayer()
    {
        Physics2D.IgnoreCollision(newPlayerController.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            newPlayerController.CanGoDown = false;
        }
    }
}
