using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Image quotaBar;
    public float quotaAmount = 0f;
    public float totalAmount = 300f;

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
        scrapWorth = Mathf.Clamp(scrapWorth, 0, totalAmount);
        quotaBar.fillAmount = quotaAmount / totalAmount;
    }

    public void QuotaCheck()
    {
        if(quotaAmount < totalAmount)
        {
            DidNotMeetQuota();
        }

        else if (quotaAmount >= totalAmount)
        {
            QuotaMade();
        }
    }

    public void DidNotMeetQuota()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
        Debug.Log("Quota Not Met");
    }

   public  void QuotaMade()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
        Debug.Log("Quota Met");
    }
}
