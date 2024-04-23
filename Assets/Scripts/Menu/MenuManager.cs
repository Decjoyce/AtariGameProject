using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public ClassUISO classUI;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject playerJoin;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void Playgame()
    {
        mainMenu.SetActive(false);
        playerJoin.SetActive(true);
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void retry()
    {

    }

    #region singlton    
    public static MenuManager instance;
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
