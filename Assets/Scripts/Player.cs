using UnityEngine;

public class Player : Character
{
    [SerializeField] protected Rigidbody2D rb;
    public bool canMoove;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float xVelocity = Input.GetAxis("Horizontal");
        float yVelocity = Input.GetAxis("Vertical");

        animator.SetFloat("xVelocityAbs", Mathf.Abs(xVelocity));
        animator.SetFloat("yVelocity", yVelocity);

        if (xVelocity < 0) spriteRenderer.flipX = true;
        else if (xVelocity > 0) spriteRenderer.flipX = false;

        if (canMoove)
        {
            rb.velocity = Vector2.ClampMagnitude(new Vector2(xVelocity * speed, yVelocity * speed), speed);
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        Debug.Log("Player dead");
    }
}
