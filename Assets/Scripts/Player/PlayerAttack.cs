using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    PlayerController controller;
    AudioSource source;

    [SerializeField] Transform firePoint;
    [SerializeField] Transform handPos;
    [SerializeField] Transform pivot;
    
    GameObject weaponMesh;
    MeshRenderer ammoGraphics; //Temp

    WeaponType weapon;
    [SerializeField] WeaponType defaultWeapon, fists;
    int currentAmmo, currentReserve;
    bool canAttack = true, isReloading;
    bool isAutoFiring;
    float attackDelay;

    [SerializeField] Gradient ammoColorGradient;

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
        SetWeaponMesh();
        SetGunColor();
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

        SetWeaponMesh();

        SetGunColor();

        if (currentAmmo == 0)
        {
            ammoGraphics.material.SetColor("_EmissionColor", Color.red);
            if (currentReserve != 0)
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
            GameObject droppedWeaponModel = Instantiate(weaponMesh, droppedWeapon.transform);
            droppedWeaponModel.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = ammoGraphics.material;
        }

        if (returnToFists)
        {
            weapon = fists;
            currentAmmo = 420;
            currentReserve = 69;
            ammoGraphics.material.SetColor("_EmissionColor", Color.yellow); //temp
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
            ammoGraphics.material.SetColor("_EmissionColor", Color.red);
            if (currentReserve > 0)
                currentReloadCoroutine = StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
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

        SetGunColor();

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

            SetGunColor();

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

            SetGunColor();

            if (currentAmmo == 0)
            {
                PerformReload();
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
                EnemyHealth enemyHealth = hits[i].transform.GetComponent<EnemyHealth>();

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
                EnemyHealth enemyHealth = hits[i].transform.GetComponent<EnemyHealth>();

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

    void SetWeaponMesh()
    {
        if(weaponMesh != null)
        {
            ammoGraphics = null;
            Destroy(weaponMesh);
        }

        weaponMesh = Instantiate(weapon.weaponModel, handPos.position, pivot.rotation, pivot);
        ammoGraphics = weaponMesh.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>();
        firePoint.position = weaponMesh.transform.GetChild(0).GetChild(0).position;
    }

    void SetGunColor()
    {
        ammoGraphics.material.SetColor("_EmissionColor", ammoColorGradient.Evaluate((float)(weapon.magCapacity - currentAmmo) / weapon.magCapacity));
    }

}
