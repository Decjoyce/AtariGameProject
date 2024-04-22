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
        GameManager.instance.ExtractPlayer(player.gameObject);
        Destroy(gameObject);
    }
}
