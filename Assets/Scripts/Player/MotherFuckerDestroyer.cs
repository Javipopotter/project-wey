using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherFuckerDestroyer : MonoBehaviour
{
    public Attack attack;
    public float DestroyerTimer = 0.1f;
    void Update()
    {
        if(attack.CodeActivator == false)
        {
            DestroyerTimer -= Time.deltaTime;
        }

        if(DestroyerTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
