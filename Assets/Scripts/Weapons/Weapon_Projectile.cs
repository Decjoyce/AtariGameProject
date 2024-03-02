using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon_Projectile : Weapon_Base
{
    [Header("Stats")]
    public float fireRate;
    public float reloadSpeed;
    public int magCapacity;
    public int reserveCapacity;

    [Header("Aesthetics")]
    public AudioClip fireSound;
    public AudioClip impactSound;


    public override void Attack(PlayerController controller)
    {

    }
}



[CreateAssetMenu(fileName = "Weap_Projectile_NAME", menuName = "Weapon/Projectile/SingleShot")]
public class Weapon_Projectile_Single : Weapon_Projectile
{

}



[CreateAssetMenu(fileName = "Weap_Projectile_NAME", menuName = "Weapon/Projectile/Auto")]
public class Weapon_Projectile_Auto : Weapon_Projectile
{

}