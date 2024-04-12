using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject characterSelect;

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
        characterSelect.SetActive(true);
    }

    public void quitToMainMenu()
    {

    }

    public void retry()
    {

    }
}
