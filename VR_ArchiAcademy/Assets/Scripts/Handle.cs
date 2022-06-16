using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Handle : MonoBehaviour
{
    public enum handleDirection { north, east, south, west }
    public handleDirection handleDir;

    [SerializeField] bool isActive = false;
    [SerializeField] bool debugMode = false;
    public GameObject pointer; // RayCast hit position
    XRRayInteractor xrRayInteractor;

    ThemeSettings themeSettings;
    Actions inputActions;

    MeshRenderer meshRenderer;
    GridTile gridTile;

    float buttonValue;
    bool dragging = false;

    private void Awake()
    {
        themeSettings = FindObjectOfType<ThemeSettings>();
        inputActions = new Actions();
        inputActions.Interaction.Confirm.performed += _ => SetHandleInactive();
        inputActions.Interaction.Drag.performed += cntxt => buttonValue = cntxt.ReadValue<float>();
        inputActions.Interaction.Drag.canceled += cntxt => buttonValue = 0;
    }

    private void Start()
    {
        gridTile = FindObjectOfType<GridTile>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        SetHandleInactive();
    }

    private void Update()
    {
        
        dragging = buttonValue > 0.5f;
        if (!dragging)
            SetHandleInactive();

        if (isActive && StillOnGrid() && dragging)
        {
            //FindOverlaps();
            Vector3 newPos = new Vector3();
            if (!debugMode)
            {
                xrRayInteractor.TryGetHitInfo(out newPos, out _, out _, out _);
            }
            else
            {
                newPos = pointer.transform.position;
            }
            
            // Sets the handle in a NewPos
            Vector3 newHandlePos = gridTile.SnapPosition(newPos, true);
            transform.position = ConstrainPosition(newHandlePos);

        }
    }

    private void OnEnable()
    {
        inputActions.Interaction.Enable();
    }

    private void OnDisable()
    {
        inputActions.Interaction.Disable();
    }

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
        Debug.Log("Set Handle Active");
        isActive = true;
        meshRenderer.material = themeSettings.activeHandleMat;
        pointer = args.interactorObject.transform.gameObject;
        xrRayInteractor = pointer.GetComponent<XRRayInteractor>();
        Debug.Log(pointer.name);
        // todo if button is not still pressed, set handle inactive
    }


    // Called from button X
    public void SetHandleInactive()
    {
        Debug.Log("Set Handle Inactive");
        isActive = false;
        meshRenderer.material = themeSettings.inactiveHandleMat;
        BlockFloor floor = GetComponentInParent<BlockFloor>();
        floor.UpdateSize();
        // todo set here updateblockSize()?
        //pointer = null;
        //GetComponent<BlockFloor>().ReconvertVertices();
    }


    public void HoverHandle()
    {
        Debug.Log("hovering Handle");
    }

    private void FindOverlaps()
    {
        // todo
        // check collisions with physics,overlapsphere
        Collider[] otherColliders = Physics.OverlapSphere(transform.position, 0.00125f);
        if(otherColliders.Length > 0)
        {
            isActive = false;
            Debug.Log("colliding with something");
        }
    }

    private bool StillOnGrid()
    {
        bool isOnGrid;
        // todo
        // When not hovering on grid stop moving
        // and disable isActive
        //isOnGrid = gridTile.isHovered;
        isOnGrid = true;
        // todo rewrite if transform is in position over grid
        // get collider bounds // get max and min
        // and compare to position
        return isOnGrid;
    }
}
