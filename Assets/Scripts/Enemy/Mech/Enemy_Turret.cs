using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret : MonoBehaviour
{
    public Transform firePoint;
    public Transform y_pivot, x_pivot;
    public LayerMask ignoreLayers;
    public GameObject projectile;

    public Transform target;
    public List<Transform> targets = new List<Transform>();

    public float shoot_delay;

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

    // Update is called once per frame
    void Update()
    {
        currentState.FrameUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
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

    public void NextTarget()
    {
        if(targets.Count > 0)
        {
            targets.RemoveAt(0);
            target = targets[0];
        }
        else
        {
            SwitchState("IDLE");
        }
    }

    public void HelpInstantiate(Vector3 pos, Quaternion angle)
    {
        Instantiate(projectile, pos, angle);
    }
}
