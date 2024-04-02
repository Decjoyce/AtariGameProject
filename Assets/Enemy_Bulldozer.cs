using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerHealth;

public class Enemy_Bulldozer : MonoBehaviour
{
    public int currentRoomID;

    [HideInInspector] public Rigidbody rb;

    public LayerMask ignoreLayers;
    public Transform firePoint;
    [SerializeField] ProximityTrigger proxTrig;
    [SerializeField] ProximityTrigger attackTrig;

    public Transform currentTarget;
    public List<Transform> targets = new List<Transform>();

    public float charge_delay;
    public float speed;
    public float turnSpeed;
    public float chargeForce;
    public float chargeDamage;
    [HideInInspector] public Vector3 chargeDirection;

    [SerializeField] BulldozerStates visibleState;
    EnemyStates_Bulldozer currentState;
    public BulldozerState_Idle state_Idle = new BulldozerState_Idle();
    public BulldozerState_Aggro state_Aggro = new BulldozerState_Aggro();
    public BulldozerState_Charge state_Charge = new BulldozerState_Charge();

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
        attackTrig.OnEnter += OnAttackTriggerEnter;
        attackTrig.OnExit += OnAttackTriggerExit;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= OnPlayerDied;
        RoomManager.OnEnter -= OnPlayerEnterRoom;
        RoomManager.OnExit -= OnPlayerExitRoom;
        proxTrig.OnEnter -= OnProximityTriggerEnter;
        proxTrig.OnExit -= OnProximityTriggerExit;
        attackTrig.OnEnter -= OnAttackTriggerEnter;
        attackTrig.OnExit -= OnAttackTriggerExit;
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

    public void OnPlayerEnterRoom(GameObject player, int roomID)
    {
        if (currentRoomID == roomID)
        {
            currentState.OnRoomEnter(this, player);
            AddTarget(player);
        }

    }

    public void OnPlayerExitRoom(GameObject player, int roomID)
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
        currentState.OnTriggerExit(this, other);
    }

    public void OnAttackTriggerEnter(Collider other)
    {
        currentState.OnAttackTriggerEnter(this, other);
    }

    public void OnAttackTriggerExit(Collider other)
    {
    }

    public void SwitchState(string state)
    {
        currentState.ExitState(this);

        switch (state)
        {
            case "IDLE":
                currentState = state_Idle;
                visibleState = BulldozerStates.idle;
                break;
            case "AGGRO":
                currentState = state_Aggro;
                visibleState = BulldozerStates.aggro;
                break;
            case "CHARGE":
                currentState = state_Charge;
                visibleState = BulldozerStates.charge;
                break;
            default:
                Debug.LogError("WARNING: STATE NOT VALID");
                break;
        }
        currentState.EnterState(this);
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

    public void SetAttackTrigger(bool active)
    {
        attackTrig.isActive = active;
    }

}
