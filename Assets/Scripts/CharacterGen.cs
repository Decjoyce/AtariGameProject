using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Character
{
    //Non Generated Stuff
    public bool isDead;
    public Weapon weapon;

    //Generated Stuff
    public string characterClass;
    //Proficiencies
    //Traits

    public void GenerateStats(bool hasCap)
    {
        int ranClass;
        if (!hasCap)
            ranClass = Random.Range(0, 5);
        else
            ranClass = Random.Range(1, 5);
        switch (ranClass)
        {
            case 0:
                characterClass = "CAPTAIN";
                GameManager.instance.hasCaptain = true;
                break;            
            case 1:
                characterClass = "ENGINEER";
                break;
            case 2:
                characterClass = "DOCTOR";
                break;
            case 3:
                characterClass = "NAVIGATOR";
                break;
            case 4:
                characterClass = "CREWMATE";
                break;
            default:
                Debug.Log("you stupid idiot, the random range is wrong stupid idiot");
                break;
        }
    }
}
