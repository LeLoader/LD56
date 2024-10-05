using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Database", menuName = "Database/Create DBRoom", order = 1)]
public class DBRoom : ScriptableObject
{
    [SerializeField] List<Room> roomList;

    public List<Room> GetRoomList()
    {
        return roomList;
    }
}
