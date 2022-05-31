using UnityEngine;

public class Handle : MonoBehaviour
{
    enum handleDirection { north, east, south, west }
    [SerializeField] handleDirection handleDir;
    [SerializeField] bool isActive = false;
    [SerializeField] GameObject pointer;

    GridTile gridTile;
    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
        gridTile = FindObjectOfType<GridTile>();
        ConstrainPosition();
    }
    //show when floor is in edition mode (selected)
    // Snap to grid

    private void Update()
    {
        if(isActive)
            transform.position = gridTile.SnapPosition(pointer.transform.position);
    }

    

    private void ConstrainPosition()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        switch (handleDir)
        {
            case handleDirection.north:
                rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                break;
            case handleDirection.east:
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                break;
            case handleDirection.south:
                rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                break;
            case handleDirection.west:
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                break;
        }
    }
}
