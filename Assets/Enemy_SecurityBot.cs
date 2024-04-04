using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem.XR;
using UnityEngine.TextCore;
using UnityEngine.TextCore.Text;

public class Enemy_SecurityBot : MonoBehaviour
{
    [Header("Room ID")]
    public int currentRoomID;

    [HideInInspector] public Rigidbody rb;

    [Header("References")]
    [SerializeField] ProximityTrigger proxTrig;
    [SerializeField] Transform head;
    [SerializeField] Transform firepoint;

    public GameObject machineGunProjectile;

    [Header("Graphics")]
    [SerializeField] GameObject[] gunGraphics; // 0 = MachineGun, 1 = Railgun, 2 = BeamGun
    public LineRenderer beamGraphic;
    [SerializeField] Gradient railBeamColor;
    [SerializeField] Gradient laserBeamColor;

    [Header("Stats")]
    public float moveSpeed = 3f;
    public float fleeDistanceMG = 5f;
    public float fleeDistanceRail = 7.5f;
    [SerializeField] Vector3[] headPositions;

    public float machineGunAttackSpeed;

    public float railgunAttackSpeed;
    public float railgunAttackDamage;
    public float railGunAttackWidth;
    public float railGunAttackRange;

    public float beamGunAttackSpeed;
    public float beamGunAttackDamage;
    public float beamGunAttackWidth;

    public LayerMask ignoreLayers;

    [Header("Targeting")]
    public Transform currentTarget;
    public List<Transform> targets = new List<Transform>();

    //RandomStateHandling
    private Coroutine coroutine_RandomStateDelay;
    private float switchDelay;

    [SerializeField] SecurityBotStates visibleState;
    EnemyStates_SecurityBot currentState;
    public SecurityBot_Idle state_Idle = new SecurityBot_Idle();
    public SecurityBot_MachineGun state_MachineGun = new SecurityBot_MachineGun();
    public SecurityBot_Railgun state_Railgun = new SecurityBot_Railgun();
    public SecurityBot_BeamGun state_BeamGun = new SecurityBot_BeamGun();

    #region StateMachine
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentState = state_Idle;
        currentState.EnterState(this);
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += OnPlayerDied;

        RoomManager.OnEnter += OnPlayerEnterRoom;
        RoomManager.OnExit += OnPlayerExitRoom;

        proxTrig.OnEnter += OnProximityTriggerEnter;
        proxTrig.OnExit += OnProximityTriggerExit;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= OnPlayerDied;

        RoomManager.OnEnter -= OnPlayerEnterRoom;
        RoomManager.OnExit -= OnPlayerExitRoom;

        proxTrig.OnEnter -= OnProximityTriggerEnter;
        proxTrig.OnExit -= OnProximityTriggerExit;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.FrameUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    public void OnPlayerDied(GameObject player, int playerID)
    {
        if (targets.Contains(player.transform))
            targets.RemoveAt(targets.IndexOf(player.transform));

        NextTarget();
    }

    void OnPlayerEnterRoom(GameObject player, int roomID)
    {
        if (currentRoomID == roomID)
        {
            currentState.OnRoomEnter(this, player);
            AddTarget(player);
        }
    }

    void OnPlayerExitRoom(GameObject player, int roomID)
    {
        if (currentRoomID == roomID)
        {
            currentState.OnRoomExit(this, player);
        }
    }

