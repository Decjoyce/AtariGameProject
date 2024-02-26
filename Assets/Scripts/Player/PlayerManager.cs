using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerInput> players = new List<PlayerInput>();

    private PlayerInputManager playerInputManager;

    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    public Camera[] playerCams;
    public Camera sharedCam;
    [SerializeField] private GameObject[] playerCanvases;

    [SerializeField] GameObject tempText; //temp
    [SerializeField] bool keepRatio; //temp

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
        tempText.SetActive(false);  //temp
        sharedCam.cullingMask = 0;

        players.Add(player);
        player.name = "Player" + players.Count;
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.playerNum = players.Count;
        controller.playerCam = playerCams[players.Count - 1];
        controller.canvas.worldCamera = playerCams[players.Count - 1];

        SetSplitScreen();

        player.transform.position = spawnPoints[players.Count - 1].position;
    }

    public void SetSplitScreen()
    {
        if (keepRatio)
        {
            switch (players.Count)
            {
                case 1:
                    playerCams[0].gameObject.SetActive(true);
                    break;
                case 2:
                    playerCams[1].gameObject.SetActive(true);

                    playerCams[0].rect = new Rect(0.25f, 0.5f, 0.5f, 1f);
                    playerCams[1].rect = new Rect(0.25f,- 0.5f, 0.5f, 1);
                    break;
                case 3:
                    playerCams[2].gameObject.SetActive(true);

                    playerCams[0].rect = new Rect(0f, 0.5f, 0.5f, 1);
                    playerCams[1].rect = new Rect(0.5f, 0.5f, 0.5f, 1);
                    playerCams[2].rect = new Rect(0.25f, -0.5f, 0.5f, 1);
                    break;
                case 4:
                    playerCams[3].gameObject.SetActive(true);

                    playerCams[0].rect = new Rect(0f, 0.5f, 0.5f, 1);
                    playerCams[1].rect = new Rect(0.5f, 0.5f, 0.5f, 1);
                    playerCams[2].rect = new Rect(0f, -0.5f, 0.5f, 1);
                    playerCams[3].rect = new Rect(0.5f, -0.5f, 0.5f, 1);
                    break;
                default:
                    Debug.LogWarning("Error when trynna splitscreen yo");
                    break;
            }
        }
        else
        {
            switch (players.Count)
            {
                case 1:
                    playerCams[0].gameObject.SetActive(true);
                    break;
                case 2:
                    playerCams[1].gameObject.SetActive(true);

                    playerCams[0].rect = new Rect(0f, 0.5f, 1, 1);
                    playerCams[1].rect = new Rect(0f, -0.5f, 1, 1);
                    break;
                case 3:
                    playerCams[2].gameObject.SetActive(true);

                    playerCams[0].rect = new Rect(-0.5f, 0.5f, 1, 1);
                    playerCams[1].rect = new Rect(0.5f, 0.5f, 1, 1);
                    playerCams[2].rect = new Rect(0f, -0.5f, 1, 1);
                    break;
                case 4:
                    playerCams[3].gameObject.SetActive(true);

                    playerCams[0].rect = new Rect(-0.5f, 0.5f, 1, 1);
                    playerCams[1].rect = new Rect(0.5f, 0.5f, 1, 1);
                    playerCams[2].rect = new Rect(-0.5f, -0.5f, 1, 1);
                    playerCams[3].rect = new Rect(0.5f, -0.5f, 1, 1);
                    break;
                default:
                    Debug.LogWarning("Error when trynna splitscreen yo");
                    break;
            }
        }
    }

}
