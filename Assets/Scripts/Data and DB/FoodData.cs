using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "ScriptableObject/Create FoodData", order = 1)]
public class FoodData : ScriptableObject
{
    public string foodName;
    public Sprite sprite;
}
