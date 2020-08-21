using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTheHatFromBottom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<HelmetsController>().GetFromBottom = true;
        }
    }
}
