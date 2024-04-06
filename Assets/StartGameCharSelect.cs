using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameCharSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGameAfterCharSelect()
    {
        SceneManager.LoadScene(1);
        Debug.Log("The game should start now");
    }

}