    public void OnProximityTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    public void OnProximityTriggerExit(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    public void SwitchState(string state)
    {
        currentState.ExitState(this);

        switch (state)
        {
            case "IDLE":
                currentState = state_Idle;
                visibleState = SecurityBotStates.idle;
                break;
            case "MACHINEGUN":
                currentState = state_MachineGun;
                visibleState = SecurityBotStates.machineGun;
                break;
            case "RAILGUN":
                currentState = state_Railgun;
                visibleState = SecurityBotStates.railgun;
                break;
            case "BEAMGUN":
                currentState = state_BeamGun;
                visibleState = SecurityBotStates.beamGun;
                break;
            default:
                Debug.LogError("WARNING: STATE NOT VALID");
                break;
        }
        currentState.EnterState(this);
    }

    public void StartRandomStateDelay()
    {
        coroutine_RandomStateDelay = StartCoroutine(RandomStateDelay());
    }

    IEnumerator RandomStateDelay()
    {
        switchDelay = Random.Range(5f, 20f);
        Debug.Log(switchDelay);
        yield return new WaitForSeconds(switchDelay);
        GetRandomState();
        GetRandomHeight();
    }

    public void GetRandomState()
    {
        int ranNum = Random.Range(0, 3);
        switch (ranNum)
        {
            case 0:
                SwitchState("MACHINEGUN");
                break;
            case 1:
                SwitchState("RAILGUN");
                SetBeamGraphics(true);
                break;
            case 2:
                SwitchState("BEAMGUN");
                SetBeamGraphics(false);
                break;
        }
        SetGunGraphics(ranNum);
    }

    public void GetRandomHeight()
    {
        int ranNum = Random.Range(0, 3);
        head.localPosition = headPositions[ranNum];
    }

    #endregion

    public void ShootMachineGun()
    {
        Instantiate(machineGunProjectile, firepoint.position, firepoint.rotation);
    }

    public void ShootRailgun()
    {
        Collider[] thingsHit = Physics.OverlapBox(firepoint.position + (firepoint.right * (railGunAttackRange/2)), new(railGunAttackRange/2, railGunAttackWidth, railGunAttackWidth), Quaternion.Euler(Vector3.zero), ignoreLayers);
        foreach(Collider thing in thingsHit)
        {
            if (thing.CompareTag("Player"))
            {
                thing.GetComponent<PlayerHealth>().TakeDamage(railgunAttackDamage);
            }
        }
        beamGraphic.enabled = true;
        beamGraphic.SetPosition(0, firepoint.position);
        beamGraphic.SetPosition(1, firepoint.position + (firepoint.right * railGunAttackRange));
    }

    public void ShootBeamGun()
    {
        RaycastHit hit;
        if (Physics.Raycast(firepoint.position, firepoint.right, out hit, Mathf.Infinity, ignoreLayers))
        {
            if (hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<PlayerHealth>().TakeDamage(beamGunAttackDamage);
            }
            beamGraphic.SetPosition(0, firepoint.position);
            beamGraphic.SetPosition(1, hit.point);
        }
    }

    void SetGunGraphics(int state)
    {
        for(int i = 0; i < 3; i++)
        {
            gunGraphics[i].SetActive(i == state);
        }
    }

    public void SetBeamGraphics(bool railgun)
    {
        if (!railgun)
        {
            beamGraphic.SetPosition(0, Vector3.zero);
            beamGraphic.SetPosition(1, Vector3.zero);
            beamGraphic.startWidth = beamGunAttackWidth;
            beamGraphic.endWidth = beamGunAttackWidth;
            beamGraphic.colorGradient = laserBeamColor;
        }
        else
        {
            beamGraphic.SetPosition(0, Vector3.zero);
            beamGraphic.SetPosition(1, Vector3.zero);
            beamGraphic.startWidth = railGunAttackWidth;
            beamGraphic.endWidth = 0.1f;
            beamGraphic.colorGradient = railBeamColor;
        }
    }

    public void FaceSomething(Vector3 theThing)
    {
        if (transform.position.x - theThing.x > 0)
        {
            transform.eulerAngles = Vector3.up * 180f;
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void AddTarget(GameObject target)
    {
        if (!targets.Contains(target.transform))
        {
            targets.Add(target.transform);
            if (currentTarget == null)
                currentTarget = target.transform;
        }
    }

    public void NextTarget()
    {
        if (targets.Count > 0)
        {
            float closestInt = 50000f;
            Transform temp_ClostestInteractable = null;
            foreach (Transform target in targets)
            {
                float dist = Vector3.SqrMagnitude(transform.position - target.transform.position);

                if (dist < closestInt)
                {
                    closestInt = dist;
                    temp_ClostestInteractable = target;
                }
            }
            currentTarget = temp_ClostestInteractable;
            Debug.Log("Got Next Target: " + currentTarget.name);
        }
        else
        {
            SwitchState("IDLE");
            Debug.Log("BackToIdle");
        }
    }
}
