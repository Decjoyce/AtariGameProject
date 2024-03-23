using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public int roomID;

    public Transform camPos;

    RoomManager manager;

    private void Start()
    {
        manager = RoomManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int player = other.GetComponent<PlayerController>().playerNum;
            manager.ChangeRoom(player, roomID);
        }
    }
}
