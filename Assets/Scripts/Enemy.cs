using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEditor;
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

    bool inFight;

    [Header("Gameplay|Attack")]
    [ReadOnly, SerializeField, Tooltip("True if enemy is melee, false if enemy is range")]
    bool IsMelee;
    [ReadOnly, Tooltip("Damage PER HIT that will inflict to player")]
    public int damage;
    [Tooltip("The range (in meters) that will proc the Prepare Attack state")]
    public float attackRange = 10;
    [HideIf("IsMelee"), Tooltip("The interval (in meters) that will be acceptable to proc Prepare Attack state (DISTANCE TO TARGET HAS TO BE INCLUDED IN [attackRange - attackRangeInterval, attackRange + attackRangeInterval])")]
    public float attackRangeInterval = 1;
    [ReadOnly, HideIf("IsMelee"), Tooltip("The prefab to spawn for range enemy")]
    public GameObject projectilePrefab;
    [ShowIf("IsMelee"), Tooltip("GameObject containing the attack collider")]
    public GameObject attackColliderWrapper;
    [ShowIf("IsMelee"), Tooltip("The collider for this attack")]
    public Collider2D attackCollider;
    [ReadOnly, Tooltip("The number of attack the enemy will perform"), Min(1)]
    public int attackCount;
    [ReadOnly, Tooltip("The time (in seconds) when this enemy will stop to charge the attack")]
    public float attackWindupTime = 1f;
    [ReadOnly, ShowIf("IsMelee"), Tooltip("The time (in seconds) at which the enemy will target the player. Must be less than attackWindupTime")]
    public float adjustAttackerColliderTime = 0.75f;
    [ReadOnly, Tooltip("The time (in seconds) when the damage collider is active")]
    public float attackTime = 0.5f;
    [ReadOnly, HideIf("IsMelee"), Tooltip("Does the projectile takes a random direction")]
    public bool hasRandomOffset = false;
    [ReadOnly, ShowIf("hasRandomOffset"), Tooltip("Max random angle a projectile"), Min(0)]
    public float randomOffset = 0;

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
    }

    public void InitFoodData(FoodData foodData)
    {
        damage = foodData.damage;
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

    protected override void OnDeath()
    {
        base.OnDeath();
        OnEnemyDeath.Invoke(this);
        Destroy(gameObject);
    }

    private void GiveReward()
    {
        throw new NotImplementedException();
    }

    private void OnDrawGizmosSelected()
    {
        if (!IsMelee)
        {
            Handles.color = Color.red;
            Handles.DrawWireArc(transform.position, Vector3.forward, Vector2.up, 360, attackRange + attackRangeInterval);
            Handles.DrawWireArc(transform.position, Vector3.forward, Vector2.up, 360, attackRange - attackRangeInterval);
        }
        Handles.color = Color.green;
        Handles.DrawWireArc(transform.position, Vector3.forward, Vector2.up, 360, attackRange);
        Handles.color = Color.blue;
        Handles.DrawWireArc(transform.position, Vector3.forward, Vector2.up, 360, aggroRange);
    }
}
