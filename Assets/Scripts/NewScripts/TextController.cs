﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public GameObject moveText;
    public bool DisablesOnExit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveText.gameObject.SetActive(true);
        }     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && DisablesOnExit == true)
        {
            moveText.gameObject.SetActive(false);
        }
    }
}