using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret : MonoBehaviour
{
    public int currentRoomID;

    public Transform firePoint;
    public Transform y_pivot, x_pivot;
    public LayerMask ignoreLayers;
    public GameObject projectile;
    [SerializeField] ProximityTrigger proxTrig;

    public Transform currentTarget;
    public List<Transform> targets = new List<Transform>();

    public float shoot_delay;

    public float turnSpeed;

    [SerializeField] TurretStates visibleState;
    EnemyStates_Turret currentState;
    public TurretState_Idle state_Idle = new TurretState_Idle();
    public TurretState_Aggro state_Aggro = new TurretState_Aggro();

    // Start is called before the first frame update
    void Start()
    {
        currentState = state_Idle;
        currentState.EnterState(this);
    }

    private void OnEnable()
    {
        RoomManager.OnEnter += OnPlayerEnterRoom;
        RoomManager.OnExit += OnPlayerEnterRoom;
        proxTrig.OnEnter += OnProximityTriggerEnter;
        proxTrig.OnExit += OnProximityTriggerExit;
    }

    private void OnDisable()
    {
        RoomManager.OnEnter -= OnPlayerEnterRoom;
        RoomManager.OnExit -= OnPlayerEnterRoom;
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

    public void OnTargetsChanged()
    {

    }

    public void SwitchState(string state)
    {
        currentState.ExitState(this);

        switch (state)
        {
            case "IDLE":
                currentState = state_Idle;
                visibleState = TurretStates.idle;
            break;
            case "AGGRO":
                currentState = state_Aggro;
                visibleState = TurretStates.aggro;
                break;
            default:
                Debug.LogError("WARNING: STATE NOT VALID");
                break;
        }
        currentState.ExitState(this);
    }

    public void AddTarget(GameObject target)
    {
        if (!targets.Contains(target.transform))
        {
            targets.Add(target.transform);

            if (currentTarget == null)
                currentTarget = target.transform;

            //OnTargetAdded();
        }
    }

    public void NextTarget()
    {
        if(targets.Count > 0)
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

    public void HelpInstantiate(Vector3 pos, Quaternion angle)
    {
        Instantiate(projectile, pos, angle);
    }
}
