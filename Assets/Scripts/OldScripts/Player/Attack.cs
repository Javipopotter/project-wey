using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject attack;
    public float AttackTimer; 
    public bool CodeActivator;
    float AttackTimerClone;
    bool TimerActivator;
    // Start is called before the first frame update
    void Start()
    {
      AttackTimerClone = AttackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && (AttackTimer == AttackTimerClone))
        {
            TimerActivator = true;
            Instantiate(attack, transform.position, transform.rotation).transform.parent = gameObject.transform;
            CodeActivator = false;
        }

        if(TimerActivator == true)
        {
            AttackTimer -= Time.deltaTime;

            if(AttackTimer <= 0)
            {
                AttackTimer = AttackTimerClone;
                TimerActivator = false;
            }
        }
    }
}
