using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    PlayerController controller;

    public Weapon currentWeapon;
    public WeaponType testWeap;

    [SerializeField] Transform firePoint;

    bool canAttack = true, isReloading;
    bool isAutoFiring;
    float attackDelay;

    private Coroutine currentReloadCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
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

    public void Attack(InputAction.CallbackContext ctx)
    {
        switch (currentWeapon.weapon.attackType)
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
                Debug.LogError("ERROR: the AttackType - " + currentWeapon.weapon.attackType.ToString() + " - is invalid");
                break;
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Relaoding");
        isReloading = true;
        yield return new WaitForSecondsRealtime(currentWeapon.weapon.reloadSpeed);
        currentWeapon.currentAmmo = currentWeapon.weapon.magCapacity;
        isReloading = false;
    }

    public void PickUpWeapon(WeaponType weapon)
    {
        StopCoroutine(currentReloadCoroutine);
        isAutoFiring = false;
        isReloading = false;
        canAttack = true;
        attackDelay = 0;
        currentWeapon.weapon = testWeap;
    }

    private void SingleShot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isReloading && canAttack)
        {
            Instantiate(currentWeapon.weapon.projectile, firePoint.position, firePoint.rotation);
            canAttack = false;
            attackDelay = currentWeapon.weapon.fireRate;
            currentWeapon.currentAmmo--;
            if (currentWeapon.currentAmmo == 0)
                currentReloadCoroutine = StartCoroutine(Reload());
        }
    }

    private void AutoShot()
    {
        if (canAttack && !isReloading)
        {
            Instantiate(currentWeapon.weapon.projectile, firePoint.position, firePoint.rotation);

            canAttack = false;
            attackDelay = currentWeapon.weapon.fireRate;

            currentWeapon.currentAmmo--;
            if (currentWeapon.currentAmmo == 0)
                currentReloadCoroutine = StartCoroutine(Reload());
        }
    }

    private void SwingMelee(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canAttack)
        {
            Instantiate(currentWeapon.weapon.projectile, firePoint.position, firePoint.rotation);

            canAttack = false;
            attackDelay = currentWeapon.weapon.fireRate;
        }
    }

    private void ThrustMelee(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canAttack)
        {
            Instantiate(currentWeapon.weapon.projectile, firePoint.position, firePoint.rotation);

            canAttack = false;
            attackDelay = currentWeapon.weapon.fireRate;
        }
    }

}