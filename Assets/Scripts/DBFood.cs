using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food Database", menuName = "Database/Create DBFood", order = 1)]
public class DBFood : ScriptableObject
{
    [SerializeField] List<Food> foodList;

    public List<Food> GetFoodList()
    {
        return foodList;
    }
}
