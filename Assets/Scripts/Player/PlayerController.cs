using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int playerNum;
    public PlayerInteraction interaction;
    public Camera playerCam;
    public Rigidbody rb;
    public CapsuleCollider col;
    public Canvas canvas;

    //These are only here until weapon func is added
    public Transform pivot, firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;   
    public GameObject standGraphics, crouchGraphics;
    ////
    //Pivot Height
    public float currentHeight, crouchHeight, normalHeight;
    //Speed
    public float currentSpeed, walkSpeed, crouchSpeed, lerpedAimSpeed;
    //Jumping
    public float jumpHeight, gravityScale, checkOffset = 0.1f;
    public bool isGrounded;
    public LayerMask groundLayers;

    PlayerState_Base currentState;
    public PlayerState_Neutral state_Neutral = new PlayerState_Neutral();

    public Character_Base currentCharacter;
    public Character_TEST character_TEST = new Character_TEST();
    public Character_Captain character_Captain = new Character_Captain();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        interaction = GetComponent<PlayerInteraction>();
    }

    
    #region StateManagement
    private void Start()
    {
        currentState = state_Neutral;
        currentState.EnterState(this);

        currentCharacter = character_Captain;
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

    public void OnAction(InputAction.CallbackContext ctx)
    {
        currentState.OnAction(this, ctx);
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        currentState.OnInteract(this, ctx);
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

    public Coroutine HelpStartCoroutine(IEnumerator coroutineMethod)
    {
        return StartCoroutine(coroutineMethod);
    }

    public void HelpStopCoroutine(Coroutine coroutineMethod)
    {
        StopCoroutine(coroutineMethod);
    }

    public GameObject HelpInstantiate(GameObject objectToSpawn, Vector3 pos, Quaternion rot)
    {
        GameObject newObject =  Instantiate(objectToSpawn, pos, rot);
        return newObject;
    }

    #endregion



}
