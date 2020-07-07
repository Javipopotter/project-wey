using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsDestroyer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Main Camera")
        {
            Destroy(gameObject);
        }
    }
}
