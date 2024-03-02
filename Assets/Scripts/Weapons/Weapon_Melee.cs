using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Melee : Weapon_Base
{
    [Header("Stats")]
    public float swingSpeed;

    [Header("Aesthetics")]
    public AudioClip swingSound;
    public AudioClip impactSound;



    private GameObject weapon;

    public override void Attack(PlayerController controller)
    {

    }
}



[CreateAssetMenu(fileName = "Weap_Melee_NAME", menuName = "Weapon/Melee/Slash")]
public class Weapon_Melee_Single : Weapon_Melee
{

}



[CreateAssetMenu(fileName = "Weap_Melee_NAME", menuName = "Weapon/Melee/Auto")]
public class Weapon_Melee_Auto : Weapon_Melee
{

}
