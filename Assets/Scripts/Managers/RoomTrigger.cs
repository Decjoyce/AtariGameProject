using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public int roomID;

    public Transform camPos, campos_only1player;

    RoomManager manager;

    private void Start()
    {
        manager = RoomManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int playerID = other.GetComponent<PlayerController>().playerNum;
            manager.ChangeRoom(other.gameObject, playerID, roomID);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.LeftRoom(other.gameObject, roomID);
        }
    }
}
