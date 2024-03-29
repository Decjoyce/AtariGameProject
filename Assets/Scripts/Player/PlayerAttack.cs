using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    PlayerController controller;
    AudioSource source;

    [SerializeField] Transform firePoint;
    [SerializeField] MeshRenderer weaponMesh; //Temp

    WeaponType weapon;
    [SerializeField] WeaponType defaultWeapon, fists;
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
        source = GetComponent<AudioSource>();

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
        DropWeapon();

        weapon = newWeapon;
        currentAmmo = newAmmo;
        currentReserve = newReserve;

        weaponMesh.material.SetColor("_EmissionColor", Color.blue); //temp

        if (currentAmmo == 0)
        {
            weaponMesh.material.SetColor("_EmissionColor", Color.red); //temp
            if(currentReserve != 0)
                currentReloadCoroutine = StartCoroutine(Reload());
        }

    }

    public void DropWeapon(bool returnToFists = false)
    {
        if (currentReloadCoroutine != null)
            StopCoroutine(currentReloadCoroutine);

        isAutoFiring = false;
        isReloading = false;
        canAttack = true;
        attackDelay = 0;

        if(weapon != fists)
        {
            GameObject droppedWeapon = Instantiate(droppedWeaponPrefab, firePoint.position, Quaternion.identity);
            droppedWeapon.GetComponent<WeaponPickup>().ChangeStats(weapon, currentAmmo, currentReserve);
        }

        if (returnToFists)
        {
            weapon = fists;
            currentAmmo = 420;
            currentReserve = 69;
            weaponMesh.material.SetColor("_EmissionColor", Color.yellow); //temp
            Debug.Log("Fistacuffs");
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
                ThrustMelee(ctx);
                break;
            default:
                Debug.LogError("ERROR: the AttackType - " + weapon.attackType.ToString() + " - is invalid");
                break;
        }
    }

    public void PerformReload()
    {
        if(weapon.reloadSpeed > 0 && currentAmmo < weapon.magCapacity)
        {
            Debug.Log("Reloading");
            weaponMesh.material.SetColor("_EmissionColor", Color.red); //temp
            if (currentReserve > 0)
                currentReloadCoroutine = StartCoroutine(Reload());
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

            source.PlayOneShot(weapon.fireSound);

            currentAmmo--;
            if (currentAmmo == 0)
            {
                PerformReload();
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

            source.PlayOneShot(weapon.fireSound);

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
            Collider[] hits;
            hits = Physics.OverlapCapsule(firePoint.position, firePoint.localPosition + (Vector3.up * weapon.meleeRange), weapon.radius);

            for(int i = 0; i < hits.Length; i++)
            {
                EnemyHealthTest enemyHealth = hits[i].transform.GetComponent<EnemyHealthTest>();

                if (enemyHealth != null)
                    enemyHealth.TakeDamage(weapon.meleeDamage);
            }

            source.PlayOneShot(weapon.fireSound);

            canAttack = false;
            attackDelay = weapon.fireRate;
        }
    }

    private void ThrustMelee(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canAttack)
        {
            Collider[] hits;
            hits = Physics.OverlapCapsule(firePoint.position, firePoint.localPosition + (Vector3.up * weapon.meleeRange), weapon.radius);

            for (int i = 0; i < hits.Length; i++)
            {
                EnemyHealthTest enemyHealth = hits[i].transform.GetComponent<EnemyHealthTest>();

                if (enemyHealth != null)
                {
                    Debug.Log("HIT " + hits[i].gameObject.name);
                    enemyHealth.TakeDamage(weapon.meleeDamage);
                }
            }

            source.PlayOneShot(weapon.fireSound);

            canAttack = false;
            attackDelay = weapon.fireRate;
        }
    }

}
