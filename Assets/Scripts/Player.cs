using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public bool CanMoove = true;

    public AnimatorController animatorRollingpin;
    public AnimatorController animatorPan;
    public AnimatorController animatorKnife;
    public Sprite spriteRollingpin;
    public Sprite spritePan;
    public Sprite spriteKnife;

    public static event Action ToggleMenu;

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

        if (CanMoove)
        {
            rb.velocity = Vector2.ClampMagnitude(new Vector2(xVelocity * speed, yVelocity * speed), speed);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu.Invoke();
        }
    } 
}
