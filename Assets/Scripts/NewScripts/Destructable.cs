using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject DestroyedObject;
    public SpriteRenderer[] Srend;
    bool InstanceOneTime = true;
    public bool CantBeDestroyedByThePlayer;
    string Player;
    private void Start()
    {
        Srend = DestroyedObject.GetComponentsInChildren<SpriteRenderer>();

        if(CantBeDestroyedByThePlayer == false)
        {
            Player = "Player";
        }
        else
        {
            Player = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Player || collision.gameObject.name == "piecex")
        {
            foreach (SpriteRenderer col in Srend)
            {
                col.color = gameObject.GetComponent<SpriteRenderer>().color;
            }

            if (InstanceOneTime == true)
            {
                InstanceOneTime = false;
                Instantiate(DestroyedObject, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            foreach (SpriteRenderer col in Srend)
            {
                col.color = gameObject.GetComponent<SpriteRenderer>().color;
            }

            if (InstanceOneTime == true)
            {
                InstanceOneTime = false;
                Instantiate(DestroyedObject, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
