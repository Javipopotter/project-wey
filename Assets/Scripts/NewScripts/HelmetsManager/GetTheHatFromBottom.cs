using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTheHatFromBottom : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<HelmetsController>().GetFromBottom = true;
        }
    }
}
