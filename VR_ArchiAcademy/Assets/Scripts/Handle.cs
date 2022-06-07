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

    private void Awake()
    {
        themeSettings = FindObjectOfType<ThemeSettings>();
        inputActions = new Actions();
        inputActions.Interaction.Confirm.performed += _ => SetHandleInactive();
    }

    private void Start()
    {
        gridTile = FindObjectOfType<GridTile>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        SetHandleInactive();
    }

    private void Update()
    {
        if (isActive) // todo && onGrid
        {
            Vector3 newPos = new Vector3();
            if (!debugMode)
            {
                xrRayInteractor.TryGetHitInfo(out newPos, out _, out _, out _);
            }
            else
            {
                newPos = pointer.transform.position;
            }
            Vector3 newHandlePos = gridTile.SnapPosition(newPos);
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

    }


    // Called from button X
    private void SetHandleInactive()
    {
        Debug.Log("Set Handle Inactive");
        isActive = false;
        meshRenderer.material = themeSettings.inactiveHandleMat;
        //pointer = null;
        //GetComponent<BlockFloor>().ReconvertVertices();
    }

    public void HoverHandle()
    {
        Debug.Log("hovering Handle");
    }
}
