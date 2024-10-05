using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Food
{
    public string foodName;
    public Sprite sprite;

    [Tooltip("Room in which the food can spawn")]
    public Room room;
}
