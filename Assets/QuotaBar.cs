using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuotaBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = ScoreManager.instance.quotaAmount;
        slider.value = ScoreManager.instance.currentScore;
    }
}
