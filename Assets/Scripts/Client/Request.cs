using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Request
{
    public List<FoodData> recipe;

    public Request(List<FoodData> recipe)
    {
        this.recipe = recipe;
    }
}
