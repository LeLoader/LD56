using System;
using UnityEditor.Animations;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed = 5;

    [SerializeField] protected int baseLife = 10;
    public int life;
    public int bonusLife = 0;

    [SerializeField] protected Collider2D collisionCollider;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public static event Action<Character> OnUpdateHealth;

    protected virtual void Start()
    {
        collisionCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
         
        life = baseLife;
    }

    protected virtual void OnDeath()
    {
        Debug.Log("OnDeath");
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void InflictDamage(int damage)
    {
        if(bonusLife > 0)
        {
            bonusLife -= damage;
            if (bonusLife < 0)
            {
                life += bonusLife; // BonusLife is negative, so we add it to inverse the sign
                bonusLife = 0; 
            }
        }
        else
        {
            life -= damage;
        }

        OnUpdateHealth.Invoke(this);
        Debug.Log($"Ouch! {this} took {damage} damage! He has now {life} hp left");

        if (life <= 0)
        {
            OnDeath();
        }
    }

    protected void UpdateHealth(Character character)
    {
        OnUpdateHealth.Invoke(character);
    }
}
