using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    DistanceJoint2D distanceJoint2D;
    public float DistanceReductor;
    public float DistanceAdd;
    // Start is called before the first frame update
    void Start()
    {
        distanceJoint2D = gameObject.GetComponent<DistanceJoint2D>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Player") && (distanceJoint2D.distance > 5))
        {
            distanceJoint2D.distance -= DistanceReductor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Player"))
        {
            while (distanceJoint2D.distance < DistanceAdd)
            {
                distanceJoint2D.distance++;
            }
        }
    }
}