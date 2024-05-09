using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_ships, text_score, text_time;

    // Start is called before the first frame update
    void Start()
    {
        text_time.text = "Time: " + GameManager.instance.currentTime;
        text_ships.text = "Ships Looted: " + GameManager.instance.roundsPlayed;
        text_score.text = "Total Made: " + ScoreManager.instance.currentScore;
    }

    public void ReturnToMenu()
    {
        GameManager.instance.EndRun();
    }
}
