using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController controller;

    private List<Interactable> availableInteractions = new List<Interactable>();

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }


}
