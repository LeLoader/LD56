using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "ScriptableObject/Create RoomData", order = 1)]
public class RoomData : ScriptableObject
{
    public string roomName;
    public string sceneName;

    [Tooltip("Is the camera be fixed on the whole map, false will be following player"), SerializeField]
    public bool isCameraFixed = false;
}
