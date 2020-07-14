using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesController : MonoBehaviour
{
    public int LifeNumber;
    public NewPlayerController newplayerController;
    public SpriteRenderer sr;
    public ParticleSystem Fire;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(newplayerController.vidas < LifeNumber)
        {
            sr.enabled = false;
            Fire.Play();
        }
        else
        {
            sr.enabled = true;
        }
    }
}
