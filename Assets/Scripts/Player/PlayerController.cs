using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public int playerNum;
    [System.NonSerialized] public PlayerInteraction interaction;
    [System.NonSerialized] public PlayerAttack attack;
    [System.NonSerialized] public PlayerHealth health;

    public GameObject cub;

    [Header("References")]
    public Camera playerCam;
    public Rigidbody rb;
    public CapsuleCollider col;
    public Canvas canvas;

    public Transform pivot;
    public Transform graphicsPivot;

    [Header("Layer")]
    public float currentLayer;
    public float layerOffset;
    public LayerMask layerLayers;
    [HideInInspector] public Vector3 checkObstacleSize;

    [Header("Speed")]
    public float currentSpeed;
    public float walkSpeed;
    public float crouchSpeed;
    public float lerpedAimSpeed;
    public float switchLaneSpeed;

    [Header("Jumping")]
    public float jumpHeight;
    public float gravityScale;
    public float checkOffset = 0.1f;
    public bool isGrounded;
    public LayerMask groundLayers;

    [Header("Crouching")]
    public GameObject standGraphics;
    public GameObject crouchGraphics;
    public float currentHeight;
    public float crouchHeight;
    public float crouchHeightHandPos;
    public float normalHeight;

    [Header("Animation")]
    public Animator anim;
    [HideInInspector] public int animFlipper = 1;

    [Header("DEBUG")]
    public bool debuggingMode;
    public bool oldLook;

    [HideInInspector] public bool faceLeft = true;

    PlayerState_Base currentState;
    public PlayerState_Neutral state_Neutral = new PlayerState_Neutral();
    public PlayerState_Death state_Death = new PlayerState_Death();

    public Character_Base currentCharacter;
    public Character_TEST character_TEST = new Character_TEST();
    public Character_Captain character_Captain = new Character_Captain();

    private void Awake()
    {
        interaction = GetComponent<PlayerInteraction>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<PlayerHealth>();

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }


    #region StateManagement
    private void Start()
    {
        checkObstacleSize = new(0.5f, 2f, layerOffset);

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

    public void OnLayerDown(InputAction.CallbackContext ctx)
    {
        currentState.OnLayerDown(this, ctx);
    }

    public void OnLayerUp(InputAction.CallbackContext ctx)
    {
        currentState.OnLayerUp(this, ctx);
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

    public void OnReload(InputAction.CallbackContext ctx)
    {
        currentState.OnReload(this, ctx);
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
            case "DEATH":
                currentState = state_Death;
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
        GameObject newObject = Instantiate(objectToSpawn, pos, rot);
        return newObject;
    }

    #endregion


    public void FaceDirection(bool faceLeft)
    {
        if (faceLeft)
        {
            attack.gunPos.localEulerAngles = new(0f, 180f, attack.gunPos.localEulerAngles.z);
            graphicsPivot.localEulerAngles = new(graphicsPivot.localEulerAngles.x, 180f, 0f);
            animFlipper = -1;
        }
        else
        {
            attack.gunPos.localEulerAngles = new(0f, 0f, attack.gunPos.localEulerAngles.z);
            graphicsPivot.localEulerAngles = new(graphicsPivot.localEulerAngles.x, 0f, 0f);
            animFlipper = 1;
        }
    }
}
