using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    PlayerController controller;

    [SerializeField] Transform firePoint;
    [SerializeField] MeshRenderer weaponMesh; //Temp

    WeaponType weapon;
    [SerializeField] WeaponType defaultWeapon;
    int currentAmmo, currentReserve;
    bool canAttack = true, isReloading;
    bool isAutoFiring;
    float attackDelay;

    [SerializeField] GameObject droppedWeaponPrefab;

    private Coroutine currentReloadCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();

        if (weapon == null)
            weapon = defaultWeapon;

        currentAmmo = weapon.magCapacity;
        currentReserve = weapon.reserveCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canAttack && attackDelay <= 0)
        {
            canAttack = true;
        }
        else
        {
            attackDelay -= Time.deltaTime;
        }

        if (isAutoFiring)
        {
            AutoShot();
        }
    }

    public void PickUpWeapon(WeaponType newWeapon, int newAmmo, int newReserve)
    {
        if (currentReloadCoroutine != null)
            StopCoroutine(currentReloadCoroutine);

        isAutoFiring = false;
        isReloading = false;
        canAttack = true;
        attackDelay = 0;

        GameObject droppedWeapon = Instantiate(droppedWeaponPrefab, firePoint.position, Quaternion.identity);
        droppedWeapon.GetComponent<WeaponPickup>().ChangeStats(weapon, currentAmmo, currentReserve);

        weapon = newWeapon;
        currentAmmo = newAmmo;
        currentReserve = newReserve;

        weaponMesh.material.SetColor("_EmissionColor", Color.blue); //temp

        if (currentAmmo == 0)
        {
            weaponMesh.material.SetColor("_EmissionColor", Color.red); //temp
            currentReloadCoroutine = StartCoroutine(Reload());
        }

    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        switch (weapon.attackType)
        {
            case AttackType.single:
                SingleShot(ctx);
                break;
            case AttackType.auto:
                if (ctx.started)
                    isAutoFiring = true;
                 else if (ctx.canceled)
                    isAutoFiring = false;
                break;
            case AttackType.swing:
                SwingMelee(ctx);
                break;
            case AttackType.thrust:
                SwingMelee(ctx);
                break;
            default:
                Debug.LogError("ERROR: the AttackType - " + weapon.attackType.ToString() + " - is invalid");
                break;
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Relaoding");
        isReloading = true;
        yield return new WaitForSecondsRealtime(weapon.reloadSpeed);

        if(currentReserve > weapon.magCapacity)
        {
            currentAmmo = weapon.magCapacity;
            currentReserve -= weapon.magCapacity;
        }
        else
        {
            currentAmmo = currentReserve;
            currentReserve = 0;
        }

        weaponMesh.material.SetColor("_EmissionColor", Color.blue); //temp

        isReloading = false;
    }

    private void SingleShot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isReloading && canAttack && currentAmmo > 0)
        {
            Instantiate(weapon.projectile, firePoint.position, firePoint.rotation);
            canAttack = false;
            attackDelay = weapon.fireRate;
            currentAmmo--;
            if (currentAmmo == 0)
            {
                weaponMesh.material.SetColor("_EmissionColor", Color.red); //temp
                if (currentReserve > 0)
                    currentReloadCoroutine = StartCoroutine(Reload());
            }
        }
    }

    private void AutoShot()
    {
        if (canAttack && !isReloading && currentAmmo > 0)
        {
            Instantiate(weapon.projectile, firePoint.position, firePoint.rotation);

            canAttack = false;
            attackDelay = weapon.fireRate;

            currentAmmo--;
            if (currentAmmo == 0)
            {
                weaponMesh.material.SetColor("_EmissionColor", Color.red); //temp
                if (currentReserve > 0)
                    currentReloadCoroutine = StartCoroutine(Reload());
            }

        }
    }

    private void SwingMelee(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canAttack)
        {
            Instantiate(weapon.projectile, firePoint.position, firePoint.rotation);

            canAttack = false;
            attackDelay = weapon.fireRate;
        }
    }

    private void ThrustMelee(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canAttack)
        {
            Instantiate(weapon.projectile, firePoint.position, firePoint.rotation);

            canAttack = false;
            attackDelay = weapon.fireRate;
        }
    }

}
