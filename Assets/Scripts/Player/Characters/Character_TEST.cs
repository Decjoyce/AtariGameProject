using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_TEST : Character_Base
{
    public override void Action(PlayerController controller)
    {
        Debug.Log("AHHHHHHHHHHH ");
        controller.HelpStartCoroutine(DoSomething());
    }

    IEnumerator DoSomething()
    {
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log("YOOOO ");
    }

}
