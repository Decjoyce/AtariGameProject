using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Interactable
{
    public WeaponType weapon;

    int currentAmmo;
    int currentReserve;

    bool hasBeenUsed;

    private void Start()
    {
        if(!hasBeenUsed)
        {
            currentAmmo = weapon.magCapacity;
            currentReserve = weapon.reserveCapacity;
            interactPrompt = "Pick up " + weapon.weaponName;
            Instantiate(weapon.weaponModel, transform);
        }
        else
        {

        }
    }

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        player.attack.PickUpWeapon(weapon, currentAmmo, currentReserve);
        Destroy(gameObject);
    }

    public void ChangeStats(WeaponType newWeap, int newAmmo, int newReserve)
    {
        hasBeenUsed = true;
        weapon = newWeap;
        currentAmmo = newAmmo;
        currentReserve = newReserve;
        interactPrompt = "Pick up " + newWeap.weaponName;
    }

}
