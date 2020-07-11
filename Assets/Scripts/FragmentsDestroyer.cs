using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Attack")
        {
            float magnitude = 500;

            Vector2 force = transform.position - collision.transform.position;

            force.Normalize();

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            gameObject.GetComponent<Rigidbody2D>().AddForce(force * magnitude);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Main Camera")
        {
            Destroy(gameObject);
        }
    }
}
