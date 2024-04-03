using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomManager : MonoBehaviour
{
    private Dictionary<int, int> playerRooms = new Dictionary<int, int>();

    [SerializeField] RoomTrigger[] rooms; //StartRoom should always be at index 0!!!!!
    [SerializeField] Camera[] playerCams;

    public delegate void RoomEntered(GameObject player, int roomID);
    public delegate void RoomExited(GameObject player, int roomID);
    public static event RoomEntered OnEnter;
    public static event RoomExited OnExit;

    public static RoomManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Error more than one " + name + " component found");
            return;
        }
        instance = this;
    }

    private void OnEnable()
    {
        OnEnter += Shush;
        OnExit += Shush;
    }

    private void OnDisable()
    {
        OnEnter -= Shush;
        OnExit -= Shush;
    }

    private void Start()
    {
        RoomsInit();
    }

    public void ChangeRoom(GameObject player, int playerNum, int roomID)
    {
        playerRooms[playerNum] = roomID;
        playerCams[playerNum].transform.position = rooms[roomID].camPos.position;
        OnEnter(player, roomID);
    }

    public void LeftRoom(GameObject player, int roomID)
    {
        OnExit(player, roomID);
    }

    void RoomsInit()
    {
        playerRooms.Add(1, 0);
        playerRooms.Add(2, 0);
        playerRooms.Add(3, 0);
        playerRooms.Add(4, 0);

        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i].roomID = i;
        }
    }

    void Shush(GameObject player, int roomID)
    {
        
    }
}
