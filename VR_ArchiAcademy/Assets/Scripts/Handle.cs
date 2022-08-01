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
        inputActions.Interaction.Drag.performed += cntxt => buttonValue = cntxt.ReadValue<float>();
        inputActions.Interaction.Drag.canceled += cntxt => buttonValue = 0;
        inputActions.Interaction.Drag.canceled += _ => SetHandleInactive();
        gridTile = FindObjectOfType<GridTile>();
    }

    private void Start()
    {
        blockFloor = GetComponentInParent<Blockfloor_V2>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        SetHandleInactive();
        meshRenderer.material = themeSettings.inactiveHandleMat;
    }

    private void OnEnable()
    {
        inputActions.Interaction.Enable();
    }

    private void OnDisable()
    {
        inputActions.Interaction.Disable();
    }

    private void Update()
    {
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

            if (!CanChangeSize(newPos))
            {
                return;
            }
            
            // Sets the handle in a NewPos
            Vector3 newHandlePos = gridTile.SnapPosition(newPos, true);
            if (isActive)
                transform.position = ConstrainPosition(newHandlePos);
        }
    }

    /// <summary>
    /// Finds overlap in the direction of this handle and returns true
    /// if it finds something
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
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

    /// <summary>
    /// First it checks the direction of the handle,
    /// then, sees in which direction it wants to move.
    /// If the newPos is towards the outside, it checks if there are objects in the perimeter.
    /// If the newPos goes towards the inside, it can move until the size is minimum 1.
    /// </summary>
    /// <param name="newPos"></param>
    /// <returns>Returns false when there are objects with colliders in the perimeter,
    /// or when it tries to change smaller than 1.
    /// </returns>
    private bool CanChangeSize(Vector3 newPos)
    {
        bool canChangeSize = true;
        
        if (handleDir == handleDirection.north)
        {
            // If the newPos is bigger than before, it tries to find
            // overlap objects in the perimeter, if there are objects in the next grid unit,
            // it cannot change size.
            if (newPos.z > transform.position.z)
            {
                if(FindOverlapsInDirection(handleDir))
                    canChangeSize = false;
            }
            // If the newPos makes the roomSize to be smaller than 1,
            // the handle cannot move towards that direction.
            else if(newPos.z < transform.position.z)
            {
                if(blockFloor.RoomSize.y == 1)
                    canChangeSize = false;
            }
        }
        if (handleDir == handleDirection.east)
        {
            if(newPos.x > transform.position.x)
            {
                if(FindOverlapsInDirection(handleDir))
                    canChangeSize = false;
            }
            else if (newPos.x < transform.position.x)
            {
                if(blockFloor.RoomSize.x == 1)
                    canChangeSize = false;
            }
        }
        if(handleDir == handleDirection.south)
        {
            if (newPos.z < transform.position.z)
            {
                if (FindOverlapsInDirection(handleDir))
                    canChangeSize = false;
            }
            else if (newPos.z > transform.position.z)
            {
                if (blockFloor.RoomSize.y == 1)
                    canChangeSize = false;
            }
        }
        if(handleDir == handleDirection.west)
        {
            if (newPos.x < transform.position.x)
            {
                if (FindOverlapsInDirection(handleDir))
                    canChangeSize = false;
            }
            else if (newPos.x > transform.position.x)
            {
                if (blockFloor.RoomSize.x == 1)
                    canChangeSize = false;
            }
        }
        return canChangeSize;
    }

    /// <summary>
    /// Constrains the position in only one axis depending
    /// on the enum settings (direction) of the handle.
    /// Example: The north handle can only move in the z axis.
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
        isActive = true;
        meshRenderer.material = themeSettings.activeHandleMat;
        pointer = args.interactorObject.transform.gameObject;
        xrRayInteractor = pointer.GetComponent<XRRayInteractor>();
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
        // Search if handle is inside grid collider bounds
        stillOnGrid = gridTile.GetComponentInChildren<Collider>().bounds.Contains(transform.position);
        return stillOnGrid;
    }

}
