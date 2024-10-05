using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    float cooldownTimer;

    void Start()
    {
        if(weaponData == null)
        {
            Debug.LogWarning("Weapon is not initialized");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCooldownTimer();
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownTimer <= 0)
        {
            Attack();

            cooldownTimer = weaponData.cooldown;
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
    }

    void UpdateCooldownTimer()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                Debug.Log("CanAttack");
            }
        }
    }
}
