using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool locked;
    [SerializeField] float doorOffset;
    [SerializeField] Renderer sign;
    [SerializeField] Material openMat;
    Animator anim;
    bool doorOpen;
    int pplAtDoor;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {        
            if(!locked && !doorOpen)
                OpenDoor();

            pplAtDoor++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pplAtDoor--;

            if (pplAtDoor == 0)
                CloseDoor();
        }
    }

    void OpenDoor()
    {
        anim.SetBool("doorOpen", true);
    }

    void CloseDoor()
    {
        anim.SetBool("doorOpen", false);
    }

    public void UnlockDoor()
    {
        locked = false;
        sign.material = openMat;
        if (pplAtDoor > 0)
            OpenDoor();
    }
}
