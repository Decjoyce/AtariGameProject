using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] characters;
    public int selectedCharacter = 0;
   public void NextCharacter()
    {
        characters[selectedCharacter].gameObject.SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].gameObject.SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter: ", selectedCharacter);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}
