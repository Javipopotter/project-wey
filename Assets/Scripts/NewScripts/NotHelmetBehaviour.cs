using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotHelmetBehaviour : MonoBehaviour
{
    bool HasRb;

    // Update is called once per frame
    void Update()
    {
        if (HasRb == false && transform.parent == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().mass = 0.1f;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3.17f;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            HasRb = false;
        }
        else
        {
            HasRb = true;
        }
    }
}
