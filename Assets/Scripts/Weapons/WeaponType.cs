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
    public string holdType;
    public GameObject projectile;
    public GameObject weaponModel;

    [Header("Stats")]
    public float fireRate;
    public float reloadSpeed;
    public int currentAmmo;
    public int magCapacity;
    public int reserveCapacity;

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