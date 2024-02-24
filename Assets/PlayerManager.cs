using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerInput> players = new List<PlayerInput>();

    private PlayerInputManager playerInputManager;

    [SerializeField] private List<LayerMask> playerLayers;
    //[SerializeField] private List<LayerMask> cameraLayers;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    public static PlayerManager instance;
    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        if (instance != null)
        {
            Debug.LogWarning("Error more than one " + name + " component found");
            return;
        }
        instance = this;
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        player.name = "Player" + players.Count;
        player.GetComponentInChildren<PlayerController>().playerNum = players.Count;
        //SetLayers(player);
        //SetVisuals(player);
        player.transform.position = spawnPoints[players.Count - 1].position;

    }

    void SetLayers(PlayerInput player)
    {
        int _playerLayerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);
        //int _cameraLayerToAdd = (int)Mathf.Log(cameraLayers[players.Count - 1].value, 2);
        player.gameObject.layer = _playerLayerToAdd;
        PlayerController controller = player.gameObject.GetComponentInChildren<PlayerController>();
        //controller.playerCam.cullingMask
    }
}
