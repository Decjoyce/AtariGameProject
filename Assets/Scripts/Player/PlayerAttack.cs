using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    PlayerController controller;
    AudioSource source;

    [SerializeField] Transform firePoint;
    public Transform handPos;
    public Transform gunPos;
    [SerializeField] Transform rightHandPos, rightHandHint, leftHandPos, leftHandHint;
    [SerializeField] Transform pivot, handPivot;

    GameObject weaponMesh;
    MeshRenderer ammoGraphics; //Temp

    WeaponType weapon;
    [SerializeField] WeaponType defaultWeapon, fists;
    int currentAmmo, currentReserve;
    bool canAttack = true, isReloading;
    bool isAutoFiring;
    float attackDelay;
    bool isSwinging, returningFromSwing;
    bool isThrusting, returningFromThrust;
    Quaternion targetRot;
    Vector3 targetPos, ogHandPos;

    [SerializeField] Gradient ammoColorGradient;

    [SerializeField] GameObject droppedWeaponPrefab;

    [SerializeField] float accurracy; //Temp

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
        if (!canAttack && attackDelay <= 0)
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

        SwingingAttack();
        ThrustingAttack();
    }

    public void PickUpWeapon(WeaponType newWeapon, int newAmmo, int newReserve)
    {
        DropWeapon();

        weapon = newWeapon;
        currentAmmo = newAmmo;
        currentReserve = newReserve;

        SetWeaponMesh();
        //SetWeaponHold();

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
        isSwinging = false;
        isReloading = false;
        canAttack = true;
        attackDelay = 0;

        if (weapon != fists)
        {
            GameObject droppedWeapon = Instantiate(droppedWeaponPrefab, firePoint.position, Quaternion.identity);
            droppedWeapon.GetComponent<WeaponPickup>().ChangeStats(weapon, currentAmmo, currentReserve);
            GameObject droppedWeaponModel = Instantiate(weaponMesh, droppedWeapon.transform);
            Transform holder = droppedWeaponModel.transform.GetChild(0);
            holder.GetComponent<Collider>().enabled = true;
            holder.GetChild(1).GetComponent<MeshRenderer>().material = ammoGraphics.material;
        }

        if (returnToFists)
        {
            weapon = fists;
            currentAmmo = 420;
            currentReserve = 69;

            //DisableWeaponMesh();
            SetFistMesh();
            //SetWeaponMesh(); // temp
            //SetWeaponHold();

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
        if (weapon.reloadSpeed > 0 && currentAmmo < weapon.magCapacity)
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

        if (currentReserve > weapon.magCapacity)
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
            float spread = Random.Range(-accurracy, accurracy);
            Quaternion bulletRotation = Quaternion.Euler(firePoint.eulerAngles.x, firePoint.eulerAngles.y, firePoint.eulerAngles.z + spread);
            Instantiate(weapon.projectile, firePoint.position, bulletRotation);

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
            float spread = Random.Range(-accurracy, accurracy);
            Quaternion bulletRotation = Quaternion.Euler(firePoint.eulerAngles.x, firePoint.eulerAngles.y, firePoint.eulerAngles.z + spread);
            Instantiate(weapon.projectile, firePoint.position, bulletRotation);

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
        if (ctx.performed && canAttack && !isSwinging)
        {
            isSwinging = true;
            targetRot = Quaternion.AngleAxis(weapon.meleeArc * controller.animFlipper, handPivot.forward);

            weaponMesh.GetComponent<MeleeDamage>().col.enabled = true;

            source.PlayOneShot(weapon.fireSound);

            canAttack = false;
            attackDelay = weapon.fireRate;
        }
    }

    private void ThrustMelee(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canAttack)
        {
            isThrusting = true;
            targetPos = rightHandPos.localPosition + (Vector3.up * (rightHandPos.localPosition.y + weapon.meleeRange));
            ogHandPos = rightHandPos.localPosition;

            rightHandPos.GetComponent<MeleeDamage>().col.enabled = true;

            source.PlayOneShot(weapon.fireSound);

            canAttack = false;
            attackDelay = weapon.fireRate;
        }
    }

    void SwingingAttack()
    {
        if (isSwinging)
        {
            handPivot.localRotation = Quaternion.Lerp(handPivot.localRotation, targetRot, weapon.meleeSpeed * Time.deltaTime);

            //Debug.Log(handPivot.localEulerAngles.z);

            if (handPivot.localRotation == targetRot)
            {
                isSwinging = false;
                returningFromSwing = true;
                targetRot = Quaternion.AngleAxis(0f, handPivot.forward);
                weaponMesh.GetComponent<MeleeDamage>().col.enabled = false;
            }
        }
        if (returningFromSwing)
        {
            handPivot.localRotation = Quaternion.Lerp(handPivot.localRotation, targetRot, (handPivot.localEulerAngles.z / attackDelay) * Time.deltaTime);
            if (handPivot.localRotation == targetRot)
            {
                returningFromSwing = false;
            }
        }
    }

    void ThrustingAttack()
    {
        if (isThrusting)
        {
            rightHandPos.localPosition = Vector3.Lerp(rightHandPos.localPosition, targetPos, weapon.meleeSpeed * Time.deltaTime);
            float dist = Vector3.Distance(rightHandPos.localPosition, targetPos);
            if (dist <= 0.1f)
            {
                isThrusting = false;
                returningFromThrust = true;
                targetPos = ogHandPos;
                rightHandPos.GetComponent<MeleeDamage>().col.enabled = false;
            }
        }
        if (returningFromThrust)
        {
            rightHandPos.localPosition = Vector3.Lerp(rightHandPos.localPosition, targetPos, weapon.meleeSpeed * Time.deltaTime);
            float dist = Vector3.Distance(rightHandPos.localPosition, targetPos);
            //Debug.Log(targetPos + " " + );
            if (dist <= 0.1f)
            {
                returningFromThrust = false;
                rightHandPos.localPosition = targetPos;
            }
        }
    }

    void SetWeaponHold()
    {
        switch (weapon.holdType)
        {
            case HoldType.gunOneHanded:
                controller.anim.SetBool("useOneHanded", true);
                controller.anim.SetBool("useTwoHanded", false);
                controller.anim.SetBool("useMelee", false);
                break;
            case HoldType.gunTwoHanded:
                controller.anim.SetBool("useTwoHanded", true);
                controller.anim.SetBool("useOneHanded", false);
                controller.anim.SetBool("useMelee", false);
                break;
            case HoldType.meleeSwing:
                controller.anim.SetBool("useMelee", true);
                controller.anim.SetBool("useOneHanded", false);
                controller.anim.SetBool("useTwoHanded", false);
                break;
            case HoldType.meleeThrust:
                controller.anim.SetBool("useMelee", true);
                controller.anim.SetBool("useOneHanded", false);
                controller.anim.SetBool("useTwoHanded", false);
                break;
            default:
                Debug.Log("tbh idek how u did this but fair play");
                break;
        }
    }

    void SetWeaponMesh()
    {
        if (weaponMesh != null)
        {
            ammoGraphics = null;
            Destroy(weaponMesh);
        }

        if(weapon.holdType != HoldType.meleeThrust)
            weaponMesh = Instantiate(weapon.weaponModel, handPos.position, handPos.rotation, handPos);
        else
            weaponMesh = Instantiate(weapon.weaponModel, handPos.position, handPos.rotation, rightHandPos);
        Transform holder = weaponMesh.transform.GetChild(0);
        holder.GetComponent<Collider>().enabled = false;
        ammoGraphics = holder.GetChild(1).GetComponent<MeshRenderer>();
        firePoint = holder.GetChild(0);
        rightHandPos.SetPositionAndRotation(holder.GetChild(2).position, weaponMesh.transform.GetChild(0).GetChild(2).rotation);
        rightHandHint.localPosition = Vector3.zero;
        leftHandPos.SetPositionAndRotation(holder.GetChild(3).position, weaponMesh.transform.GetChild(0).GetChild(3).rotation);
        leftHandHint.localPosition = Vector3.zero;
    }

    void SetFistMesh()
    {
        if (weaponMesh != null)
        {
            ammoGraphics = null;
            Destroy(weaponMesh);
        }

        weaponMesh = Instantiate(weapon.weaponModel, handPos.position, handPos.rotation, handPos);

        Transform holder = weaponMesh.transform.GetChild(0);
        firePoint = holder.GetChild(0);

        rightHandPos.SetPositionAndRotation(holder.GetChild(2).position, weaponMesh.transform.GetChild(0).GetChild(2).rotation);
        rightHandHint.localPosition = new Vector3(0.65f, -1.32f, -0.47f);
        leftHandPos.SetPositionAndRotation(holder.GetChild(3).position, weaponMesh.transform.GetChild(0).GetChild(3).rotation);
        leftHandHint.localPosition = new Vector3(-0.65f, -1.32f, -0.47f);
    }

    private void DisableWeaponMesh()
    {
        if (weaponMesh != null)
        {
            ammoGraphics = null;
            Destroy(weaponMesh);
            weaponMesh = null;
        }
    }

    public void AdjustFirePoint()
    {
        firePoint = weaponMesh.transform.GetChild(0).GetChild(0);
    }

    void SetGunColor()
    {
        ammoGraphics.material.SetColor("_EmissionColor", ammoColorGradient.Evaluate((float)(weapon.magCapacity - currentAmmo) / weapon.magCapacity));
    }

}
