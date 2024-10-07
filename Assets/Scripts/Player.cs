using UnityEngine;

public class Player : Character
{
   
    public bool canMoove = true;

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
