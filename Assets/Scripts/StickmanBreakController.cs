using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanBreakController : MonoBehaviour
{
    public NewPlayerController newPlayerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<HingeJoint2D>() == null)
        {
            ignoreCollisionWithPlayer();       
        }
    }
    void ignoreCollisionWithPlayer()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
