using UnityEngine;

public class Handle : MonoBehaviour
{
    public enum handleDirection { north, east, south, west }
    public handleDirection handleDir;

    [SerializeField] bool isActive = false;
    [SerializeField] GameObject pointer;

    ThemeSettings themeSettings;

    MeshRenderer meshRenderer;
    GridTile gridTile;

    private void Awake()
    {
        themeSettings = FindObjectOfType<ThemeSettings>();
    }

    private void Start()
    {
        gridTile = FindObjectOfType<GridTile>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        SetHandleInactive();
    }

    private void Update()
    {
        if (isActive)
        {
            Vector3 newHandlePos = gridTile.SnapPosition(pointer.transform.position);
            transform.position = ConstrainPosition(newHandlePos);
        }    
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

    public void SetHandleActive()
    {
        isActive = true;
        meshRenderer.material = themeSettings.activeHandleMat;
    }

    public void SetHandleInactive()
    {
        isActive = false;
        meshRenderer.material = themeSettings.inactiveHandleMat;
    }
}
