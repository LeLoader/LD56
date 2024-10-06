using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    DBRoom roomDatabase;
    [SerializeField]
    RoomData startRoom;
    [SerializeField]
    Player player;

    RoomData currentRoom;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player.gameObject);
        RoomEntryPoint.OnPlayerChangeRoom += LoadScene;
        SceneManager.sceneLoaded += InitScene;
    }

    public void StartGame()
    {
        LoadScene(startRoom);
        player.canMoove = true;
    }

    void LoadScene(RoomData room)
    {
        SceneManager.LoadScene(room.sceneName);
    }

    void InitScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (currentRoom == null)
        {
            GameObject[] gos = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject go in gos)
            {
                if (go.TryGetComponent<Room>(out Room newRoom))
                {
                    currentRoom = newRoom.Init(player);
                    return;
                }
            }
        }
        else
        {
            currentRoom = FindObjectOfType<Room>().Init(player, currentRoom);
        }
    }
}
