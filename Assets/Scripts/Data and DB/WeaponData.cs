using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/Create Weapon", order = 1)]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    [Tooltip("Per seconds")]
    public float cooldown;

    [Tooltip("Per hits")]
    public float damage;

    public Sprite sprite;
}
