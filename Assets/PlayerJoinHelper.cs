using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerJoinHelper : MonoBehaviour
{
    [SerializeField] GameObject joinText, startGame;
    [SerializeField] TextMeshProUGUI p1_text, p2_text;

    private void OnEnable()
    {
        PlayerManager.OnPlayerHasJoined += PlayerHasJoined;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerHasJoined -= PlayerHasJoined;
    }

    public void PlayerHasJoined(int id)
    {
        joinText.SetActive(false);
        startGame.SetActive(true);
        if (id == 1)
        {
            p1_text.text = "JOINED";
        }
        if(id == 2)
        {
            p2_text.text = "JOINED";
        }
    }
}
