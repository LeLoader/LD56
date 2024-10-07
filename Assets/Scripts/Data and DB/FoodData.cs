using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "ScriptableObject/Create FoodData", order = 1)]
public class FoodData : ScriptableObject
{
    [Header("General")]
    public string foodName;
    public Sprite sprite;
    public Vector2 spawnPosition;

    [Header("Gameplay")]
    public int baseLife;
    public int speed;

    [Header("Attack")]
    [Tooltip("True if enemy is melee, false if enemy is range")]
    public bool IsMelee;
    [Tooltip("Damage PER HIT that will inflict to player")]
    public int damage;
    [Tooltip("The range (in meters) that will proc the Prepare Attack state")]
    public float attackRange = 10;
    [HideIf("IsMelee"), Tooltip("The interval (in meters) that will be acceptable to proc Prepare Attack state (DISTANCE TO TARGET HAS TO BE INCLUDED IN [attackRange - attackRangeInterval, attackRange + attackRangeInterval])")]
    public float attackRangeInterval = 1;
    [HideIf("IsMelee"), Tooltip("The prefab to spawn for range enemy")]
    public GameObject projectilePrefab;
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
    [ShowIf("hasRandomOffset"), Tooltip("Max random angle a projectile"), Min(0)]
    public float randomOffset = 0;
}
