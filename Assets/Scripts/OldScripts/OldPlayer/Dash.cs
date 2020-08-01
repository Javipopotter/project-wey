using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public NewPlayerController newPlayerController;
    public ParticleSystem Dust;
    Rigidbody2D rb2d;
    public float dashSpeed;
    public float rdashTimer = 0.2f;
    public float ldashTimer = 0.2f;
    private float cdashTimer;
    bool rdTimerActivator;
    bool ldTimerActivator;
    bool dashed;
    bool FlagDashStop;

    public float GravST = 0.2f;
    bool GravSTA;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        cdashTimer = rdashTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            rdTimerActivator = true;
        }

        if (Input.GetKeyDown("d") && (rdashTimer > 0 && rdashTimer < cdashTimer) && dashed == false)
        {
            rb2d.AddForce(new Vector2(dashSpeed, 0));
            Dust.Play();
            newPlayerController.Jparticles.Stop();
            dashed = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            FlagDashStop = true;

            GravSTA = true;
            rb2d.gravityScale = 0;
        }

        if (Input.GetKeyDown("a"))
        {
            ldTimerActivator = true;
        }

        if (Input.GetKeyDown("a") && (ldashTimer > 0 && ldashTimer < cdashTimer) && dashed == false)
        {
            rb2d.AddForce(new Vector2(-dashSpeed, 0));
            Dust.Play();
            newPlayerController.Jparticles.Stop();
            dashed = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            FlagDashStop = true;

            GravSTA = true;
            rb2d.gravityScale = 0;
        }

        if(FlagDashStop == true)
        {
            FlagDashStop = false;
            rb2d.velocity = Vector2.zero;
        }

        restartDashTimer();
        DashTimerActivation();
    }
    void restartDashTimer()
    {
        if (rdashTimer <= 0)
        {
            rdashTimer = cdashTimer;
            rdTimerActivator = false;
        }

        if (ldashTimer <= 0)
        {
            ldashTimer = cdashTimer;
            ldTimerActivator = false;
        }

        if (GravST <= 0 || Input.GetKeyDown(KeyCode.Space))
        {
            GravSTA = false;
            GravST = 0.4f;
            rb2d.gravityScale = 19.1f;
        }
    }

    void DashTimerActivation()
    {
        if (rdTimerActivator == true)
        {
            rdashTimer -= Time.deltaTime;
        }

        if (ldTimerActivator == true)
        {
            ldashTimer -= Time.deltaTime;
        }

        if (GravSTA == true)
        {
            GravST -= Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground" || collision.gameObject.tag == "AnotherBrickInTheWall")
        {
            dashed = false;
        }
    }
}
