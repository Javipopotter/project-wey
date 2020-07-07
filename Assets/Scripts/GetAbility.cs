using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAbility : MonoBehaviour
{
    public GameObject Player;
    public string ScriptAdd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch(ScriptAdd)
            {
                case "Dash":
                    Player.GetComponent<Dash>().enabled = true;
                    Destroy(gameObject);
                    break;
                case "Agarre":
                    Player.GetComponent<Agarre>().enabled = true;
                    Destroy(gameObject);
                    break;

            }
        }
    }
}
