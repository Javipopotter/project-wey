using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject Attack;
    public GameObject DestroyedObject;
    public SpriteRenderer[] Srend;
    bool InstanceOneTime = true;
    public bool DestroysACEnemies;
    public bool DestroysAAtkCollision;
    public bool DestroysAtContact;
    public bool DestroyedByOtherFragments;
    private void Start()
    {
        Srend = DestroyedObject.GetComponentsInChildren<SpriteRenderer>();   
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") && DestroysAtContact) || collision.gameObject.name == "piecex" && DestroyedByOtherFragments || (collision.gameObject.CompareTag("Enemy") && DestroysACEnemies))
        {
            foreach (SpriteRenderer col in Srend)
            {
                col.color = gameObject.GetComponent<SpriteRenderer>().color;
            }

            if (InstanceOneTime == true)
            {
                InstanceOneTime = false;
                if(DestroysACEnemies == true)
                {
                    Instantiate(Attack, transform.position, transform.rotation);
                }
                Instantiate(DestroyedObject, transform.position, transform.rotation);
                transform.DetachChildren();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") && DestroysAAtkCollision)
        {
            foreach (SpriteRenderer col in Srend)
            {
                col.color = gameObject.GetComponent<SpriteRenderer>().color;
            }

            if (InstanceOneTime == true)
            {
                InstanceOneTime = false;
                Instantiate(DestroyedObject, transform.position, transform.rotation);
                transform.DetachChildren();
                Destroy(gameObject);
            }
        }
    }
}
