using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject DestroyedObject;
    public SpriteRenderer[] Srend;

    private void Start()
    {
        Srend = DestroyedObject.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.name == "piecex")
        {
            foreach(SpriteRenderer col in Srend)
            {
                col.color = gameObject.GetComponent<SpriteRenderer>().color;
            }

            Instantiate(DestroyedObject, transform.position, transform.rotation);
             Destroy(gameObject);
        }
    }
}
