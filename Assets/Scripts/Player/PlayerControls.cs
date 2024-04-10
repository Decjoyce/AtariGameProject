//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Player/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""GameplayControls"",
            ""id"": ""7088c8e2-f8a5-4f22-8e33-8295044dd51a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f3706b6c-bb96-4984-b3f1-d423ab2923c6"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""d3cd9142-553a-48e7-bec8-5ca7daccd157"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f4fcebe4-2598-495e-a810-486e2bdb954a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""5ac6532c-015c-4d14-a9f9-cce59b09cf01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""72849393-0cbf-4cc1-af92-d6560762f341"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""a92c99d5-3f41-473a-b1a3-d137b350b40f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""272f9a24-5fe7-4cca-a3c4-c4ba6d848590"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""7b7b11e6-6302-413c-95d9-3a86fc77cc7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LayerUp"",
                    ""type"": ""Button"",
                    ""id"": ""e29c2ef8-30d6-4bb9-a7dc-673297e1ea06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LayerDown"",
                    ""type"": ""Button"",
                    ""id"": ""9bf9aa72-7bac-482c-9408-ae2b99255cb4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0fde7cba-6231-46f0-87d5-124748e8397b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8b993cf-348b-4c65-b567-f62e5749b833"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb736aa7-3035-45c1-8247-40744e5dce6d"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f2c99f3-99ed-42e6-97ac-7a7980a55435"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""69fae581-32b9-4411-be35-b9790709e49c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6078e539-fb2e-4145-9f6d-691f0a460579"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""782da4a6-1fc2-456b-9840-d3d05f6a7775"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4223eeb8-9855-40db-bd16-19880af393e5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afaf5cae-c057-426a-8bcc-1a3e74cca63b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1377bfa-ba76-4156-a1fb-4bcbbdedb6c3"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LayerUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee09f9b0-601e-4563-af2e-7bbde7263b47"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LayerDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2237c311-8580-499b-8b15-2a4a2d84421a"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""BaseControlScheme"",
            ""bindingGroup"": ""BaseControlScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GameplayControls
        m_GameplayControls = asset.FindActionMap("GameplayControls", throwIfNotFound: true);
        m_GameplayControls_Move = m_GameplayControls.FindAction("Move", throwIfNotFound: true);
        m_GameplayControls_Look = m_GameplayControls.FindAction("Look", throwIfNotFound: true);
        m_GameplayControls_Jump = m_GameplayControls.FindAction("Jump", throwIfNotFound: true);
        m_GameplayControls_Crouch = m_GameplayControls.FindAction("Crouch", throwIfNotFound: true);
        m_GameplayControls_Shoot = m_GameplayControls.FindAction("Shoot", throwIfNotFound: true);
        m_GameplayControls_Action = m_GameplayControls.FindAction("Action", throwIfNotFound: true);
        m_GameplayControls_Reload = m_GameplayControls.FindAction("Reload", throwIfNotFound: true);
        m_GameplayControls_Interact = m_GameplayControls.FindAction("Interact", throwIfNotFound: true);
        m_GameplayControls_LayerUp = m_GameplayControls.FindAction("LayerUp", throwIfNotFound: true);
        m_GameplayControls_LayerDown = m_GameplayControls.FindAction("LayerDown", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // GameplayControls
    private readonly InputActionMap m_GameplayControls;
    private List<IGameplayControlsActions> m_GameplayControlsActionsCallbackInterfaces = new List<IGameplayControlsActions>();
    private readonly InputAction m_GameplayControls_Move;
    private readonly InputAction m_GameplayControls_Look;
    private readonly InputAction m_GameplayControls_Jump;
    private readonly InputAction m_GameplayControls_Crouch;
    private readonly InputAction m_GameplayControls_Shoot;
    private readonly InputAction m_GameplayControls_Action;
    private readonly InputAction m_GameplayControls_Reload;
    private readonly InputAction m_GameplayControls_Interact;
    private readonly InputAction m_GameplayControls_LayerUp;
    private readonly InputAction m_GameplayControls_LayerDown;
    public struct GameplayControlsActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GameplayControls_Move;
        public InputAction @Look => m_Wrapper.m_GameplayControls_Look;
        public InputAction @Jump => m_Wrapper.m_GameplayControls_Jump;
        public InputAction @Crouch => m_Wrapper.m_GameplayControls_Crouch;
        public InputAction @Shoot => m_Wrapper.m_GameplayControls_Shoot;
        public InputAction @Action => m_Wrapper.m_GameplayControls_Action;
        public InputAction @Reload => m_Wrapper.m_GameplayControls_Reload;
        public InputAction @Interact => m_Wrapper.m_GameplayControls_Interact;
        public InputAction @LayerUp => m_Wrapper.m_GameplayControls_LayerUp;
        public InputAction @LayerDown => m_Wrapper.m_GameplayControls_LayerDown;
        public InputActionMap Get() { return m_Wrapper.m_GameplayControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayControlsActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayControlsActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @Action.started += instance.OnAction;
            @Action.performed += instance.OnAction;
            @Action.canceled += instance.OnAction;
            @Reload.started += instance.OnReload;
            @Reload.performed += instance.OnReload;
            @Reload.canceled += instance.OnReload;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @LayerUp.started += instance.OnLayerUp;
            @LayerUp.performed += instance.OnLayerUp;
            @LayerUp.canceled += instance.OnLayerUp;
            @LayerDown.started += instance.OnLayerDown;
            @LayerDown.performed += instance.OnLayerDown;
            @LayerDown.canceled += instance.OnLayerDown;
        }

        private void UnregisterCallbacks(IGameplayControlsActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @Action.started -= instance.OnAction;
            @Action.performed -= instance.OnAction;
            @Action.canceled -= instance.OnAction;
            @Reload.started -= instance.OnReload;
            @Reload.performed -= instance.OnReload;
            @Reload.canceled -= instance.OnReload;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @LayerUp.started -= instance.OnLayerUp;
            @LayerUp.performed -= instance.OnLayerUp;
            @LayerUp.canceled -= instance.OnLayerUp;
            @LayerDown.started -= instance.OnLayerDown;
            @LayerDown.performed -= instance.OnLayerDown;
            @LayerDown.canceled -= instance.OnLayerDown;
        }

        public void RemoveCallbacks(IGameplayControlsActions instance)
        {
            if (m_Wrapper.m_GameplayControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayControlsActions @GameplayControls => new GameplayControlsActions(this);
    private int m_BaseControlSchemeSchemeIndex = -1;
    public InputControlScheme BaseControlSchemeScheme
    {
        get
        {
            if (m_BaseControlSchemeSchemeIndex == -1) m_BaseControlSchemeSchemeIndex = asset.FindControlSchemeIndex("BaseControlScheme");
            return asset.controlSchemes[m_BaseControlSchemeSchemeIndex];
        }
    }
    public interface IGameplayControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnLayerUp(InputAction.CallbackContext context);
        void OnLayerDown(InputAction.CallbackContext context);
    }
}
