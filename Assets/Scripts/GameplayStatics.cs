using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayStatics
{ 
    static public T AddOrGetComponent<T>(this GameObject go) where T : Component
    {
        if (go.GetComponent<T>() == null)
        {
            return go.AddComponent<T>();
        }
        else
        {
            return go.GetComponent<T>();
        }
    }
}
