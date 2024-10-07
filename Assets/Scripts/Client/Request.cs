using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Request
{
    public Dictionary<FoodData, FoodState> recipe;

    public Request(Dictionary<FoodData, FoodState> recipe)
    {
        this.recipe = recipe;
    }
}

public enum FoodState
{
    None,
    Alive,
    Killed
}
