using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TraitsScript : MonoBehaviour
{
    public PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        if(pc.playerStats.isIntroverted)
        {
            pc.playerStats.hackSpeedMod = pc.playerStats.hackSpeedMod + pc.playerStats.introvertedBoon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && pc.playerNum != other.GetComponent<PlayerController>().playerNum)
        {
            if (pc.playerStats.isClingy)
            {
                pc.playerStats.hackSpeedMod = pc.playerStats.hackSpeedMod + pc.playerStats.clingyBoon;
            }
            if (pc.playerStats.isIntroverted)
            {
                pc.playerStats.hackSpeedMod = pc.playerStats.hackSpeedMod - (pc.playerStats.introvertedPenalty);
            }

            Debug.Log("bleh");
            Debug.Log(pc.playerStats.hackSpeedMod);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && pc.playerNum != other.GetComponent<PlayerController>().playerNum)
        {
            if (pc.playerStats.isClingy)
            {
                pc.playerStats.hackSpeedMod = pc.playerStats.hackSpeedMod - pc.playerStats.clingyBoon;
            }

            if (pc.playerStats.isIntroverted)
            {
                pc.playerStats.hackSpeedMod = pc.playerStats.hackSpeedMod + (pc.playerStats.introvertedPenalty);
            }


            Debug.Log(pc.playerStats.hackSpeedMod);
        }
    }
}
