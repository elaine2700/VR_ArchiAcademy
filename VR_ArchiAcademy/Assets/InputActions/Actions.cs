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
    public struct InteractionActions
    {
        private @Actions m_Wrapper;
        public InteractionActions(@Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Confirm => m_Wrapper.m_Interaction_Confirm;
        public InputAction @RotateBlock => m_Wrapper.m_Interaction_RotateBlock;
        public InputAction @ChooseBlock => m_Wrapper.m_Interaction_ChooseBlock;
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
            }
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);
    public interface IInteractionActions
    {
        void OnConfirm(InputAction.CallbackContext context);
        void OnRotateBlock(InputAction.CallbackContext context);
        void OnChooseBlock(InputAction.CallbackContext context);
    }
}
