using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExtractionScript : Interactable
{

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        player.controller.SwitchState("EXTRACTED");
    }

    //public GameObject ScoreManagerObject;
    //ScoreManager scoreScript;
    //public int levelsCleared;

    //private void Start()
    //{
    //    scoreScript = ScoreManagerObject.GetComponent<ScoreManager>();
    //}
    //public void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Player") 
    //    {
    //        if(Input.GetKeyDown(KeyCode.E)) 
    //        {
    //           // levelsCleared++;
    //           //PlayerPrefs.SetInt("levelsCleared", levelsCleared);
    //            //May have to add this to player script so it doesn't have each player extracting counting as 2 level clears
    //            //May also need to add this to the player script itself so each player is taken individually to the extraction scene 
    //            //rather than all getting extracted due to one person extracting
    //            other.GetComponent<PlayerController>().IsExtracted();

    //            if(levelsCleared > 2)
    //            {
    //                scoreScript.QuotaCheck();
    //            }
    //        }
    //    }
    //}




}
