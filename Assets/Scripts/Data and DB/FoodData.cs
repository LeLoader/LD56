using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FoodData
{
    public string foodName;
    public Sprite sprite;

    [Tooltip("RoomData in which the food can spawn")]
    public RoomData room;
}
