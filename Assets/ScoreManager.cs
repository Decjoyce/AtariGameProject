using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region singlton
    public static ScoreManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one <b>ScoreManager</b> has been found");
            return;
        }
        instance = this;
    }
    #endregion 

    public Image quotaBar;
    public float currentScore = 0f;
    public float quotaAmount = 300f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            IncreaseScore(30);
        }
    }

    public void IncreaseScore(float scrapWorth)
    {
        currentScore += scrapWorth;
        currentScore = Mathf.Clamp(currentScore, 0, quotaAmount);
        //quotaBar.fillAmount = currentScore / currentScore;
    }

    public void GetNewQuota()
    {
        currentScore = 0;
    }

    public bool QuotaCheck()
    {
        return currentScore == quotaAmount;
    }
}
