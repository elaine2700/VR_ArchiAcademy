using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Handle : MonoBehaviour
{
    public enum handleDirection { north, east, south, west }
    public handleDirection handleDir;

    public bool isActive = false;
    [SerializeField] bool debugMode = false;
    public GameObject pointer; // RayCast hit position
    XRRayInteractor xrRayInteractor;

    [SerializeField] ThemeSettings themeSettings;
    Actions inputActions;

    public UnityEvent OnPlacedHandle;

    MeshRenderer meshRenderer;
    GridTile gridTile;
    Blockfloor_V2 blockFloor;

    float buttonValue;
    public bool dragging = false;
    bool isOnGrid = false;

    private void Awake()
    {
        inputActions = new Actions();
        //inputActions.Interaction.Confirm.performed += _ => SetHandleInactive();
        inputActions.Interaction.Drag.performed += cntxt => buttonValue = cntxt.ReadValue<float>();
        inputActions.Interaction.Drag.canceled += cntxt => buttonValue = 0;
        inputActions.Interaction.Drag.canceled += _ => SetHandleInactive();
        gridTile = FindObjectOfType<GridTile>();
    }

    private void Start()
    {
        //changeMaterial = FindObjectOfType<ChangeMaterial>();
        blockFloor = GetComponentInParent<Blockfloor_V2>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        SetHandleInactive();
        meshRenderer.material = themeSettings.inactiveHandleMat;
    }

    private void Update()
    {
        /*if (!debugMode)
        {
            //dragging = buttonValue > 0.5f;
            if (!dragging)
                //SetHandleInactive();
                isOnGrid = StillOnGrid();
        }*/
        if (debugMode)
        {
            //isOnGrid = true;
            dragging = true;
        }

        if (isActive)  //&& dragging)
        {
            dragging = buttonValue > 0.5f;
            if (!dragging)
                return;
            Vector3 newPos = new Vector3();
            if (!debugMode)
            {
                bool rayHit = xrRayInteractor.TryGetHitInfo(out newPos, out _, out _, out _);
                if (!rayHit)
                {
                    return;
                }
            }
            else
            {
                newPos = pointer.transform.position;
            }

            if (FindOverlapsInDirection(handleDir))
            {
                // if there is an overlap in the perimeter,
                // this function checks the position the user wants to
                // move the handle, if it is in the opposite direction of
                // the overlap the handle can move, otherwise it exits and doesn't
                // change the position
                if (!CanChangeSize(newPos))
                {
                    return;
                }
            }
            // Sets the handle in a NewPos
            Vector3 newHandlePos = gridTile.SnapPosition(newPos, true);
            if (isActive)
                transform.position = ConstrainPosition(newHandlePos);
        }
    }

    private bool FindOverlapsInDirection(handleDirection direction)
    {
        bool foundOverlap = false;
        List<OverlapFinder> overlapFinders = new List<OverlapFinder>();
        switch (direction)
        {
            case handleDirection.north:
                overlapFinders = blockFloor.northOverlapFinders;
                break;
            case handleDirection.east:
                overlapFinders = blockFloor.eastOverlapFinders;
                break;
            case handleDirection.south:
                overlapFinders = blockFloor.southOverlapFinders;
                break;
            case handleDirection.west:
                overlapFinders = blockFloor.westOverlapFinders;
                break;
        }
        if (handleDir == direction)
        {
            foreach (OverlapFinder overlap in overlapFinders)
            {
                if (!overlap.FindAvailablePosition())
                {
                    foundOverlap = true;
                }
            }
        }
        return foundOverlap;
    }

    private bool CanChangeSize(Vector3 newPos)
    {
        bool canChangeSize = true;
        if (handleDir == handleDirection.north)
        {
            if (newPos.y > transform.position.y)
            {
                canChangeSize = false;
            }
        }
        if (handleDir == handleDirection.east)
        {
            if(newPos.x > transform.position.x)
            {
                canChangeSize = false;
            }
        }
        if(handleDir == handleDirection.south)
        {
            if (newPos.y < transform.position.y)
            {
                canChangeSize = false;
            }
        }
        if(handleDir == handleDirection.west)
        {
            if (newPos.x < transform.position.x)
            {
                canChangeSize = false;
            }
        }
        return canChangeSize;
    }

    private void OnEnable()
    {
        inputActions.Interaction.Enable();
    }

    private void OnDisable()
    {
        inputActions.Interaction.Disable();
    }

    /// <summary>
    /// Constrains the position in only one direction depending
    /// on the enum settings of the handle.
    /// </summary>
    /// <param name="followPos"></param>
    /// <returns></returns>
    private Vector3 ConstrainPosition(Vector3 followPos)
    {
        Vector3 constrainedPos = transform.position;
        switch (handleDir)
        {
            case handleDirection.north:
                constrainedPos.z = followPos.z;
                break;
            case handleDirection.east:
                constrainedPos.x = followPos.x;
                break;
            case handleDirection.south:
                constrainedPos.z = followPos.z;
                break;
            case handleDirection.west:
                constrainedPos.x = followPos.x;
                break;
        }
        return constrainedPos;
    }

    public void SetHandleActive(SelectEnterEventArgs args)
    {
        //Debug.Log("Set Handle Active");
        isActive = true;
        meshRenderer.material = themeSettings.activeHandleMat;
        pointer = args.interactorObject.transform.gameObject;
        xrRayInteractor = pointer.GetComponent<XRRayInteractor>();
        //Debug.Log(pointer.name);
    }

    // Called from button X, or after deselecting handle.
    public void SetHandleInactive()
    {
        isActive = false;
        dragging = false;
        OnPlacedHandle.Invoke();
    }

    public void HoverHandle(bool isHovering)
    {
        // called from xr event
        if (isHovering == true)
            meshRenderer.material = themeSettings.hoveredBlockMaterial;
        else
            meshRenderer.material = themeSettings.inactiveHandleMat;
    }

    private bool StillOnGrid()
    {
        bool stillOnGrid = false;
        //stillOnGrid = gridTile.isHovered;
        //stillOnGrid = true;
        // Search if handle is inside grid collider bounds
        stillOnGrid = gridTile.GetComponentInChildren<Collider>().bounds.Contains(transform.position);
        return stillOnGrid;
    }

}
