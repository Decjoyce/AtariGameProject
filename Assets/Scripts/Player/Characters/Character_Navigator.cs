using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Navigator : Character_Base
{
    bool mapOpened;
 
    public override void Action(PlayerController controller)
    {
        mapOpened = !mapOpened;

        if (mapOpened)
        {
            OpenMap();
        }
        else
        {
            CloseMap();
        }
    }

    private void OpenMap()
    {
        
    }

    private void CloseMap()
    {

    }
}
