using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public int SceneIndex;
    public float loadDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(NextScene), loadDelay);
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
