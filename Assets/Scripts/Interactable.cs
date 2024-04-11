using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canInteract;
    public string interactPrompt;
    public string switch2State;

    public virtual void Interaction(PlayerInteraction player)
    {
        Debug.Log(player.name + " Interacted with " + name);
    }
}
