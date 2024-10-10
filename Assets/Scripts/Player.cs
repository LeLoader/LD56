using System;
using UnityEngine;

public class Player : Character
{
    public bool CanMoove = true;

    public RuntimeAnimatorController animatorRollingpin;
    public RuntimeAnimatorController animatorPan;
    public RuntimeAnimatorController animatorKnife;
    public Sprite spriteRollingpin;
    public Sprite spritePan;
    public Sprite spriteKnife;

    public static event Action ToggleMenu;

    private void Awake()
    {
        UI.OnRetry += Retry;
    }

    protected override void Start()
    {
        base.Start();
        base.UpdateHealth(this);
    }

    protected override void Update()
    {
        base.Update(); 

        float xVelocity = Input.GetAxis("Horizontal");
        float yVelocity = Input.GetAxis("Vertical");

        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        if (rb.velocity.x < 0) spriteRenderer.flipX = true;
        else if (rb.velocity.x > 0) spriteRenderer.flipX = false;

        animator.SetBool("IsAttacking", IsAttacking);

        if (CanMoove)
        {
            rb.velocity = Vector2.ClampMagnitude(new Vector2(xVelocity * speed, yVelocity * speed), speed);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu.Invoke();
        }
    }

    void Retry()
    {
        life = baseLife;
        speed = baseSpeed;
        transform.position = Vector2.zero;
        base.UpdateHealth(this);
    }
}
