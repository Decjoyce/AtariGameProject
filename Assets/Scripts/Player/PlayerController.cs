using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int playerNum;
    public Camera playerCam;
    public Rigidbody rb;

    //These are only here until weapon func is added
    public Transform pivot, firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    ////

    public float currentHeight, crouchHeight, normalHeight, jumpHeight;
    public float currentSpeed, walkSpeed, crouchSpeed, lerpedAimSpeed;
    public GameObject crouchGraphics, standGraphics;

    PlayerState_Base currentState;
    public PlayerState_Neutral state_Neutral = new PlayerState_Neutral();

    private void Awake()
    {
        playerCam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    
    #region StateManagement
    private void Start()
    {
        currentState = state_Neutral;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.FrameUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        currentState.OnMove(this, ctx);
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        currentState.OnLook(this, ctx);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        currentState.OnJump(this, ctx);
    }

    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        currentState.OnCrouch(this, ctx);
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        currentState.OnShoot(this, ctx);
    }

    public void SwitchState(string newState, bool performExit = true)
    {
        if (performExit)
            currentState.ExitState(this);

        switch (newState)
        {
            case "NEUTRAL":
                currentState = state_Neutral;
                break;
            default:
                Debug.LogError("INVALID STATE: " + newState);
                break;
        }

        currentState.EnterState(this);
    }

    public GameObject HelpInstantiate(GameObject objectToSpawn, Vector3 pos, Quaternion rot)
    {
        GameObject newObject =  Instantiate(objectToSpawn, pos, rot);
        return newObject;
    }

    #endregion



}
