using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/New Weapon")]
public class WeaponType: ScriptableObject
{
    [Header("Defaults")]
    public string weaponName;
    public AttackType attackType;
    public HoldType holdType;
    public GameObject weaponModel;

    [Header("General Stats")]
    public float fireRate;

    [Header("Projectile Stats")]
    public GameObject projectile;
    public float reloadSpeed;
    public int magCapacity;
    public int reserveCapacity;

    [Header("Melee Stats")]
    public float meleeDamage;
    public float meleeArc;
    public float meleeRange;
    public float radius;
    public float speed;

    [Header("Aesthetics")]
    public AudioClip fireSound;
    public AudioClip impactSound;
}

    public enum AttackType
    {
        single,
        auto,
        swing,
        thrust
    }

    public enum HoldType
    {
        gunOneHanded,
        gunTwoHanded,
        meleeSwing,
        meleeThrust
    }