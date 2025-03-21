using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomEntryPoint : MonoBehaviour
{
    [SerializeField]
    public Room to;

    bool playerInCollider;

    public static event Action<Room> OnPlayerChangeRoom;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInCollider = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInCollider = false;
        }  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInCollider)
        {
            OnPlayerChangeRoom.Invoke(to);
        }
    }
}
