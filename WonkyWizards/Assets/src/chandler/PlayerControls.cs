// GENERATED AUTOMATICALLY FROM 'Assets/src/chandler/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerDefault"",
            ""id"": ""367c3de2-ebd3-4a9c-8368-63ff62a0582a"",
            ""actions"": [
                {
                    ""name"": ""BasicMovement"",
                    ""type"": ""Button"",
                    ""id"": ""b7cb41a3-a531-474b-b923-f1c39d77a496"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""932f82b4-aa99-45ae-ad15-87b06d32637b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""BasicMovement"",
                    ""id"": ""8903b530-7463-42de-b7f5-2ca24e2ee121"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5b87da94-86ac-4a52-aab0-a02fbedd9386"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8316d142-38ab-4ed3-aa87-f88aef406128"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6cbc8067-980e-43b6-95aa-141292b9372d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b778293b-7420-4862-8a5f-66b7219dae5e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bee3b5be-838f-4d42-abb2-42d90e6d34b0"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerDefault
        m_PlayerDefault = asset.FindActionMap("PlayerDefault", throwIfNotFound: true);
        m_PlayerDefault_BasicMovement = m_PlayerDefault.FindAction("BasicMovement", throwIfNotFound: true);
        m_PlayerDefault_MousePosition = m_PlayerDefault.FindAction("MousePosition", throwIfNotFound: true);
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

    // PlayerDefault
    private readonly InputActionMap m_PlayerDefault;
    private IPlayerDefaultActions m_PlayerDefaultActionsCallbackInterface;
    private readonly InputAction m_PlayerDefault_BasicMovement;
    private readonly InputAction m_PlayerDefault_MousePosition;
    public struct PlayerDefaultActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerDefaultActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @BasicMovement => m_Wrapper.m_PlayerDefault_BasicMovement;
        public InputAction @MousePosition => m_Wrapper.m_PlayerDefault_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_PlayerDefault; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerDefaultActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerDefaultActions instance)
        {
            if (m_Wrapper.m_PlayerDefaultActionsCallbackInterface != null)
            {
                @BasicMovement.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnBasicMovement;
                @BasicMovement.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnBasicMovement;
                @BasicMovement.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnBasicMovement;
                @MousePosition.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_PlayerDefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @BasicMovement.started += instance.OnBasicMovement;
                @BasicMovement.performed += instance.OnBasicMovement;
                @BasicMovement.canceled += instance.OnBasicMovement;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public PlayerDefaultActions @PlayerDefault => new PlayerDefaultActions(this);
    public interface IPlayerDefaultActions
    {
        void OnBasicMovement(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
