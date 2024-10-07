using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleWeapon : MonoBehaviour
{
    bool playerIn;
    int weaponindex;
    public List<WeaponData> unlockedWeaponList;
    public Weapon weapon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIn)
        {
            CycleWeaponn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = false;
        }
    }

    void CycleWeaponn()
    {
        if (unlockedWeaponList.Count <= 1)
        {
            return;
        }
        else if (weaponindex == unlockedWeaponList.Count - 1)
        {
            weaponindex = 0;
        }
        else
        {
            weaponindex++;
        }
        weapon.SetWeapon(unlockedWeaponList[weaponindex]);
    }
}
