using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    const int baseLife = 10;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        float xVelocity = Input.GetAxis("Horizontal");
        float yVelocity = Input.GetAxis("Vertical");
        // if (xVelocity < 0) spriteRenderer.flipX = false;
        // else if (xVelocity > 0) spriteRenderer.flipX = true;
        rb.velocity = Vector2.ClampMagnitude(new Vector2(xVelocity * speed, yVelocity * speed), speed);
    }
}
