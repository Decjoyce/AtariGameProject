using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon List", menuName = "Weapons/Weapon List", order = 0)]
public class SO_AllWeapons : ScriptableObject
{
    public WeaponType[] weapons;
    public float[] chances2Spawn;

    public WeaponType GetRandomWeapon()
    {
        float total = 0;

            foreach (float elem in chances2Spawn)
            {
                total += elem;
            }


        float randomPoint = Random.value * total;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (randomPoint < chances2Spawn[i])
            {
                return weapons[i];
            }
            else
            {
                randomPoint -= chances2Spawn[i];
            }
        }
        return weapons[weapons.Length - 1];
    }
}
