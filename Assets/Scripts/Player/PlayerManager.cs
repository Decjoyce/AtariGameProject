using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerInput> players = new List<PlayerInput>();

    private PlayerInputManager playerInputManager;

    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    public Camera[] playerCams;
    public Camera sharedCam;
    //[SerializeField] private GameObject[] playerCanvases;

    [SerializeField] private GameObject playerHealths_one, playerHealths_two;
    [SerializeField] private Slider playerHealthSlider_one_p1, playerHealthSlider_two_p1, playerHealthSlider_two_p2; 
    [SerializeField] private Slider playerAmmoSlider_one_p1, playerAmmoSlider_two_p1, playerAmmoSlider_two_p2; 

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

    public void EnableJoining()
    {
        playerInputManager.EnableJoining();
    }

    public void DisableJoining()
    {
        playerInputManager.DisableJoining();
    }

    public void AddPlayer(PlayerInput player)
    {
        sharedCam.cullingMask = 0;

        player.transform.parent = transform;

        players.Add(player);
        player.name = "Player" + players.Count;
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.playerNum = players.Count;
        controller.playerCam = playerCams[players.Count - 1];
        controller.canvas.worldCamera = playerCams[players.Count - 1];

        SetSplitScreen(players.Count);

        player.transform.position = spawnPoints[players.Count - 1].position;
    }

    public void ClearPlayers()
    {
        for(int i = 0; i < players.Count; i++)
        {
            Destroy(players[i]);
        }
        players.Clear();
        playerInputManager.DisableJoining();
    }

    public void PlayersToCharacterSelect()
    {
        for (int i = 0; i < players.Count; i++)
        {
            PlayerController pc = players[i].GetComponent<PlayerController>();
            if (!pc.isOut)
            {
                pc.SwitchState("CHARACTERSELECT");
            }
        }
    }

    public void ResetPlayers()
    {
        for(int i = 0; i < players.Count; i++)
        {           
            PlayerController pc = players[i].GetComponent<PlayerController>();
            Rigidbody rb = players[i].GetComponent<Rigidbody>();
            if (!pc.isOut)
            {
                rb.MovePosition(spawnPoints[i].position);
                pc.SwitchState("NEUTRAL");
                pc.health.Revive();
                pc.health.SetHealthColor();
            }
            else
            {
                rb.MovePosition(Vector3.one * 420f);
                pc.SwitchState("DEATH");
            }
        }
    }

    public void SetSplitScreen(int amountOfPlayers)
    {
        if (keepRatio)
        {
            switch (amountOfPlayers)
            {
                case 1:
                    playerCams[0].gameObject.SetActive(true);
                    playerHealths_one.SetActive(true);
                    players[0].GetComponent<PlayerHealth>().healthSlider = playerHealthSlider_one_p1;
                    players[0].GetComponent<PlayerAttack>().ammoSlider = playerAmmoSlider_one_p1;
                    break;
                case 2:
                    playerCams[1].gameObject.SetActive(true);

                    playerHealths_one.SetActive(false);
                    playerHealths_two.SetActive(true);

                    players[0].GetComponent<PlayerHealth>().healthSlider = playerHealthSlider_two_p1;
                    players[1].GetComponent<PlayerHealth>().healthSlider = playerHealthSlider_two_p2;

                    players[0].GetComponent<PlayerAttack>().ammoSlider = playerAmmoSlider_two_p1;
                    players[1].GetComponent<PlayerAttack>().ammoSlider = playerAmmoSlider_two_p2;

                    playerCams[0].rect = new Rect(0.25f, 0.5f, 0.5f, 1f);
                    playerCams[1].rect = new Rect(0.25f,- 0.5f, 0.5f, 1);
                    break;
                #region 4 player Code
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
                #endregion
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
