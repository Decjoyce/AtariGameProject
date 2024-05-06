using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<PlayerInput> playersPlaying = new List<PlayerInput>();
    public List<PlayerInput> playersActive = new List<PlayerInput>();
    public List<GameObject> playersDead = new List<GameObject>();
    public List<GameObject> playersExtracted = new List<GameObject>();

    public List<List<Character>> playerCharacters = new List<List<Character>>();
    public List<Character> testingList = new List<Character>();
    public List<Character> testingList2 = new List<Character>();

    [SerializeField] int numberOfLives;

    /*[HideInInspector]*/ public bool hasCaptain;

    int roundsPlayed;

    [SerializeField] int[] randomLevels;
    public int currentLevel;

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
            playersPlaying = pm.players.ToList();
            LoadScene_Loading();
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
        for(int i = 0; i < playersPlaying.Count; i++)
        {
            if (playerCharacters[i].Count > 0)
            {
                int ranClass = Random.Range(0, playerCharacters[i].Count);
                playersPlaying[i].GetComponent<PlayerController>().SetUpCharacter(playerCharacters[i][ranClass], ranClass);
            }
        }
        //Temp

        playersDead.Clear();
        playersExtracted.Clear();
        playersActive = playersPlaying.ToList();
        GetRandomLevel();
        pm.ResetPlayers();
    }

    void EndRound()
    {
        //Check if players stills have characters to play as
        if (!CheckIfLivesLeft())
            return;

        roundsPlayed++;

        //Checks if its been 3 rounds
        if (roundsPlayed % 3 == 0) //if it has been 3 rounds
        {
            //Checks if quota was reached 
            if (!sm.QuotaCheck())
            {
                Debug.Log("Did not make quota");
                EndRun(); //Ends the the run
            }

            else
            {
                //Gets new quota, generates a new character for each player, loads the character select scene
                sm.GetNewQuota();
                //GenerateNewCharacter();
                LoadScene_Loading();
            }
        }
        else //if it hasnt
        {
            LoadScene_Loading();            //loads the character select scene
        }
    }

    void GetRandomLevel()
    {
        currentLevel = randomLevels[Random.Range(0, randomLevels.Length)];
        SceneManager.LoadScene(currentLevel);
    }

    public void LoadScene_Loading()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene_CharacterSelect()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadScene_Level()
    {
        SceneManager.LoadScene(currentLevel);
    }

    #region Extraction Code
    public void ExtractPlayer(GameObject player)
    {
        if(!playersDead.Contains(player) && !playersExtracted.Contains(player))
        {
            playersExtracted.Add(player);
            playersActive.Remove(player.GetComponent<PlayerInput>());
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.SaveCharacter();
            pc.interaction.ConvertItemsToScore();
        }
        CheckIfAllExtracted();
    }

    public void ExtractAllPlayers()
    {
        foreach(PlayerInput player in playersPlaying)
        {
            if (!playersDead.Contains(player.gameObject) && !playersExtracted.Contains(player.gameObject))
            {
                playersExtracted.Add(player.gameObject);
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
        Debug.Log(playerCharacters[playerNum - 1].Count);
        playerCharacters[playerNum - 1].RemoveAt(player.GetComponent<PlayerController>().charNum);

        playersDead.Add(player);
        playersActive.Remove(player.GetComponent<PlayerInput>());

        CheckIfAllDead();
    }

    void CheckIfAllDead()
    {
        if(playersDead.Count == playersPlaying.Count - playersExtracted.Count)
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

        for (int i = 0; i < playersPlaying.Count; i++)
        {
            if (playerCharacters[i].Count == 0)
            {
                playersPlaying[i].GetComponent<PlayerController>().isOut = true;
                playersPlaying.RemoveAt(i);
            }
        }

       // Debug.Log(playersWithNoLives);

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

    public void SelectCharacter(int playerNum, int charNum)
    {
        pm.players[playerNum - 1].GetComponent<PlayerController>().SetUpCharacter(playerCharacters[playerNum - 1][charNum], charNum);
    }

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
            if (i == 0)
                testingList = tempList;
            else
                testingList2 = tempList;
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
