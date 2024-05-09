using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Captain : Character_Base
{
    Coroutine evacedCoroutine;
    bool beganEvacuation;
    public override void Action(PlayerController controller)
    {
        if(!beganEvacuation)
            evacedCoroutine = controller.HelpStartCoroutine(StartEvac());
        else
        {
            if (evacedCoroutine != null)
            {
                controller.HelpStopCoroutine(evacedCoroutine);
                evacedCoroutine = null;
            }
            beganEvacuation = false;
            Debug.Log("Stopped Evac");
            //Exit Animation
        }
    }

    IEnumerator StartEvac()
    {
        Debug.Log("EVACUATION BEGINS");
        //Enter Animation
        beganEvacuation = true;
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log("EVACED");
        GameManager.instance.ExtractAllPlayers();
        beganEvacuation = false;
        //Exit Animation
    }

}
