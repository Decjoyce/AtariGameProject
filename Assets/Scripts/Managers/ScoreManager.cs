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

    public float currentScore = 0f;
    public float quotaAmount = 300f;

    public float fakeScore = 0f;

    [SerializeField] Slider[] quotaSliders;

    private void Start()
    {
        foreach (Slider qb in quotaSliders)
        {
            qb.maxValue = quotaAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            IncreaseScore(30);
        }
    }

    public void IncreaseFakeQuota(float worth)
    {
        fakeScore += worth;
        foreach (Slider qb in quotaSliders)
        {
            qb.value = fakeScore;
        }
    }

    public void IncreaseScore(float scrapWorth)
    {
        currentScore += scrapWorth; 
        foreach(Slider qb in quotaSliders)
        {
            qb.value = currentScore;
        }
    }

    public void GetNewQuota()
    {
        quotaAmount += 300f;
        foreach (Slider qb in quotaSliders)
        {
            qb.maxValue = quotaAmount;
        }
    }

    public bool QuotaCheck()
    {
        return currentScore == quotaAmount;
    }
}
