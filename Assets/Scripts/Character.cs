using System;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    [SerializeField] protected float speed = 5;

    public int baseLife = 10;
    [SerializeField] protected int life;
    [SerializeField] protected int bonusLife = 0;

    [SerializeField] protected Collider2D collisionCollider;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rb;

    public static event Action<Character> OnUpdateHealth;

    protected virtual void Start()
    {
        if(collisionCollider == null)
        {
            collisionCollider = GetComponent<CapsuleCollider2D>();
        }
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        life = baseLife;
    }

    protected virtual void OnDeath()
    {
        //Play death anim
    }

    protected virtual void Update()
    {
        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
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

        if (life <= 0)
        {
            OnDeath();
        }
    }

    public void AddLife(int amount)
    {
        OnUpdateHealth.Invoke(this);
        life += amount;

        if (life > baseLife)
        {
            life = baseLife;
        }

        if (life <= 0)
        {
            OnDeath();
        }
    }

    public void AddBonusLife(int amount)
    {
        OnUpdateHealth.Invoke(this);
        bonusLife += amount;
    }

    public void ModifyBaseLife(int amount)
    {
        OnUpdateHealth.Invoke(this);
        baseLife = amount;
    }

    public int GetLife()
    {
        return life;
    }

    public int GetBonusLife()
    {
        return bonusLife;
    }

    protected void UpdateHealth(Character character)
    {
        OnUpdateHealth.Invoke(character);
    }
}
