using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public int playersAlive;
    public int playersDead;
    public int playersExtracted;
    //[System.NonSerialized] public PlayerController playerController;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance != null)
        {
            Debug.LogError("More than one gamemanager has been found");
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playersAlive = 0;
        playersDead = 0;
        playersExtracted = 0;
        //StartRound();
        Debug.Log("This is the start of the GameManager");
    }

    void StartRound()
    {
        playersAlive = PlayerManager.instance.players.Count - playersDead;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            StartRound();
        }
        if (playersAlive < 1 && playersExtracted < 1) 
        {
            //Debug.Log("Everyone is dead");
        }
        if(playersAlive < 1 && playersExtracted > 0)
        {
            //Debug.Log("Part of the crew escaped");
        }
        if (playersAlive > 0 && playersExtracted >0)
        {
            //Debug.Log("Waiting on other players to extract");
        }
        if(playersDead < 1 && playersExtracted > 0 && playersAlive < 1)
        {
           // Debug.Log("Everyone escaped");
            SceneManager.LoadScene(0);
        }
    }
    void OnEnable()
    {
        PlayerHealth.OnPlayerDied += DecreasePlayersAliveAmount;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= DecreasePlayersAliveAmount;
    }

    public void IncreaseExtractedPlayerAmount()
    {
        playersExtracted++;
        playersAlive--;
    }

    public void DecreaseExtractedPlayerAmount()
    {
        playersAlive++;
        playersExtracted--;

    }

    public void IncreasePlayersAliveAmount()
    {
        playersAlive++;
    }
    public void DecreasePlayersAliveAmount(GameObject player,int playerID)
    {
        playersAlive--;
        playersDead++;
    }
}
