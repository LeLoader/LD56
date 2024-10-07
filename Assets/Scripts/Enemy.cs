using System;
using NaughtyAttributes;
using UnityEngine;
using System.Collections.Generic;

public class Enemy : Character
{
    [Header("References")]
    [SerializeField] AggroZone aggroZone;
    [SerializeField] StateController stateController;

    [Header("Gameplay")]
    [SerializeField, Tooltip("The range of the colliders (in meters) that will proc the chase state")]
    float aggroRange = 5;
    [SerializeField]
    public FoodData foodData;

    [Header("Gameplay|Attack")]
    [SerializeField, Tooltip("True if enemy is melee, false if enemy is range")]
    bool IsMelee;
    [Tooltip("Damage PER HIT that will inflict to player")]
    public int damage;
    [Tooltip("The range (in meters) that will proc the Prepare Attack state")]
    public float attackRange = 10;
    [HideIf("IsMelee"), Tooltip("The interval (in meters) that will be acceptable to proc Prepare Attack state (DISTANCE TO TARGET HAS TO BE INCLUDED IN [attackRange - attackRangeInterval, attackRange + attackRangeInterval])")]
    public float attackRangeInterval = 1;
    [HideIf("IsMelee"), Tooltip("The prefab to spawn for range enemy")]
    public GameObject projectilePrefab;
    [ShowIf("IsMelee"), Tooltip("GameObject containing the attack collider")]
    public GameObject attackColliderWrapper;
    [ShowIf("IsMelee"), Tooltip("The collider for this attack")]
    public Collider2D attackCollider;
    [Tooltip("The number of attack the enemy will perform"), Min(1)]
    public int attackCount;
    [Tooltip("The time (in seconds) when this enemy will stop to charge the attack")]
    public float attackWindupTime = 1f;
    [ShowIf("IsMelee"), Tooltip("The time (in seconds) at which the enemy will target the player. Must be less than attackWindupTime")]
    public float adjustAttackerColliderTime = 0.75f;
    [Tooltip("The time (in seconds) when the damage collider is active")]
    public float attackTime = 0.5f;
    [HideIf("IsMelee"), Tooltip("Does the projectile takes a random direction")]
    public bool hasRandomOffset = false;
    [ShowIf("hasRandomOffset"), Tooltip("Max random angle a projectile")]
    public float randomOffset = 0;

    bool inFight;

    public static event Action<Enemy> OnEnemyDeath;

    protected override void Start()
    {
        base.Start();

        if (aggroZone == null)
        {
            aggroZone = GetComponentInChildren<AggroZone>();
        }
        else
        {
            aggroZone.radius = aggroRange;
        }

        stateController = GetComponent<StateController>();

        if (attackCollider == null && IsMelee)
        {
            Debug.LogWarning($"Enemy {foodData.foodName} has no attached attack collider");
        } 

        if(foodData != null)
        {
            InitFoodData(foodData);
        }
    }

    public void InitFoodData(FoodData foodData)
    {
        /* damage = foodData.damage;
        attackRange = foodData.attackRange;
        attackRangeInterval = foodData.attackRangeInterval;
        projectilePrefab = foodData.projectilePrefab;
        attackCount = foodData.attackCount;
        attackWindupTime = foodData.attackWindupTime;
        adjustAttackerColliderTime = foodData.adjustAttackerColliderTime;
        attackTime = foodData.attackTime;
        hasRandomOffset = foodData.hasRandomOffset;
        randomOffset = foodData.randomOffset;
        baseLife = foodData.baseLife;
        speed = foodData.speed;

        AnimatorOverrideController animatorOverrideController = new(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["Carrot Walk Front"] = foodData.frontWalkClip;
        animatorOverrideController["Carrot Walk Back"] = foodData.backWalkClip;
        animatorOverrideController["Carrot Walk Side"] = foodData.sideWalkClip;*/     
    }

    protected override void Update()
    {
        base.Update();

        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        // Debug.Log("Fish" + foodData.name);

        if (rb.velocity.x < 0)
        {
            if (foodData.name == "Fish" || foodData.name == "Mushroom")
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (rb.velocity.x > 0)
        {
            if (foodData.name == "Fish" || foodData.name == "Mushroom")
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        animator.SetBool("IsAttacking", IsAttacking);
    }

    public void TriggerFight(Player player)
    {
        stateController.player = player;
        if (IsMelee)
        {
            stateController.ChangeState(stateController.chaseState);
        }
        else
        {
            stateController.ChangeState(stateController.repositionState);
        }
    }

    public void Shoot(Player target, Quaternion direction)
    {
        Quaternion randomOffsetQuaternion = Quaternion.Euler(0, 0, 0);
        if (hasRandomOffset)
        {
            randomOffsetQuaternion.eulerAngles = new(0, 0, UnityEngine.Random.Range(0, randomOffset));
        }
        GameObject instance = Instantiate(projectilePrefab, transform.position, direction * randomOffsetQuaternion); //transform.position + offset for adjusting where to spawn the projectile
        instance.GetComponent<Projectile>().damageComponent.SetDamage(damage);
    }

    protected override void OnDeath(Character character)
    {

        base.OnDeath(this);
        OnEnemyDeath.Invoke(this);
        Destroy(gameObject);
    }

    private void GiveReward()
    {
        throw new NotImplementedException();
    }
}
