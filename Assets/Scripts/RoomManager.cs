using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    Room[] rooms;
    [SerializeField]
    Player player;

    [SerializeField]
    Room kitchenRoom;

    [SerializeField]
    Room currentRoom;

    private void Awake()
    {
        RoomEntryPoint.OnPlayerChangeRoom += LoadRoom;
        rooms = FindObjectsByType<Room>(FindObjectsSortMode.None);
        UI.OnRetry += Retry;
    }

    void LoadRoom(Room room)
    {
        Vector2 newSpawnPosition = Vector2.zero;

        if (currentRoom != null)
        {
            currentRoom.camera.Priority = 0;

            foreach (RoomEntryPoint entryPoint in room.spawnPoints)
            {
                if (entryPoint.to == currentRoom)
                {
                    newSpawnPosition = entryPoint.transform.position;
                    break;
                }
            }

            player.transform.position = newSpawnPosition;
        }
        else
        {
            player.transform.position = Vector2.zero;
        }
        
        currentRoom = room;
        currentRoom.camera.Priority = 1;
        if (currentRoom.roomData.isCameraFixed)
        {
            currentRoom.camera.Follow = null;
        }
        else
        {
            currentRoom.camera.Follow = player.transform;
        }
    }

    void Retry()
    {
        LoadRoom(kitchenRoom);
    }
}
