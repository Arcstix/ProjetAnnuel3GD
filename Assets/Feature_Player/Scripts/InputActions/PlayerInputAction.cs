//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Feature_Player/Scripts/InputActions/PlayerInputAction.inputactions
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

public partial class @PlayerInputAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""7b9554ac-318b-4325-8d9c-f6f1df1a53ee"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""9e299fb6-3296-4636-ac30-f3affdc1a898"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FreeLook"",
                    ""type"": ""Value"",
                    ""id"": ""38036733-1254-4c3e-9040-80f663aee4b4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""WalkToggle"",
                    ""type"": ""Button"",
                    ""id"": ""7fac0d22-402f-4679-b899-3db442abfc04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Throw/RecallRight"",
                    ""type"": ""Button"",
                    ""id"": ""b424d459-e885-4d6d-8783-77cf0972ce52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Throw/RecallLeft"",
                    ""type"": ""Button"",
                    ""id"": ""8c22dd22-e0c0-40ec-b9d4-45bb885829f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AttractionRight"",
                    ""type"": ""Button"",
                    ""id"": ""33d9deac-5310-4f6c-80f2-d9b48e3e98aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.4)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AttractionLeft"",
                    ""type"": ""Button"",
                    ""id"": ""312cac2c-cdc5-4c36-8152-ed9d476c1bd2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.4)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchAttraction"",
                    ""type"": ""Button"",
                    ""id"": ""c87b8402-16f8-4233-bed1-f040e519dd60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""44ec42c2-cc3f-4e12-987d-fd785c608b06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""1ed233cd-6c1c-45ed-8b4d-f80af3ac905a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Clamp(min=-0.1,max=0.1),Invert"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraToggle"",
                    ""type"": ""Button"",
                    ""id"": ""ce194096-1956-4135-a950-66b61da22aae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""6fc3a7e2-6bab-40e8-9990-b865e813e78f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4412c176-10b2-4846-bedc-dc68ae7f9b7c"",
                    ""path"": ""<Keyboard>/#(Z)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""452934c9-ea3d-4999-9e82-753455577b41"",
                    ""path"": ""<Keyboard>/#(S)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""74604b9a-f029-4781-8a93-8b38904b4bfe"",
                    ""path"": ""<Keyboard>/#(Q)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2de9e646-3323-4ea9-8b7b-a434feb53c29"",
                    ""path"": ""<Keyboard>/#(D)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""01ede8f5-3a25-489f-b637-a51d4d4a2c9c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c13ca7e-e6ab-49a6-8319-48857f6c2cbb"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""DeltaTimeScale"",
                    ""groups"": """",
                    ""action"": ""FreeLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7d7b27a-fe9c-40bc-bb87-ccc1e4112a14"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8bd97ac-aeb7-4be5-91fb-f97fa3aad5c1"",
                    ""path"": ""<Keyboard>/#(M)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WalkToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5c2d149-e97c-4bcb-99e7-59ca68b689bb"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WalkToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46a0eeee-fe28-4eb7-8728-e8da1780ba92"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw/RecallRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""116877c3-1f77-4b0f-83be-d7b1c4d72732"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw/RecallRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b2ced4f-0bf1-4b83-83b6-65bb63aca2c8"",
                    ""path"": ""<Keyboard>/#(R)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchAttraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80b31d52-f2f9-4cf4-8e74-fbfada95b0cb"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchAttraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""226d0694-b560-4f2b-8711-c67711f90d8d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""caf67bc2-a3d2-465d-bc25-ca67f867a716"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b64c54e8-0983-4c6e-a193-cc7ffb2520b2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw/RecallLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""245487ae-857a-466f-8477-c2d7933074c1"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw/RecallLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a164121-e92b-4397-ab8a-317b6b9fe071"",
                    ""path"": ""<Keyboard>/#(E)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttractionRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b395b725-d16e-46d7-a437-5afb302f3e45"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttractionRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e0f40ce-d59b-4230-a0f5-1442a0955b82"",
                    ""path"": ""<Keyboard>/#(A)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttractionLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ae72019-0c1a-4c3e-9c4d-1213e0582c6c"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttractionLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7a40088-e463-48fb-8afc-6071ebc9ef29"",
                    ""path"": ""<Keyboard>/#(A)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d18ae059-0ca5-4409-8012-43e895847a83"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_FreeLook = m_Player.FindAction("FreeLook", throwIfNotFound: true);
        m_Player_WalkToggle = m_Player.FindAction("WalkToggle", throwIfNotFound: true);
        m_Player_ThrowRecallRight = m_Player.FindAction("Throw/RecallRight", throwIfNotFound: true);
        m_Player_ThrowRecallLeft = m_Player.FindAction("Throw/RecallLeft", throwIfNotFound: true);
        m_Player_AttractionRight = m_Player.FindAction("AttractionRight", throwIfNotFound: true);
        m_Player_AttractionLeft = m_Player.FindAction("AttractionLeft", throwIfNotFound: true);
        m_Player_SwitchAttraction = m_Player.FindAction("SwitchAttraction", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Zoom = m_Player.FindAction("Zoom", throwIfNotFound: true);
        m_Player_CameraToggle = m_Player.FindAction("CameraToggle", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_FreeLook;
    private readonly InputAction m_Player_WalkToggle;
    private readonly InputAction m_Player_ThrowRecallRight;
    private readonly InputAction m_Player_ThrowRecallLeft;
    private readonly InputAction m_Player_AttractionRight;
    private readonly InputAction m_Player_AttractionLeft;
    private readonly InputAction m_Player_SwitchAttraction;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Zoom;
    private readonly InputAction m_Player_CameraToggle;
    public struct PlayerActions
    {
        private @PlayerInputAction m_Wrapper;
        public PlayerActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @FreeLook => m_Wrapper.m_Player_FreeLook;
        public InputAction @WalkToggle => m_Wrapper.m_Player_WalkToggle;
        public InputAction @ThrowRecallRight => m_Wrapper.m_Player_ThrowRecallRight;
        public InputAction @ThrowRecallLeft => m_Wrapper.m_Player_ThrowRecallLeft;
        public InputAction @AttractionRight => m_Wrapper.m_Player_AttractionRight;
        public InputAction @AttractionLeft => m_Wrapper.m_Player_AttractionLeft;
        public InputAction @SwitchAttraction => m_Wrapper.m_Player_SwitchAttraction;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Zoom => m_Wrapper.m_Player_Zoom;
        public InputAction @CameraToggle => m_Wrapper.m_Player_CameraToggle;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @FreeLook.started += instance.OnFreeLook;
            @FreeLook.performed += instance.OnFreeLook;
            @FreeLook.canceled += instance.OnFreeLook;
            @WalkToggle.started += instance.OnWalkToggle;
            @WalkToggle.performed += instance.OnWalkToggle;
            @WalkToggle.canceled += instance.OnWalkToggle;
            @ThrowRecallRight.started += instance.OnThrowRecallRight;
            @ThrowRecallRight.performed += instance.OnThrowRecallRight;
            @ThrowRecallRight.canceled += instance.OnThrowRecallRight;
            @ThrowRecallLeft.started += instance.OnThrowRecallLeft;
            @ThrowRecallLeft.performed += instance.OnThrowRecallLeft;
            @ThrowRecallLeft.canceled += instance.OnThrowRecallLeft;
            @AttractionRight.started += instance.OnAttractionRight;
            @AttractionRight.performed += instance.OnAttractionRight;
            @AttractionRight.canceled += instance.OnAttractionRight;
            @AttractionLeft.started += instance.OnAttractionLeft;
            @AttractionLeft.performed += instance.OnAttractionLeft;
            @AttractionLeft.canceled += instance.OnAttractionLeft;
            @SwitchAttraction.started += instance.OnSwitchAttraction;
            @SwitchAttraction.performed += instance.OnSwitchAttraction;
            @SwitchAttraction.canceled += instance.OnSwitchAttraction;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @CameraToggle.started += instance.OnCameraToggle;
            @CameraToggle.performed += instance.OnCameraToggle;
            @CameraToggle.canceled += instance.OnCameraToggle;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @FreeLook.started -= instance.OnFreeLook;
            @FreeLook.performed -= instance.OnFreeLook;
            @FreeLook.canceled -= instance.OnFreeLook;
            @WalkToggle.started -= instance.OnWalkToggle;
            @WalkToggle.performed -= instance.OnWalkToggle;
            @WalkToggle.canceled -= instance.OnWalkToggle;
            @ThrowRecallRight.started -= instance.OnThrowRecallRight;
            @ThrowRecallRight.performed -= instance.OnThrowRecallRight;
            @ThrowRecallRight.canceled -= instance.OnThrowRecallRight;
            @ThrowRecallLeft.started -= instance.OnThrowRecallLeft;
            @ThrowRecallLeft.performed -= instance.OnThrowRecallLeft;
            @ThrowRecallLeft.canceled -= instance.OnThrowRecallLeft;
            @AttractionRight.started -= instance.OnAttractionRight;
            @AttractionRight.performed -= instance.OnAttractionRight;
            @AttractionRight.canceled -= instance.OnAttractionRight;
            @AttractionLeft.started -= instance.OnAttractionLeft;
            @AttractionLeft.performed -= instance.OnAttractionLeft;
            @AttractionLeft.canceled -= instance.OnAttractionLeft;
            @SwitchAttraction.started -= instance.OnSwitchAttraction;
            @SwitchAttraction.performed -= instance.OnSwitchAttraction;
            @SwitchAttraction.canceled -= instance.OnSwitchAttraction;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @CameraToggle.started -= instance.OnCameraToggle;
            @CameraToggle.performed -= instance.OnCameraToggle;
            @CameraToggle.canceled -= instance.OnCameraToggle;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFreeLook(InputAction.CallbackContext context);
        void OnWalkToggle(InputAction.CallbackContext context);
        void OnThrowRecallRight(InputAction.CallbackContext context);
        void OnThrowRecallLeft(InputAction.CallbackContext context);
        void OnAttractionRight(InputAction.CallbackContext context);
        void OnAttractionLeft(InputAction.CallbackContext context);
        void OnSwitchAttraction(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnCameraToggle(InputAction.CallbackContext context);
    }
}
