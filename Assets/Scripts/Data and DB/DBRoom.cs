using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomData Database", menuName = "Database/Create DBRoom", order = 1)]
public class DBRoom : ScriptableObject
{
    [SerializeField] List<RoomData> roomList;

    public List<RoomData> GetRoomList()
    {
        return roomList;
    }
}
