using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Character_Navigator : Character_Base
{
    bool mapOpened;
 
    public override void Action(PlayerController controller)
    {
        mapOpened = !mapOpened;

        if (mapOpened)
        {
            OpenMap(controller);
        }
        else
        {
            CloseMap(controller);
        }
    }

    private void OpenMap(PlayerController controller)
    {
        Debug.Log("Map Opened");
        controller.nav_map.gameObject.SetActive(true);
    }

    private void CloseMap(PlayerController controller)
    {
        Debug.Log("Map Closed");
        controller.nav_map.gameObject.SetActive(false);
    }
}
