using System;
using UnityEngine.Localization;
using NaughtyAttributes;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Enemy : Character
{
    [Header("References")]
    [SerializeField] CircleCollider2D aggroZone;
    [SerializeField] StateController stateController;

    [Header("General")]
    public LocalizedString enemyName;

    [Header("Gameplay")]
    [SerializeField, Tooltip("The range of the colliders (in meters) that will proc the chase state")]
    float aggroRange = 5;

    bool inFight;

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

    protected override void Start()
    {
        base.Start();

        if (aggroZone == null)
        {
            Debug.LogWarning($"Enemy {enemyName["carrot"]} has no attached aggro zone collider");
        }
        aggroZone = GetComponent<CircleCollider2D>(); 
        aggroZone.radius = aggroRange;
        aggroZone.isTrigger = true;

        stateController = GetComponent<StateController>();
        
        if(attackCollider == null)
        {
            Debug.LogWarning($"Enemy {enemyName["carrot"]} has no attached attack collider");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.TryGetComponent<Player>(out Player player))
        {
            TriggerFight(player);
            Destroy(aggroZone);
        }
    }

    void TriggerFight(Player player)
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
        GameObject instance = Instantiate(projectilePrefab, transform.position, direction); //transform.position + offset for adjusting where to spawn the projectile
        instance.GetComponent<Projectile>().damagePlayer.SetDamage(damage);
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        Debug.Log("Enemy dead");
        GiveReward();
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
