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
                    ""name"": ""SelectBlock"",
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
                    ""expectedControlType"": """",
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
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectBlock"",
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
        },
        {
            ""name"": ""Switch"",
            ""id"": ""eb8eac4c-98f6-4c94-9e7c-c44cc97fb2b7"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""2b3047a7-4cb0-42b3-947e-c715e3f2c9c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectValue"",
                    ""type"": ""Value"",
                    ""id"": ""916499a2-f4fa-4bc3-bca2-b2dff7e13edd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""58356aeb-3133-4541-9156-1110c6b1d618"",
                    ""path"": ""<HID::Unknown Joy-Con (R)>/button15"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""636f222a-8854-4ea0-933d-a4fbb8d1b3ca"",
                    ""path"": ""<HID::Unknown Joy-Con (R)>/button15"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectValue"",
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
        m_Interaction_SelectBlock = m_Interaction.FindAction("SelectBlock", throwIfNotFound: true);
        m_Interaction_RotateBlock = m_Interaction.FindAction("RotateBlock", throwIfNotFound: true);
        m_Interaction_ChooseBlock = m_Interaction.FindAction("ChooseBlock", throwIfNotFound: true);
        // Switch
        m_Switch = asset.FindActionMap("Switch", throwIfNotFound: true);
        m_Switch_Select = m_Switch.FindAction("Select", throwIfNotFound: true);
        m_Switch_SelectValue = m_Switch.FindAction("SelectValue", throwIfNotFound: true);
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
    private readonly InputAction m_Interaction_SelectBlock;
    private readonly InputAction m_Interaction_RotateBlock;
    private readonly InputAction m_Interaction_ChooseBlock;
    public struct InteractionActions
    {
        private @Actions m_Wrapper;
        public InteractionActions(@Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @SelectBlock => m_Wrapper.m_Interaction_SelectBlock;
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
                @SelectBlock.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnSelectBlock;
                @SelectBlock.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnSelectBlock;
                @SelectBlock.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnSelectBlock;
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
                @SelectBlock.started += instance.OnSelectBlock;
                @SelectBlock.performed += instance.OnSelectBlock;
                @SelectBlock.canceled += instance.OnSelectBlock;
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

    // Switch
    private readonly InputActionMap m_Switch;
    private ISwitchActions m_SwitchActionsCallbackInterface;
    private readonly InputAction m_Switch_Select;
    private readonly InputAction m_Switch_SelectValue;
    public struct SwitchActions
    {
        private @Actions m_Wrapper;
        public SwitchActions(@Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_Switch_Select;
        public InputAction @SelectValue => m_Wrapper.m_Switch_SelectValue;
        public InputActionMap Get() { return m_Wrapper.m_Switch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SwitchActions set) { return set.Get(); }
        public void SetCallbacks(ISwitchActions instance)
        {
            if (m_Wrapper.m_SwitchActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_SwitchActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_SwitchActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_SwitchActionsCallbackInterface.OnSelect;
                @SelectValue.started -= m_Wrapper.m_SwitchActionsCallbackInterface.OnSelectValue;
                @SelectValue.performed -= m_Wrapper.m_SwitchActionsCallbackInterface.OnSelectValue;
                @SelectValue.canceled -= m_Wrapper.m_SwitchActionsCallbackInterface.OnSelectValue;
            }
            m_Wrapper.m_SwitchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @SelectValue.started += instance.OnSelectValue;
                @SelectValue.performed += instance.OnSelectValue;
                @SelectValue.canceled += instance.OnSelectValue;
            }
        }
    }
    public SwitchActions @Switch => new SwitchActions(this);
    public interface IInteractionActions
    {
        void OnSelectBlock(InputAction.CallbackContext context);
        void OnRotateBlock(InputAction.CallbackContext context);
        void OnChooseBlock(InputAction.CallbackContext context);
    }
    public interface ISwitchActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnSelectValue(InputAction.CallbackContext context);
    }
}
