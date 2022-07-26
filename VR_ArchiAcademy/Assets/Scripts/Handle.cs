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

    public UnityEvent OnPlacedHandle; // todo to call constructFloor();

    MeshRenderer meshRenderer;
    GridTile gridTile;
    //ChangeMaterial changeMaterial;

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
        
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        SetHandleInactive();
        meshRenderer.material = themeSettings.inactiveHandleMat;
    }

    private void Update()
    {
        if (!debugMode)
        {
            dragging = buttonValue > 0.5f;
            if (!dragging)
                //SetHandleInactive();
            isOnGrid = StillOnGrid();
        }
        if (debugMode)
        {
            isOnGrid = true;
            dragging = true;
        }

        Debug.Log($"isActive: {isActive}, isOnGrid: {isOnGrid}, dragging: {dragging}");
        if (isActive && isOnGrid && dragging)
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
            if(isActive)
                transform.position = ConstrainPosition(newHandlePos);
            // todo update information
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
        //Debug.Log(pointer.name);
    }


    // Called from button X
    public void SetHandleInactive()
    {
        isActive = false;
        Debug.Log("Handle Inactive");
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
    
    private void FindOverlaps()
    {
        // todo
        // check collisions with physics,overlapsphere
        Collider[] otherColliders = Physics.OverlapSphere(transform.position, 0.05f);
        if(otherColliders.Length > 0)
        {
            isActive = false;
            Debug.Log("colliding with something");
        }
    }

    private void Test()
    {
        Debug.Log(" Test");
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
