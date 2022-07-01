using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] XRRayInteractor leftRayInteractor;
    [SerializeField] TeleportationProvider teleportationProvider;
    XRIDefaultInputActions inputActions;
    bool isActive;
    float thumbstickInput;
    RaycastHit hit;

    private void Awake()
    {
        inputActions = new XRIDefaultInputActions();
        inputActions.XRILeftHandLocomotion.TeleportModeActivate.performed += _ => OnTeleportActivate();
        inputActions.XRILeftHandLocomotion.TeleportModeActivate.canceled += _ => OnTeleportCancel();
        //inputActions.XRILeftHandLocomotion.TeleportModeCancel.performed += _ => OnTeleportCancel();
        //inputActions.XRILeftHandLocomotion.Move.performed += cntxt => thumbstickInput = cntxt.ReadValue<float>();
        //inputActions.XRILeftHandLocomotion.Move.canceled += cntxt => thumbstickInput = 0;
    }

    private void Start()
    {
        leftRayInteractor.enabled = false;
    }


    private void OnEnable()
    {
        inputActions.XRILeftHandLocomotion.Enable();
    }

    private void OnDisable()
    {
        inputActions.XRILeftHandLocomotion.Disable();
    }

    void OnTeleportActivate()
    {
        leftRayInteractor.enabled = true;
        isActive = true;
    }

    void OnTeleportCancel()
    {
        if (!leftRayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            leftRayInteractor.enabled = false;
            isActive = false;
            return;
        }
        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        teleportationProvider.QueueTeleportRequest(request);
        leftRayInteractor.enabled = false;
        isActive = false;
    }
}
