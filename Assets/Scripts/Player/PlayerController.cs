using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[System.Serializable]
public struct PlayerStats
{
    public int moveMod;
    public int jumpPowerMod;
    public int weaponSpreadMod;
    public int hackSpeedMod;

    public void GenerateProciencies()
    {
        moveMod = Random.Range(-5, 5);
        jumpPowerMod = Random.Range(-5, 5);
        weaponSpreadMod = Random.Range(-5, 5);
        hackSpeedMod = Random.Range(-5, 5);

        if (moveMod == 0)
        {
            moveMod = 1;
        }
        if (jumpPowerMod == 0)
        {
            jumpPowerMod = 1;
        }
        if (weaponSpreadMod == 0)
        {
            weaponSpreadMod = 1;
        }
        if (hackSpeedMod == 0)
        {
            hackSpeedMod = 1;
        }
    }
}

public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public int playerNum;
    public bool isOut;
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

    [Header("Character")]
    bool hasHadCharacter;
    public int charNum;
    public Character character;
    public PlayerStats playerStats;
    [SerializeField] GameObject skin_captain;
    [SerializeField] GameObject skin_engineer;
    [SerializeField] GameObject skin_doctor;
    [SerializeField] GameObject skin_navigator;
    [SerializeField] GameObject skin_crewmate;

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
    public RigBuilder[] rigs;

    [Header("DEBUG")]
    public bool debuggingMode;
    public bool oldLook;

    [Header("Extracted")]
    public bool extractedCheck;

    [HideInInspector] public bool faceLeft = true;

    //public PlayerStates playerState;
    PlayerState_Base currentState;
    public PlayerState_Joined state_Joined = new PlayerState_Joined();
    public PlayerState_CharacterSelect state_CharacterSelect = new PlayerState_CharacterSelect();
    public PlayerState_Neutral state_Neutral = new PlayerState_Neutral();
    public PlayerState_Death state_Death = new PlayerState_Death();
    public PlayerState_Extracted state_Extracted = new PlayerState_Extracted();

    public Character_Base currentCharacter;
    public Character_TEST character_TEST = new Character_TEST();
    public Character_Captain character_Captain = new Character_Captain();
    public Character_Engineer character_Engineer = new Character_Engineer();
    public Character_Doctor character_Doctor = new Character_Doctor();
    public Character_Navigator character_Navigator = new Character_Navigator();
    public Character_Crewmate character_Crewmate = new Character_Crewmate();

    private void Awake()
    {
        interaction = GetComponent<PlayerInteraction>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<PlayerHealth>();

        
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        playerStats.GenerateProciencies();
    }


    #region StateManagement
    private void Start()
    {
        checkObstacleSize = new(0.5f, 2f, layerOffset);

        currentState = state_Joined;
        currentState.EnterState(this);

        currentCharacter = character_Captain;
    }

    private void Update()
    {
        currentState.FrameUpdate(this);
        if (Input.GetKeyDown(KeyCode.Z))
            attack.SaveCharacter();
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
            case "CHARACTERSELECT":
                currentState = state_CharacterSelect;
                //playerState = PlayerStates.Selecting;
                break;
            case "NEUTRAL":
                currentState = state_Neutral;
                Debug.Log("Yo");
                //playerState = PlayerStates.Neutral;
                break;
            case "DEATH":
                currentState = state_Death;
                //playerState = PlayerStates.Death;
                break;
            case "EXTRACTED":
                currentState = state_Extracted;
                //playerState = PlayerStates.Extracted;
                break;
            default:
                Debug.LogError("INVALID STATE: " + newState);
                break;
        }

        currentState.EnterState(this);
    }

    public void SwitchClass(string newCharacter)
    {
        switch (newCharacter)
        {
            case "CAPTAIN":
                currentCharacter = character_Captain;
                interaction.isEngineer = false;
                break;
            case "ENGINEER":
                currentCharacter = character_Engineer;
                interaction.isEngineer = true;
                break;
            case "DOCTOR":
                currentCharacter = character_Doctor;
                interaction.isEngineer = false;
                break;
            case "NAVIGATOR":
                currentCharacter = character_Navigator;
                interaction.isEngineer = false;
                break;
            case "CREWMATE":
                currentCharacter = character_Crewmate;
                interaction.isEngineer = false;
                break;
            default:
                Debug.LogError("INVALID STATE: " + newCharacter);
                break;
        }
        SetClassSkin(newCharacter);
    }

    public void SetUpCharacter(Character newChar, int newCharNum)
    {
        charNum = newCharNum;
        character = newChar;
        SwitchClass(character.characterClass);
        health.currentHealth = character.health;
        if (character.weapon != null)
            attack.PickUpWeapon(character.weapon, character.currentAmmo, character.currentReserve, false);
        else
            attack.UseDefaultWeapon();
        Debug.Log("Cheadle");
    }

    public void SaveCharacter()
    {
        character.health = health.currentHealth;
        attack.SaveCharacter();
            GameManager.instance.SaveCharacter(character, playerNum, charNum);
    }

    void SetClassSkin(string nameofClass)
    {
        switch (nameofClass)
        {
            case "CAPTAIN":
                skin_captain.SetActive(true);
                skin_engineer.SetActive(false);
                skin_doctor.SetActive(false);
                skin_navigator.SetActive(false);
                skin_crewmate.SetActive(false);

                anim = skin_captain.GetComponent<Animator>();
                break;
            case "ENGINEER":
                skin_captain.SetActive(false);
                skin_engineer.SetActive(true);
                skin_doctor.SetActive(false);
                skin_navigator.SetActive(false);
                skin_crewmate.SetActive(false);

                anim = skin_engineer.GetComponent<Animator>();
                break;
            case "DOCTOR":
                skin_captain.SetActive(false);
                skin_engineer.SetActive(false);
                skin_doctor.SetActive(true);
                skin_navigator.SetActive(false);
                skin_crewmate.SetActive(false);

                anim = skin_doctor.GetComponent<Animator>();
                break;
            case "NAVIGATOR":
                skin_captain.SetActive(false);
                skin_engineer.SetActive(false);
                skin_doctor.SetActive(false);
                skin_navigator.SetActive(true);
                skin_crewmate.SetActive(false);

                anim = skin_navigator.GetComponent<Animator>();
                break;
            case "CREWMATE":
                skin_captain.SetActive(false);
                skin_engineer.SetActive(false);
                skin_doctor.SetActive(false);
                skin_navigator.SetActive(false);
                skin_crewmate.SetActive(true);

                anim = skin_crewmate.GetComponent<Animator>();
                break;
            default:
                Debug.LogError("INVALID STATE: " + nameofClass);
                break;
        }
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

    public void TurnOnIK(int layerNum, bool turnOn)
    {
        foreach(RigBuilder rig in rigs)
        {
            rig.layers[layerNum].active = turnOn;
        }

    }

    public void FaceDirection(bool faceLeft)
    {
        if (faceLeft)
        {
            attack.gunPos.localEulerAngles = new(0f, 180f, attack.gunPos.localEulerAngles.z);
            graphicsPivot.localEulerAngles = new(graphicsPivot.localEulerAngles.x, 180f, 0f);
            animFlipper = -1;
            Debug.Log("WTF");
        }
        else
        {
            attack.gunPos.localEulerAngles = new(0f, 0f, attack.gunPos.localEulerAngles.z);
            graphicsPivot.localEulerAngles = new(graphicsPivot.localEulerAngles.x, 0f, 0f);
            animFlipper = 1;
            Debug.Log("CUCK");
        }
    }

    public void IsExtracted()
    {
        extractedCheck = true;
    }

    public void NotExtracted()
    {
        extractedCheck = false;
    }
}
