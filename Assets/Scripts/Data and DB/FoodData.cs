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
    public GameObject prefab;

    [Header("Gameplay")]
    public int baseLife;
    public int speed;
}
