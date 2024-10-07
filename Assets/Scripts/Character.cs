using System;
using System.Collections;
using System.Threading;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    public int baseSpeed = 5;
    public int speed;

    public int baseLife = 10;
    public int initLife = 10;
    [SerializeField] protected int life;
    [SerializeField] protected int bonusLife = 0;

    [SerializeField] protected Collider2D collisionCollider;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rb;

    public bool IsAttacking = false;

    public static event Action<Character> OnPlayerDeath;
    public static event Action<Character> OnUpdateHealth;

    protected virtual void Start()
    {
        UI.OnRetry += Retry;
        if(collisionCollider == null)
        {
            collisionCollider = GetComponent<CapsuleCollider2D>();
        }
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        life = initLife;
        speed = baseSpeed;
    }

    protected virtual void Update()
    {
        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        if (rb.velocity.x < 0) spriteRenderer.flipX = true;
        else if (rb.velocity.x > 0) spriteRenderer.flipX = false;
        animator.SetBool("IsAttacking", IsAttacking);
    }

    public int GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(int amount)
    {
        speed = amount;
        if (speed <= 0)
        {
            speed = 1;
        }
        if (speed >= 10)
        {
            speed = 10;
        }
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
            OnDeath(this);
        }
        StartCoroutine(RedFlash(this));
    }

    IEnumerator RedFlash(Character target)
    {
        float atime = 1;
        float atimer = 0;
        while (atime > atimer) {
            target.spriteRenderer.color = Color.Lerp(Color.red, Color.white, atimer / atime);
            atimer += Time.deltaTime;
            yield return null;
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
            OnDeath(this);
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

    protected virtual void OnDeath(Character character)
    {
        if (character.CompareTag("Player"))
        {
            speed = 0;
            foreach (Client client in FindObjectsByType<Client>(FindObjectsSortMode.None))
            {
                Destroy(client.gameObject);
            }
            foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
            {
                Destroy(enemy.gameObject);
            }
            OnPlayerDeath.Invoke(this);
        }
    }

    void Retry()
    {
        life = baseLife;
        speed = baseSpeed;
        transform.position = Vector2.zero;
        OnUpdateHealth.Invoke(this);
    }
}
