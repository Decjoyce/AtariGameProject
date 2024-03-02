using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon_Base : ScriptableObject
{
    [Header("Defaults")]
    public string weaponName;
    public int damage;
    public GameObject weaponModel;
    public string holdType;

    public abstract void Attack(PlayerController controller);
}
