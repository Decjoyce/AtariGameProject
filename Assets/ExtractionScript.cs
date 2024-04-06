using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExtractionScript : MonoBehaviour
{
    public GameObject ScoreManagerObject;
    ScoreManager scoreScript;
    public int levelsCleared;
    
    private void Start()
    {
        scoreScript = ScoreManagerObject.GetComponent<ScoreManager>();
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                levelsCleared++;
                PlayerPrefs.SetInt("levelsCleared", levelsCleared);
                if(levelsCleared > 2)
                {
                    scoreScript.QuotaCheck();
                }
            }
        }
    }
}
