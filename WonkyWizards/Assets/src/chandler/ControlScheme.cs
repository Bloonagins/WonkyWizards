// GENERATED AUTOMATICALLY FROM 'Assets/src/chandler/ControlScheme.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ControlScheme : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlScheme()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlScheme"",
    ""maps"": [
        {
            ""name"": ""PlayerDefault"",
            ""id"": ""367c3de2-ebd3-4a9c-8368-63ff62a0582a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""b7cb41a3-a531-474b-b923-f1c39d77a496"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cast"",
                    ""type"": ""Button"",
                    ""id"": ""675ed597-16c3-4db5-8d3f-17e0dbb28a7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Summon"",
                    ""type"": ""Button"",
                    ""id"": ""73cc0ef1-bad0-4e09-907d-9f73a7c074aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchMagicMode"",
                    ""type"": ""Button"",
                    ""id"": ""6d21ec47-28e6-4dbb-83e1-5c74e6382adc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar1"",
                    ""type"": ""Button"",
                    ""id"": ""88e68661-ecb8-4d28-a108-5a3452882a19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar2"",
                    ""type"": ""Button"",
                    ""id"": ""50c1aede-e6d7-4a92-86eb-3e77c0d471db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar3"",
                    ""type"": ""Button"",
                    ""id"": ""a2f2f24b-6a2b-4ea4-bac4-4f1acc3a0dfd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar4"",
                    ""type"": ""Button"",
                    ""id"": ""293b346d-bdd5-4ee2-b845-6f09310ff457"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar5"",
                    ""type"": ""Button"",
                    ""id"": ""a00e5810-f029-4268-af81-d192d29f9535"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar6"",
                    ""type"": ""Button"",
                    ""id"": ""9293d9fb-50f4-44a4-8fa6-7a9f7d2205c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar7"",
                    ""type"": ""Button"",
                    ""id"": ""cbe7c482-3102-4357-a83b-9615b8b67fea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar8"",
                    ""type"": ""Button"",
                    ""id"": ""2bb0972f-5027-4eca-995c-ddf64ec9137c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar9"",
                    ""type"": ""Button"",
                    ""id"": ""11bbf251-9a98-4afd-aa0d-e2bf56e656cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar0"",
                    ""type"": ""Button"",
                    ""id"": ""8b1f6193-83c1-44d9-9bf4-8c0ed54d8618"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""330e3230-eb9f-424b-8de2-dd20fd3d7832"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FlipSpawner"",
                    ""type"": ""Button"",
                    ""id"": ""094ec46e-b694-4586-a8e9-568f6dd21a0c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BossSpawn"",
                    ""type"": ""Button"",
                    ""id"": ""654272e5-2dca-4202-9cb2-39f77c29334d"",
                    ""expectedControlType"": ""Button"",
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
                    ""action"": ""Move"",
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
                    ""action"": ""Move"",
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
                    ""action"": ""Move"",
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
                    ""action"": ""Move"",
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
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0b6e1208-74c5-460c-b556-5be406e597b1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01f95a50-a424-43aa-9565-0ef446cf22da"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchMagicMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b03476b5-746d-4643-ab15-f3703968eafd"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb50b802-cfe4-435f-9354-f84d5cad7465"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b9db222-06b2-4ce7-b3b5-0f0393823793"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8af9e7d8-740f-49a2-96f2-19ad34b4b2cf"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f161bd9-5217-4dfc-8eee-e6f4018a8d7a"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb1e620a-fd98-4ddd-94ce-78c91131515f"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ca2c4e4-0158-4838-93cd-9cc6e6e6a415"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d2b419b-f01e-4b4f-ace4-8c944cd1b80e"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c3a5b1e-25e5-419b-86ce-b3ae2a0b5942"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82f50221-961f-4cb6-b254-5118e98f7be0"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60e32346-d9ec-4c86-b67a-5259775da7d5"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d2699ea-d4ab-4af0-8faf-9150d3895038"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Summon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bbf5400-f3e4-456e-b552-adde26415cb3"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FlipSpawner"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2fc6ee3-6fd1-4979-bd54-1ecae3218acf"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BossSpawn"",
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
        m_PlayerDefault_Move = m_PlayerDefault.FindAction("Move", throwIfNotFound: true);
        m_PlayerDefault_Cast = m_PlayerDefault.FindAction("Cast", throwIfNotFound: true);
        m_PlayerDefault_Summon = m_PlayerDefault.FindAction("Summon", throwIfNotFound: true);
        m_PlayerDefault_SwitchMagicMode = m_PlayerDefault.FindAction("SwitchMagicMode", throwIfNotFound: true);
        m_PlayerDefault_Hotbar1 = m_PlayerDefault.FindAction("Hotbar1", throwIfNotFound: true);
        m_PlayerDefault_Hotbar2 = m_PlayerDefault.FindAction("Hotbar2", throwIfNotFound: true);
        m_PlayerDefault_Hotbar3 = m_PlayerDefault.FindAction("Hotbar3", throwIfNotFound: true);
        m_PlayerDefault_Hotbar4 = m_PlayerDefault.FindAction("Hotbar4", throwIfNotFound: true);
        m_PlayerDefault_Hotbar5 = m_PlayerDefault.FindAction("Hotbar5", throwIfNotFound: true);
        m_PlayerDefault_Hotbar6 = m_PlayerDefault.FindAction("Hotbar6", throwIfNotFound: true);
        m_PlayerDefault_Hotbar7 = m_PlayerDefault.FindAction("Hotbar7", throwIfNotFound: true);
        m_PlayerDefault_Hotbar8 = m_PlayerDefault.FindAction("Hotbar8", throwIfNotFound: true);
        m_PlayerDefault_Hotbar9 = m_PlayerDefault.FindAction("Hotbar9", throwIfNotFound: true);
        m_PlayerDefault_Hotbar0 = m_PlayerDefault.FindAction("Hotbar0", throwIfNotFound: true);
        m_PlayerDefault_Pause = m_PlayerDefault.FindAction("Pause", throwIfNotFound: true);
        m_PlayerDefault_FlipSpawner = m_PlayerDefault.FindAction("FlipSpawner", throwIfNotFound: true);
        m_PlayerDefault_BossSpawn = m_PlayerDefault.FindAction("BossSpawn", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerDefault_Move;
    private readonly InputAction m_PlayerDefault_Cast;
    private readonly InputAction m_PlayerDefault_Summon;
    private readonly InputAction m_PlayerDefault_SwitchMagicMode;
    private readonly InputAction m_PlayerDefault_Hotbar1;
    private readonly InputAction m_PlayerDefault_Hotbar2;
    private readonly InputAction m_PlayerDefault_Hotbar3;
    private readonly InputAction m_PlayerDefault_Hotbar4;
    private readonly InputAction m_PlayerDefault_Hotbar5;
    private readonly InputAction m_PlayerDefault_Hotbar6;
    private readonly InputAction m_PlayerDefault_Hotbar7;
    private readonly InputAction m_PlayerDefault_Hotbar8;
    private readonly InputAction m_PlayerDefault_Hotbar9;
    private readonly InputAction m_PlayerDefault_Hotbar0;
    private readonly InputAction m_PlayerDefault_Pause;
    private readonly InputAction m_PlayerDefault_FlipSpawner;
    private readonly InputAction m_PlayerDefault_BossSpawn;
    public struct PlayerDefaultActions
    {
        private @ControlScheme m_Wrapper;
        public PlayerDefaultActions(@ControlScheme wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerDefault_Move;
        public InputAction @Cast => m_Wrapper.m_PlayerDefault_Cast;
        public InputAction @Summon => m_Wrapper.m_PlayerDefault_Summon;
        public InputAction @SwitchMagicMode => m_Wrapper.m_PlayerDefault_SwitchMagicMode;
        public InputAction @Hotbar1 => m_Wrapper.m_PlayerDefault_Hotbar1;
        public InputAction @Hotbar2 => m_Wrapper.m_PlayerDefault_Hotbar2;
        public InputAction @Hotbar3 => m_Wrapper.m_PlayerDefault_Hotbar3;
        public InputAction @Hotbar4 => m_Wrapper.m_PlayerDefault_Hotbar4;
        public InputAction @Hotbar5 => m_Wrapper.m_PlayerDefault_Hotbar5;
        public InputAction @Hotbar6 => m_Wrapper.m_PlayerDefault_Hotbar6;
        public InputAction @Hotbar7 => m_Wrapper.m_PlayerDefault_Hotbar7;
        public InputAction @Hotbar8 => m_Wrapper.m_PlayerDefault_Hotbar8;
        public InputAction @Hotbar9 => m_Wrapper.m_PlayerDefault_Hotbar9;
        public InputAction @Hotbar0 => m_Wrapper.m_PlayerDefault_Hotbar0;
        public InputAction @Pause => m_Wrapper.m_PlayerDefault_Pause;
        public InputAction @FlipSpawner => m_Wrapper.m_PlayerDefault_FlipSpawner;
        public InputAction @BossSpawn => m_Wrapper.m_PlayerDefault_BossSpawn;
        public InputActionMap Get() { return m_Wrapper.m_PlayerDefault; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerDefaultActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerDefaultActions instance)
        {
            if (m_Wrapper.m_PlayerDefaultActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnMove;
                @Cast.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnCast;
                @Cast.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnCast;
                @Cast.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnCast;
                @Summon.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnSummon;
                @Summon.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnSummon;
                @Summon.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnSummon;
                @SwitchMagicMode.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnSwitchMagicMode;
                @SwitchMagicMode.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnSwitchMagicMode;
                @SwitchMagicMode.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnSwitchMagicMode;
                @Hotbar1.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar1;
                @Hotbar1.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar1;
                @Hotbar1.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar1;
                @Hotbar2.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar2;
                @Hotbar2.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar2;
                @Hotbar2.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar2;
                @Hotbar3.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar3;
                @Hotbar3.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar3;
                @Hotbar3.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar3;
                @Hotbar4.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar4;
                @Hotbar4.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar4;
                @Hotbar4.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar4;
                @Hotbar5.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar5;
                @Hotbar5.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar5;
                @Hotbar5.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar5;
                @Hotbar6.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar6;
                @Hotbar6.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar6;
                @Hotbar6.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar6;
                @Hotbar7.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar7;
                @Hotbar7.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar7;
                @Hotbar7.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar7;
                @Hotbar8.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar8;
                @Hotbar8.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar8;
                @Hotbar8.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar8;
                @Hotbar9.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar9;
                @Hotbar9.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar9;
                @Hotbar9.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar9;
                @Hotbar0.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar0;
                @Hotbar0.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar0;
                @Hotbar0.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnHotbar0;
                @Pause.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnPause;
                @FlipSpawner.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnFlipSpawner;
                @FlipSpawner.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnFlipSpawner;
                @FlipSpawner.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnFlipSpawner;
                @BossSpawn.started -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnBossSpawn;
                @BossSpawn.performed -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnBossSpawn;
                @BossSpawn.canceled -= m_Wrapper.m_PlayerDefaultActionsCallbackInterface.OnBossSpawn;
            }
            m_Wrapper.m_PlayerDefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Cast.started += instance.OnCast;
                @Cast.performed += instance.OnCast;
                @Cast.canceled += instance.OnCast;
                @Summon.started += instance.OnSummon;
                @Summon.performed += instance.OnSummon;
                @Summon.canceled += instance.OnSummon;
                @SwitchMagicMode.started += instance.OnSwitchMagicMode;
                @SwitchMagicMode.performed += instance.OnSwitchMagicMode;
                @SwitchMagicMode.canceled += instance.OnSwitchMagicMode;
                @Hotbar1.started += instance.OnHotbar1;
                @Hotbar1.performed += instance.OnHotbar1;
                @Hotbar1.canceled += instance.OnHotbar1;
                @Hotbar2.started += instance.OnHotbar2;
                @Hotbar2.performed += instance.OnHotbar2;
                @Hotbar2.canceled += instance.OnHotbar2;
                @Hotbar3.started += instance.OnHotbar3;
                @Hotbar3.performed += instance.OnHotbar3;
                @Hotbar3.canceled += instance.OnHotbar3;
                @Hotbar4.started += instance.OnHotbar4;
                @Hotbar4.performed += instance.OnHotbar4;
                @Hotbar4.canceled += instance.OnHotbar4;
                @Hotbar5.started += instance.OnHotbar5;
                @Hotbar5.performed += instance.OnHotbar5;
                @Hotbar5.canceled += instance.OnHotbar5;
                @Hotbar6.started += instance.OnHotbar6;
                @Hotbar6.performed += instance.OnHotbar6;
                @Hotbar6.canceled += instance.OnHotbar6;
                @Hotbar7.started += instance.OnHotbar7;
                @Hotbar7.performed += instance.OnHotbar7;
                @Hotbar7.canceled += instance.OnHotbar7;
                @Hotbar8.started += instance.OnHotbar8;
                @Hotbar8.performed += instance.OnHotbar8;
                @Hotbar8.canceled += instance.OnHotbar8;
                @Hotbar9.started += instance.OnHotbar9;
                @Hotbar9.performed += instance.OnHotbar9;
                @Hotbar9.canceled += instance.OnHotbar9;
                @Hotbar0.started += instance.OnHotbar0;
                @Hotbar0.performed += instance.OnHotbar0;
                @Hotbar0.canceled += instance.OnHotbar0;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @FlipSpawner.started += instance.OnFlipSpawner;
                @FlipSpawner.performed += instance.OnFlipSpawner;
                @FlipSpawner.canceled += instance.OnFlipSpawner;
                @BossSpawn.started += instance.OnBossSpawn;
                @BossSpawn.performed += instance.OnBossSpawn;
                @BossSpawn.canceled += instance.OnBossSpawn;
            }
        }
    }
    public PlayerDefaultActions @PlayerDefault => new PlayerDefaultActions(this);
    public interface IPlayerDefaultActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnCast(InputAction.CallbackContext context);
        void OnSummon(InputAction.CallbackContext context);
        void OnSwitchMagicMode(InputAction.CallbackContext context);
        void OnHotbar1(InputAction.CallbackContext context);
        void OnHotbar2(InputAction.CallbackContext context);
        void OnHotbar3(InputAction.CallbackContext context);
        void OnHotbar4(InputAction.CallbackContext context);
        void OnHotbar5(InputAction.CallbackContext context);
        void OnHotbar6(InputAction.CallbackContext context);
        void OnHotbar7(InputAction.CallbackContext context);
        void OnHotbar8(InputAction.CallbackContext context);
        void OnHotbar9(InputAction.CallbackContext context);
        void OnHotbar0(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnFlipSpawner(InputAction.CallbackContext context);
        void OnBossSpawn(InputAction.CallbackContext context);
    }
}
