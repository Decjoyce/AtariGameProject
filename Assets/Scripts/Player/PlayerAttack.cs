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
    [SerializeField] Transform rightHandPos, leftHandPos;
    [SerializeField] Transform[] rightHandHints, leftHandHints;
    [SerializeField] Transform pivot, handPivot;

    GameObject weaponMesh;
    MeshRenderer ammoGraphics; //Temp

    public WeaponType weapon;
    [SerializeField] WeaponType defaultWeapon, fists;
    bool cannotDropWeapon;
    int currentAmmo, currentReserve;
    bool canAttack = true, isReloading;
    bool isAutoFiring;
    float attackDelay;
    bool isSwinging, returningFromSwing;
    bool isThrusting, returningFromThrust;
    Quaternion targetRot;
    Vector3 targetPos, ogHandPos;

    bool doc_isUsing;
    float doc_currentammo;
    [Header("Doctor")]
    [SerializeField] GameObject doc_gun;
    [SerializeField] int doc_maxammo;
    [SerializeField] float doc_range, doc_healamount, doc_firerate;
    [SerializeField] Transform doc_firepoint;
    [SerializeField] LayerMask doc_ignorelayers;
    [SerializeField] LineRenderer doc_beamgraphics;
    [SerializeField] MeshRenderer doc_ammographics;

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

        doc_currentammo = doc_maxammo;
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
            if (!doc_isUsing)
                AutoShot();
            else
                DocShot();
        }

        SwingingAttack();
        ThrustingAttack();
    }
    
    public void SaveCharacter()
    {
        controller.character.weapon = weapon;
        controller.character.currentAmmo = currentAmmo;
        controller.character.currentReserve = currentReserve;
    }

    public void SetUpClassWeapon()
    {
        accurracy = accurracy * controller.playerStats.weaponSpreadMod + (0.5f * controller.playerStats.moveMod);
    }

    public void UseDefaultWeapon()
    {
        weapon = defaultWeapon;

        currentAmmo = weapon.magCapacity;
        currentReserve = weapon.reserveCapacity;

        SetWeaponMesh();
        SetGunColor();
    }

    public void PickUpWeapon(WeaponType newWeapon, int newAmmo, int newReserve, bool dropWeapon = true)
    {
        if(dropWeapon)
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
        isSwinging = false;
        isReloading = false;
        canAttack = true;
        attackDelay = 0;

        if (weapon != fists)
        {
            GameObject droppedWeapon = Instantiate(droppedWeaponPrefab, firePoint.position, Quaternion.identity);
            droppedWeapon.GetComponent<WeaponPickup>().ChangeStats(weapon, currentAmmo, currentReserve);
            GameObject droppedWeaponModel = Instantiate(weaponMesh, droppedWeapon.transform);
            droppedWeaponModel.SetActive(true);
            Transform holder = droppedWeaponModel.transform.GetChild(0);
            holder.GetComponent<Collider>().enabled = true;
            holder.GetChild(1).GetComponent<MeshRenderer>().material = ammoGraphics.material;
        }

        if (returnToFists)
        {
            weapon = fists;
            currentAmmo = 420;
            currentReserve = 69;

            SetFistMesh();

        }

    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (!doc_isUsing)
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
        else
        {
            if (ctx.started)
            {
                isAutoFiring = true;
                doc_beamgraphics.gameObject.SetActive(true);
            }
            else if (ctx.canceled)
            {
                isAutoFiring = false;
                doc_beamgraphics.gameObject.SetActive(false);
            }

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
            Quaternion bulletRotation = Quaternion.Euler(firePoint.eulerAngles.x + spread, firePoint.eulerAngles.y, firePoint.eulerAngles.z);
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
        SetWeaponHold();

        if (doc_isUsing)
            weaponMesh.SetActive(false);
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

        SetWeaponHold(false);
    }

    void SetWeaponHold(bool isFists = false)
    {
        if (!doc_isUsing)
        {
            Transform holder = weaponMesh.transform.GetChild(0);
            if (!isFists)
            {
                rightHandPos.SetPositionAndRotation(holder.GetChild(2).position, holder.GetChild(2).rotation);
                AdjustRightHandHints(Vector3.zero);
                leftHandPos.SetPositionAndRotation(holder.GetChild(3).position, holder.GetChild(3).rotation);
                AdjustLeftHandHints(Vector3.zero);
            }
            else
            {
                rightHandPos.SetPositionAndRotation(holder.GetChild(2).position, holder.GetChild(2).rotation);
                AdjustRightHandHints(new Vector3(0.65f, -1.32f, -0.47f));
                leftHandPos.SetPositionAndRotation(holder.GetChild(3).position, holder.GetChild(3).rotation);
                AdjustLeftHandHints(new Vector3(-0.65f, -1.32f, -0.47f));
            }
        }
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

    void AdjustRightHandHints(Vector3 pos)
    {
        foreach(Transform hint in rightHandHints)
        {
            hint.localPosition = pos;
        }
    }

    void AdjustLeftHandHints(Vector3 pos)
    {
        foreach (Transform hint in leftHandHints)
        {
            hint.localPosition = pos;
        }
    }

    #region DocShit
    public void UseDocGun()
    {
        doc_isUsing = !doc_isUsing;
        if (doc_isUsing)
        {
            cannotDropWeapon = true;
            doc_gun.SetActive(true);
            weaponMesh.SetActive(false);
            rightHandPos.SetPositionAndRotation(doc_gun.transform.GetChild(1).position, doc_gun.transform.GetChild(1).rotation);
            AdjustRightHandHints(Vector3.zero);
            leftHandPos.SetPositionAndRotation(doc_gun.transform.GetChild(2).position, doc_gun.transform.GetChild(2).rotation);
            AdjustLeftHandHints(Vector3.zero);
        }
        else
        {
            cannotDropWeapon = false;
            doc_gun.SetActive(false);
            weaponMesh.SetActive(true);
            SetWeaponHold();
        }

    }

    void DocShot()
    {
        if (doc_currentammo > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(doc_firepoint.position, doc_firepoint.up, out hit, doc_range, doc_ignorelayers))
            {
                if (canAttack && hit.transform.CompareTag("Player"))
                {
                    hit.transform.GetComponent<PlayerHealth>().Heal(doc_healamount);
                }
                doc_beamgraphics.SetPosition(0, doc_firepoint.position);
                doc_beamgraphics.SetPosition(1, hit.point);
            }
            else
            {
                doc_beamgraphics.SetPosition(0, doc_firepoint.position);
                doc_beamgraphics.SetPosition(1, doc_firepoint.position + (doc_firepoint.up * doc_range));
            }
            if (canAttack)
            {
                canAttack = false;
                attackDelay = doc_firerate;

                //source.PlayOneShot(weapon.fireSound);

                doc_currentammo--;

                DocSetGunColor();
            }

        }
    }

    void DocSetGunColor()
    {
        doc_ammographics.material.SetColor("_EmissionColor", ammoColorGradient.Evaluate((float)(doc_maxammo - doc_currentammo) / doc_maxammo));
    }
    #endregion

}
