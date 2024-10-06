using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomEntryPoint : MonoBehaviour
{
    [SerializeField]
    public RoomData to;

    bool playerInCollider;

    public static event Action<RoomData> OnPlayerChangeRoom;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInCollider = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInCollider = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInCollider)
        {
            OnPlayerChangeRoom.Invoke(to);
        }
    }
}
