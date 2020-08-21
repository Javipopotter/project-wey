using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuulletBehaviour : MonoBehaviour
{
    Vector3 PlayerPos;
    Rigidbody2D rb;
    public GameObject Player;
    // Start is called before the first frame update
    void Awake()
    {     
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = transform.position - GameObject.Find("Player").transform.position;
        PlayerPos.Normalize();
        rb.AddForce(-PlayerPos * 1600f * Time.deltaTime);
    }
}
