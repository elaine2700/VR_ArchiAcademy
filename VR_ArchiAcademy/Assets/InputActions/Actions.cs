//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/InputActions/Actions.inputactions
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

public partial class @Actions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Actions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""Interaction"",
            ""id"": ""33486981-3d03-4779-aeda-97864e97c4d8"",
            ""actions"": [
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""3896307a-d462-4d19-9492-b9772368e073"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateBlock"",
                    ""type"": ""Value"",
                    ""id"": ""2145e2f9-5a0a-4cea-aa12-5a6cf92703b4"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ChooseBlock"",
                    ""type"": ""Button"",
                    ""id"": ""665ca700-5fd4-46d6-b957-169f25a472e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""Value"",
                    ""id"": ""3a57694f-42c0-4d33-9c52-63cf2369514c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fcd85f58-4cb8-4579-bc7a-ff99929e7639"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d4449a9-102a-45b3-8ecf-d97afe5f22e6"",
                    ""path"": ""<HID::Unknown Joy-Con (R)>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b6e50e0-ee00-48fb-b59b-d193a9deddc8"",
                    ""path"": ""<XRController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8935dd9f-76bf-40a2-97f6-45adaeb789f7"",
                    ""path"": ""<HID::Unknown Joy-Con (R)>/hat"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3292e11e-158e-4151-a4e0-4ea45e7c83b8"",
                    ""path"": ""<XRController>{RightHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3594bd96-c4d1-411e-8f29-481ee72e5385"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChooseBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0a95453-bfa9-4fda-9496-c097c4032357"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChooseBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aebc2914-0fcb-453b-a9fc-26bdc187429b"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb1966aa-eed5-4d78-9b48-b73e5eb3675f"",
                    ""path"": ""<HID::Unknown Joy-Con (R)>/button16"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Tools"",
            ""id"": ""59c1f930-d793-4c05-8226-61a25c6caac6"",
            ""actions"": [
                {
                    ""name"": ""Swaptools"",
                    ""type"": ""Button"",
                    ""id"": ""983d4f0c-4351-4b0c-ae49-e027936bace1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScaleWorld"",
                    ""type"": ""Button"",
                    ""id"": ""0c17b7cf-fa27-41e9-949b-9a70c68a901a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""da4252b1-978c-49a3-98da-75430bf74f90"",
                    ""path"": ""<HID::Unknown Joy-Con (R)>/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScaleWorld"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4893d55-1778-4a08-be00-5bde99637a01"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScaleWorld"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8885433-1ac8-4944-bcab-17c1ae12da2a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScaleWorld"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4115a38-ad34-4588-b925-79a99f6eb954"",
                    ""path"": ""<HID::Unknown Joy-Con (R)>/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swaptools"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Interaction
        m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
        m_Interaction_Confirm = m_Interaction.FindAction("Confirm", throwIfNotFound: true);
        m_Interaction_RotateBlock = m_Interaction.FindAction("RotateBlock", throwIfNotFound: true);
        m_Interaction_ChooseBlock = m_Interaction.FindAction("ChooseBlock", throwIfNotFound: true);
        m_Interaction_Drag = m_Interaction.FindAction("Drag", throwIfNotFound: true);
        // Tools
        m_Tools = asset.FindActionMap("Tools", throwIfNotFound: true);
        m_Tools_Swaptools = m_Tools.FindAction("Swaptools", throwIfNotFound: true);
        m_Tools_ScaleWorld = m_Tools.FindAction("ScaleWorld", throwIfNotFound: true);
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

    // Interaction
    private readonly InputActionMap m_Interaction;
    private IInteractionActions m_InteractionActionsCallbackInterface;
    private readonly InputAction m_Interaction_Confirm;
    private readonly InputAction m_Interaction_RotateBlock;
    private readonly InputAction m_Interaction_ChooseBlock;
    private readonly InputAction m_Interaction_Drag;
    public struct InteractionActions
    {
        private @Actions m_Wrapper;
        public InteractionActions(@Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Confirm => m_Wrapper.m_Interaction_Confirm;
        public InputAction @RotateBlock => m_Wrapper.m_Interaction_RotateBlock;
        public InputAction @ChooseBlock => m_Wrapper.m_Interaction_ChooseBlock;
        public InputAction @Drag => m_Wrapper.m_Interaction_Drag;
        public InputActionMap Get() { return m_Wrapper.m_Interaction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
        public void SetCallbacks(IInteractionActions instance)
        {
            if (m_Wrapper.m_InteractionActionsCallbackInterface != null)
            {
                @Confirm.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnConfirm;
                @RotateBlock.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnRotateBlock;
                @RotateBlock.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnRotateBlock;
                @RotateBlock.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnRotateBlock;
                @ChooseBlock.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnChooseBlock;
                @ChooseBlock.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnChooseBlock;
                @ChooseBlock.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnChooseBlock;
                @Drag.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnDrag;
                @Drag.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnDrag;
                @Drag.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnDrag;
            }
            m_Wrapper.m_InteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @RotateBlock.started += instance.OnRotateBlock;
                @RotateBlock.performed += instance.OnRotateBlock;
                @RotateBlock.canceled += instance.OnRotateBlock;
                @ChooseBlock.started += instance.OnChooseBlock;
                @ChooseBlock.performed += instance.OnChooseBlock;
                @ChooseBlock.canceled += instance.OnChooseBlock;
                @Drag.started += instance.OnDrag;
                @Drag.performed += instance.OnDrag;
                @Drag.canceled += instance.OnDrag;
            }
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);

    // Tools
    private readonly InputActionMap m_Tools;
    private IToolsActions m_ToolsActionsCallbackInterface;
    private readonly InputAction m_Tools_Swaptools;
    private readonly InputAction m_Tools_ScaleWorld;
    public struct ToolsActions
    {
        private @Actions m_Wrapper;
        public ToolsActions(@Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Swaptools => m_Wrapper.m_Tools_Swaptools;
        public InputAction @ScaleWorld => m_Wrapper.m_Tools_ScaleWorld;
        public InputActionMap Get() { return m_Wrapper.m_Tools; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ToolsActions set) { return set.Get(); }
        public void SetCallbacks(IToolsActions instance)
        {
            if (m_Wrapper.m_ToolsActionsCallbackInterface != null)
            {
                @Swaptools.started -= m_Wrapper.m_ToolsActionsCallbackInterface.OnSwaptools;
                @Swaptools.performed -= m_Wrapper.m_ToolsActionsCallbackInterface.OnSwaptools;
                @Swaptools.canceled -= m_Wrapper.m_ToolsActionsCallbackInterface.OnSwaptools;
                @ScaleWorld.started -= m_Wrapper.m_ToolsActionsCallbackInterface.OnScaleWorld;
                @ScaleWorld.performed -= m_Wrapper.m_ToolsActionsCallbackInterface.OnScaleWorld;
                @ScaleWorld.canceled -= m_Wrapper.m_ToolsActionsCallbackInterface.OnScaleWorld;
            }
            m_Wrapper.m_ToolsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Swaptools.started += instance.OnSwaptools;
                @Swaptools.performed += instance.OnSwaptools;
                @Swaptools.canceled += instance.OnSwaptools;
                @ScaleWorld.started += instance.OnScaleWorld;
                @ScaleWorld.performed += instance.OnScaleWorld;
                @ScaleWorld.canceled += instance.OnScaleWorld;
            }
        }
    }
    public ToolsActions @Tools => new ToolsActions(this);
    public interface IInteractionActions
    {
        void OnConfirm(InputAction.CallbackContext context);
        void OnRotateBlock(InputAction.CallbackContext context);
        void OnChooseBlock(InputAction.CallbackContext context);
        void OnDrag(InputAction.CallbackContext context);
    }
    public interface IToolsActions
    {
        void OnSwaptools(InputAction.CallbackContext context);
        void OnScaleWorld(InputAction.CallbackContext context);
    }
}
