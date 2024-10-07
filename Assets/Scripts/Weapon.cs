using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damage))]
public class Weapon : MonoBehaviour
{
    [SerializeField]
    public AudioSource attacksound;
    [SerializeField] 
    public WeaponData weaponData;
    [SerializeField]
    Collider2D attackCollider;
    [SerializeField]
    Player player;

    float cooldownTimer;
    float attackTimer;
    Damage damageComponent;

    public static event Action<WeaponData> OnUpdateWeapon;

    void Start()
    {
        if(weaponData == null)
        {
            Debug.LogWarning("Weapon is not initialized");
        }
        cooldownTimer = weaponData.cooldown;
        attackTimer = weaponData.attackTime;

        damageComponent = GetComponent<Damage>();
        damageComponent.SetDamage(weaponData.damage);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCooldownTimer();
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownTimer <= 0)
        {
            Attack();      
        }
    }

    void Attack()
    {
        player.IsAttacking = true;
        attackCollider.enabled = true;
        StartCoroutine(StopAttack(attackTimer));
        attacksound.Play();
    }

    public void SetWeapon(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        if(weaponData.weaponName == "rollingpin")
        {
            player.spriteRenderer.sprite = player.spriteRollingpin;
            player.animator.runtimeAnimatorController = player.animatorRollingpin;
        }
        if (weaponData.weaponName == "pan")
        {
            player.spriteRenderer.sprite = player.spritePan;
            player.animator.runtimeAnimatorController = player.animatorPan;

        }
        if (weaponData.weaponName == "knife")
        {
            player.spriteRenderer.sprite = player.spriteKnife;
            player.animator.runtimeAnimatorController = player.animatorKnife;
        }
        OnUpdateWeapon.Invoke(weaponData);
    }

    IEnumerator StopAttack(float afterTime)
    {
        float timer = afterTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        attackCollider.enabled = false;
        player.IsAttacking = false;
        cooldownTimer = weaponData.cooldown;
    }

    void UpdateCooldownTimer()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
