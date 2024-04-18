using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    #region singlton    
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one <b>GameManager</b> has been found");
            return;
        }
        instance = this;
    }
    #endregion 

    [SerializeField] ScoreManager sm;
    [SerializeField] PlayerManager pm;

    public List<GameObject> playersPlaying = new List<GameObject>();
    public List<GameObject> playersDead = new List<GameObject>();
    public List<GameObject> playersExtracted = new List<GameObject>();

    public Character[][] generatedCharacters;

    public int[] playersLives;

    /*[HideInInspector]*/ public bool hasCaptain;

    int roundsPlayed;

    void Start()
    {        

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            StartRound();
        }
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += PlayerDied;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= PlayerDied;
    }

    public void StartRun()
    {
        GenerateCharactersForAll();
        pm.DisableJoining();
        playersLives = new int[pm.players.Count];
        for(int i = 0; i < playersLives.Length; i++)
        {
            playersLives[i] = 5;
        }
        StartRound();
    }

    void EndRun()
    {
        roundsPlayed = 0;
        //pm.ClearPlayers();
        Destroy(transform.parent.gameObject);
        SceneManager.LoadScene(0);
    }

    public void StartRound()
    {
        for(int i = 0; i < pm.players.Count; i++)
        {
            int ranClass = Random.Range(0, 5);
            pm.players[i].GetComponent<PlayerController>().SetUpCharacter(generatedCharacters[i][ranClass], ranClass);
        }

        SceneManager.LoadScene(1);
        pm.ResetPlayers();
    }

    void EndRound()
    {
        CheckIfLivesLeft();
        roundsPlayed++;
        if(roundsPlayed % 3 == 0)
        {
            if (!sm.QuotaCheck())
                EndRun();
            else
            {
                sm.GetNewQuota();
                //GenerateNewCharacter();
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ExtractPlayer(GameObject player)
    {
        if(!playersDead.Contains(player) && !playersExtracted.Contains(player))
        {
            playersExtracted.Add(player);
        }
        CheckIfAllExtracted();
    }

    public void ExtractAllPlayers()
    {
        foreach(GameObject player in playersPlaying)
        {
            if (!playersDead.Contains(player) && !playersExtracted.Contains(player))
            {
                playersExtracted.Add(player);
            }
        }
        EndRound();
    }

    void CheckIfAllExtracted()
    {
        if(playersExtracted.Count == pm.players.Count - playersDead.Count)
        {
            EndRound();
        }
    }

    public void PlayerDied(GameObject player, int playerNum)
    {
        playersLives[playerNum]--;
        generatedCharacters[playerNum][player.GetComponent<PlayerController>().charNum].isDead = true;
        CheckIfAllDead();
    }

    void CheckIfAllDead()
    {

        if(playersDead.Count == pm.players.Count - playersExtracted.Count)
        {
            EndRound();
        }
    }

    void CheckIfLivesLeft()
    {
        int playersWithNoLives = 0;
        for(int i = 0; i < playersLives.Length; i++)
        {
            if (playersLives[i] == 0)
            {
                playersWithNoLives++;
            }
        }
        if (playersWithNoLives == pm.players.Count)
            EndRun();
    }

    void GenerateCharactersForAll()
    {
        Debug.Log("sjkodn");
        generatedCharacters = new Character[pm.players.Count][];
        for (int i = 0; i < generatedCharacters.Length; i++)
        {
            Character char1 = new Character();
            char1.GenerateStats(hasCaptain);
            Character char2 = new Character();
            char2.GenerateStats(hasCaptain);
            Character char3 = new Character();
            char3.GenerateStats(hasCaptain);
            Character char4 = new Character();
            char4.GenerateStats(hasCaptain);
            Character char5 = new Character();
            char5.GenerateStats(hasCaptain);
            generatedCharacters[i] = new Character[] { char1, char2, char3, char4, char5 };
        }

    }

    void GenerateCharacterForOne(int playerNum, int index)
    {

    }
}
