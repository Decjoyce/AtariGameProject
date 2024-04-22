using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Character
{
    //Non Generated Stuff
    public bool beenUsed;
    public bool isDead;
    public WeaponType weapon;
    public int currentAmmo, currentReserve;
    public float health;

    //Generated Stuff
    public int dudesname;
    public string characterClass;
    public PlayerStats proficiencies;
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

        dudesname = Random.Range(-69, 70);

        health = 100;

        proficiencies.GenerateProciencies();
    }
}
