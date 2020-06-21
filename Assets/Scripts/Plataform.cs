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
        /*  if(newPlayerController.IsGoingUp == true)
          {
              ignoreCollisionWithPlayer();
          }
          */

        if(newPlayerController.IsGoingUp == true || newPlayerController.CanGoDown == true)
        {
            ignoreCollisionWithPlayer();
        } 
        else if(newPlayerController.CanGoDown == false)
        {
            enableCollisionWithPlayer();
        }
    }
    void enableCollisionWithPlayer()
    {
        Physics2D.IgnoreCollision(newPlayerController.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }
    void ignoreCollisionWithPlayer()
    {
        Physics2D.IgnoreCollision(newPlayerController.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
