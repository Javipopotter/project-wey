using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    public NewPlayerController newPlayerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InstaKiller"))
        {
            newPlayerController.vivo = false;
        }
    }
}
