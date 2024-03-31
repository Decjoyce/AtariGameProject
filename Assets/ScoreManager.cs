using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Image quotaBar;
    public float quotaAmount = 0f;

    // Update is called once per frame
    void Update()
    { /*
        if (quotaAmount > 300)
        {
            SceneManager.LoadScene("JamesScene", LoadScene.Additive);
        } */
        if(Input.GetKeyUp(KeyCode.KeypadEnter))
            {
            IncreaseScore(30);
            }
    }

    public void IncreaseScore(float scrapWorth)
    {
        quotaAmount += scrapWorth;
        scrapWorth = Mathf.Clamp(scrapWorth, 0, 300);
        quotaBar.fillAmount = quotaAmount / 300f;
    }
}
