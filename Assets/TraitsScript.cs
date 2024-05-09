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
            if(pc.playerStats.weaponSpreadMod > -2)
            {
                pc.playerStats.weaponSpreadMod = 0;
            }
            else
            {
                pc.playerStats.weaponSpreadMod = pc.playerStats.weaponSpreadMod = pc.playerStats.weaponSpreadMod + pc.playerStats.introvertedBoon;
            }
            pc.playerStats.jumpPowerMod = pc.playerStats.jumpPowerMod + pc.playerStats.introvertedBoon;
            pc.playerStats.moveMod = pc.playerStats.moveMod + pc.playerStats.introvertedBoon;
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
                pc.playerStats.moveMod = pc.playerStats.moveMod + pc.playerStats.clingyBoon;
                if(pc.playerStats.weaponSpreadMod > -2)
                {
                    pc.playerStats.weaponSpreadMod = 0;
                }
                else
                {
                    pc.playerStats.weaponSpreadMod = pc.playerStats.weaponSpreadMod + pc.playerStats.clingyBoon;
                }
                pc.playerStats.jumpPowerMod = pc.playerStats.jumpPowerMod + pc.playerStats.clingyBoon;
            }
            if (pc.playerStats.isIntroverted)
            {
                pc.playerStats.hackSpeedMod = pc.playerStats.hackSpeedMod - (pc.playerStats.introvertedPenalty);
                pc.playerStats.moveMod = (pc.playerStats.moveMod - (pc.playerStats.introvertedPenalty));
                pc.playerStats.weaponSpreadMod = (pc.playerStats.weaponSpreadMod - (pc.playerStats.introvertedPenalty));
                pc.playerStats.jumpPowerMod = (pc.playerStats.jumpPowerMod - (pc.playerStats.introvertedPenalty));

            }

            Debug.Log("bleh"); 
            Debug.Log("Hack Speed: " + pc.playerStats.hackSpeedMod);
            Debug.Log("Move Speed: " + pc.playerStats.moveMod);
            Debug.Log("Weapon Spread: " + pc.playerStats.weaponSpreadMod);
            Debug.Log("Jump Power: " + pc.playerStats.jumpPowerMod);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && pc.playerNum != other.GetComponent<PlayerController>().playerNum)
        {
            if (pc.playerStats.isClingy)
            {
                pc.playerStats.hackSpeedMod = pc.playerStats.hackSpeedMod - pc.playerStats.clingyPenalty;
                pc.playerStats.moveMod = pc.playerStats.moveMod - pc.playerStats.clingyPenalty;
                pc.playerStats.weaponSpreadMod = pc.playerStats.weaponSpreadMod + pc.playerStats.clingyPenalty;
                pc.playerStats.hackSpeedMod = pc.playerStats.hackSpeedMod - pc.playerStats.clingyPenalty;
            }

            if (pc.playerStats.isIntroverted)
            {
                pc.playerStats.hackSpeedMod = pc.playerStats.hackSpeedMod + (pc.playerStats.introvertedPenalty);
                pc.playerStats.moveMod = pc.playerStats.moveMod + (pc.playerStats.introvertedPenalty);
                pc.playerStats.weaponSpreadMod = pc.playerStats.weaponSpreadMod + (pc.playerStats.introvertedPenalty);
                pc.playerStats.jumpPowerMod = pc.playerStats.jumpPowerMod + (pc.playerStats.introvertedPenalty);
            }

            
            Debug.Log("Hack Speed: " + pc.playerStats.hackSpeedMod);
            Debug.Log("Move Speed: " + pc.playerStats.moveMod);
            Debug.Log("Weapon Spread: " + pc.playerStats.weaponSpreadMod);
            Debug.Log("Jump Power: " + pc.playerStats.jumpPowerMod);
        }
    }
}
