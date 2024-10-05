using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    [Header("References")]
    [SerializeField] CircleCollider2D aggroZone;
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] StateController stateController;

    [Header("Gameplay")]
    [Category("Gameplay"), SerializeField, Tooltip("The range of the colliders (in meters) that will proc the chase state")]
    float aggroRange = 5;

    [Header("Gameplay|Attack")]
    [SerializeField, Tooltip("Damage PER HIT that will inflict to player")]
    int damage;
    [Tooltip("The range (in meters) that will proc the Prepare Attack state")]
    public float attackRange = 10;
    [Tooltip("GameObject containing the attack collider")]
    public GameObject attackColliderWrapper;
    [Tooltip("The collider for this attack")]
    public Collider2D attackCollider;
    [Tooltip("The time (in seconds) when this enemy will stop to charge the attack")]
    public float attackWindupTime = 1f;
    [Tooltip("The time (in seconds) when the damage collider is active")]
    public float attackTime = 0.5f;

    private void Start()
    {
        base.Start();

        aggroZone ??= GetComponent<CircleCollider2D>(); 
        aggroZone.radius = aggroRange;
        aggroZone.isTrigger = true;

        spriteRenderer ??= GetComponent<SpriteRenderer>();
        stateController ??= GetComponent<StateController>();
        
        if(attackCollider == null)
        {
            Debug.LogWarning($"Ennemy {this} has no attack collider attach");
        }
    }

    private void Update()
    {
        if(life <= 0)
        {
            OnDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.TryGetComponent<Player>(out Player player))
        {
            TriggerFight(player);
        }
    }

    void TriggerFight(Player player)
    {
        stateController.player = player;
        stateController.ChangeState(stateController.chaseState);
    }

    void OnDeath()
    {
        GiveReward();
    }

    private void GiveReward()
    {
        throw new NotImplementedException();
    }
}
