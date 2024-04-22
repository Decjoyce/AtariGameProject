using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoundButton : MonoBehaviour
{
    public void HelpStartRound()
    {
        GameManager.instance.StartRound();
    }
}
