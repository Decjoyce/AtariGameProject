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

    public List<List<Character>> playerCharacters = new List<List<Character>>();
    public List<Character> testingList = new List<Character>();

    [SerializeField] int numberOfLives;

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
        if(pm.players.Count > 0)
        {
            GenerateCharactersForAll();
            pm.DisableJoining();
            StartRound();
        }
        else
        {
            Debug.Log("NOT ENOUGH PLAYERS TO BEGIN. PRESS START TO JOIN!!!!!!!");
        }
    }

    void EndRun()
    {
        roundsPlayed = 0;
        Destroy(transform.parent.gameObject);
        SceneManager.LoadScene(0);
    }

    public void StartRound()
    {
        //Temp
        for(int i = 0; i < pm.players.Count; i++)
        {
            int ranClass = Random.Range(0, playerCharacters[i].Count);
            pm.players[i].GetComponent<PlayerController>().SetUpCharacter(playerCharacters[i][ranClass], ranClass);
        }
        //Temp

        playersDead.Clear();
        playersExtracted.Clear();
        SceneManager.LoadScene(1);
        pm.ResetPlayers();
    }

    void EndRound()
    {
        if (!CheckIfLivesLeft())
            return;

        roundsPlayed++;
        if (roundsPlayed % 3 == 0)
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

    #region Extraction Code
    public void ExtractPlayer(GameObject player)
    {
        if(!playersDead.Contains(player) && !playersExtracted.Contains(player))
        {
            playersExtracted.Add(player);
            player.GetComponent<PlayerController>().SaveCharacter();
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
    #endregion

    #region Death Code
    public void PlayerDied(GameObject player, int playerNum)
    {
        //generatedCharacters[playerNum - 1][player.GetComponent<PlayerController>().charNum].isDead = true;
        Debug.Log(player.GetComponent<PlayerController>().charNum);
        playerCharacters[playerNum - 1].RemoveAt(player.GetComponent<PlayerController>().charNum);
        playersDead.Add(player);
        CheckIfAllDead();
    }

    void CheckIfAllDead()
    {
        if(playersDead.Count == pm.players.Count - playersExtracted.Count)
        {
            EndRound();
        }
    }

    bool CheckIfLivesLeft()
    {
        int playersWithNoLives = 0;
        for(int i = 0; i < playerCharacters.Count; i++)
        {
            if (playerCharacters[i].Count == 0)
            {
                playersWithNoLives++;
            }
        }
        Debug.Log(playersWithNoLives);

        if (playersWithNoLives == pm.players.Count)
        {
            EndRun();
            return false;
        }
        else
            return true;
    }
    #endregion

    #region Character Code
    void GenerateCharactersForAll()
    {
        for (int i = 0; i < pm.players.Count; i++)
        {
            List<Character> tempList = new List<Character>();
            for (int j = 0; j < numberOfLives; j++)
            {
                Character tempChar = new Character();
                tempChar.GenerateStats(hasCaptain);
                tempList.Add(tempChar);
            }
            playerCharacters.Add(tempList);
            testingList = tempList;
        }

    }

    void GenerateCharacterForOne(int playerNum, int index)
    {
        if (playerCharacters[playerNum].Count > 5)
        {
            Character tempChar = new Character();
            tempChar.GenerateStats(hasCaptain);
            playerCharacters[playerNum].Add(tempChar);
        }
    }
    
    public void SaveCharacter(Character character,int playerNum, int charNum)
    {
        playerCharacters[playerNum - 1][charNum] = character;
        Debug.Log("Don");
    }
    #endregion
}
