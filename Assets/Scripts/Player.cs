using UnityEngine;

public class Player : Character
{
    [SerializeField] protected Rigidbody2D rb;
    public bool canMoove = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        base.UpdateHealth(this);
    }

    void Update()
    {

        float xVelocity = Input.GetAxis("Horizontal");
        float yVelocity = Input.GetAxis("Vertical");

        if (canMoove)
        {
            rb.velocity = Vector2.ClampMagnitude(new Vector2(xVelocity * speed, yVelocity * speed), speed);

            if (xVelocity < 0) spriteRenderer.flipX = true;
            else if (xVelocity > 0) spriteRenderer.flipX = false;

            animator.SetFloat("xVelocityAbs", Mathf.Abs(xVelocity));
            animator.SetFloat("yVelocity", yVelocity);
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        Debug.Log("Player dead");
    }
}
