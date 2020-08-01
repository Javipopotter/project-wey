using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherFuckingRotation : MonoBehaviour
{
    public GameObject Player;
    //Esta Clase es totalmente copiada puesto que soy sumamente inútil

    // Update is called once per frame
    void Update()
    {
        Vector3 difer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotz = Mathf.Atan2(difer.y, difer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotz);
    }
}
