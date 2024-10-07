using AYellowpaper.SerializedCollections;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("References")]
    public new CinemachineVirtualCamera camera;
    public RoomData roomData;
    [SerializedDictionary("From Room", "SpawnPoint")]
    public List<RoomEntryPoint> spawnPoints;

    [HideInInspector]
    public Player player;
    
    public RoomData Init(Player player, RoomData oldRoom = null)
    {
        this.player = player;
        player.canMoove = true;

        if (!roomData.isCameraFixed)
        {
            camera.Follow = player.transform;
        }

        if (oldRoom != null)
        {
            Vector2 newSpawnPosition = Vector2.zero;
            foreach (RoomEntryPoint entryPoint in spawnPoints)
            {
                if (entryPoint.to == oldRoom)
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
            Debug.LogWarning("Didn't find a suitable place to spawn, spawning in 0,0");
        }

        return roomData;
    }
}
