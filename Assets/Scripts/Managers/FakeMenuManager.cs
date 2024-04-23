using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMenuManager : MonoBehaviour
{
    #region singlton    
    public static FakeMenuManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one <b>FakeMenuManager</b> has been found");
            return;
        }
        instance = this;
    }
    #endregion 

    [SerializeField] CharacterCard[] characterCards;

    private void Start()
    {
        PlayerManager.instance.PlayersToCharacterSelect();
    }

    public void ChangeDisplayCard(int playerNum, int charNum)
    {
        characterCards[playerNum - 1].SetDisplay(GameManager.instance.playerCharacters[playerNum - 1][charNum]);
    }
}
