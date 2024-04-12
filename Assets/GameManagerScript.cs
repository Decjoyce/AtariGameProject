using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int playersAlive;
    public int playersDead;
    public int playersExtracted;
    //[System.NonSerialized] public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playersAlive = 0;
        playersDead = 0;
        playersExtracted = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playersAlive < 1 && playersExtracted < 1) 
        {
            Debug.Log("Everyone is dead");
        }
        if(playersAlive < 1 && playersExtracted > 0)
        {
            Debug.Log("Part of the crew escaped");
        }
        if (playersAlive > 0 && playersExtracted >0)
        {
            Debug.Log("Waiting on other players to extract");
        }
        if(playersDead < 1 && playersExtracted > 0 && playersAlive < 1)
        {
            Debug.Log("Everyone escaped");
        }
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
    public void DecreasePlayersAliveAmount()
    {
        playersAlive--;
        playersDead++;
    }
}
