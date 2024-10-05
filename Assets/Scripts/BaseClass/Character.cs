using UnityEditor.Animations;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed = 5;

    [SerializeField] protected int baseLife = 10;
    protected int life;

    [SerializeField] protected Collider2D collisionCollider;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    protected virtual void Start()
    {
        collisionCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
         
        life = baseLife;
    }

    private void Update()
    {
        if (life <= 0)
        {
            OnDeath();
        }
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
        life -= damage;
        Debug.Log($"Ouch! {this} took {damage} damage! He has now {life} hp left");
    }
}
