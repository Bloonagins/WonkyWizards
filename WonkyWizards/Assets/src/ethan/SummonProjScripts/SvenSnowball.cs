using UnityEngine;

public class SvenSnowball : SummonProj
{
    private bool bursting;
    private Vector3 burstingSpeed;

    Animator animator;

    public SvenSnowball()
    {
        speed = 2.0f;
        damage = 15;
        kockback = 10f;

        bursting = false;
        burstingSpeed = new Vector3(0.2f, 0.2f, 0f);
    }

    public override void Start()
    {
        // get animator object for fade-out
        animator = gameObject.GetComponent<Animator>();
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        // deal damage as usuall
        base.OnTriggerEnter2D(col);

        // check that the gameObject is an Enemy
        if (col.gameObject.tag == "Enemy")
        {
            // if so, delete self after a half of a second

            Invoke("killSelf", 0.5f);
            bursting = true;
            animator.SetTrigger("FadeOut");
        }
    }

    public override void FixedUpdate()
    {
        if (bursting)
        {
            transform.localScale += burstingSpeed;
        }
        else
        {
            base.FixedUpdate();
        }
    }
}
