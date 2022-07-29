using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class Blockfloor_V2 : MonoBehaviour
{
    [SerializeField] UnitFloor floorUnit;
    [SerializeField] Transform unitsParent;
    [SerializeField] TextMeshPro nameField;
    public List<Handle> handles = new List<Handle>();

    [SerializeField] List<UnitFloor> unitFloorTiles = new List<UnitFloor>();

    public List<OverlapFinder> northOverlapFinders = new List<OverlapFinder>();
    public List<OverlapFinder> eastOverlapFinders = new List<OverlapFinder>();
    public List<OverlapFinder> southOverlapFinders = new List<OverlapFinder>();
    public List<OverlapFinder> westOverlapFinders = new List<OverlapFinder>();

    [Range(0f,2f)]
    [SerializeField] float adjustmentCenter = 0.5f;
   
    string roomName;

    Vector2Int roomSize;
    public Vector2Int RoomSize { get { return roomSize; } }

    Vector2 unitSize;
    Vector3 center;

    Handle handleNorth;
    Handle handleEast;
    Handle handleSouth;
    Handle handleWest;

    GridLayers gridLayers;
    RoomType areaType;
    TransformBlock blockTransform;
    BlocksTracker blocksTracker;
    PreviewBlock previewBlock;

    int count = 0;

    private void Awake()
    {
        //baseInteractable = GetComponent<XRSimpleInteractable>();
    }

    private void OnEnable()
    {
        //baseInteractable.selectEntered.AddListener(EditFloor);
        foreach (Handle handle in handles)
        {
            handle.OnPlacedHandle.AddListener(UpdateFloorInformation);
        }
    }

    private void OnDisable()
    {
        //baseInteractable.selectEntered.RemoveListener(EditFloor);
        foreach (Handle handle in handles)
        {
            handle.OnPlacedHandle.RemoveListener(UpdateFloorInformation);
        }
    }

    private void Start()
    {
        gridLayers = FindObjectOfType<GridLayers>();
        transform.parent = gridLayers.ParentToCurrentLayer(1).transform;

        areaType = FindObjectOfType<RoomType>();
        nameField.text = areaType.area;

        blockTransform = GetComponent<TransformBlock>();
        previewBlock = GetComponent<PreviewBlock>();
        SetHandles();
        blockTransform.MakeBlockEditable(false);
        //ShowHandles(false);
        unitSize = new Vector2(1, 1);
        blocksTracker = FindObjectOfType<BlocksTracker>();
        blocksTracker.AddRoomToList(this);
        FindName();
        center.y = 0.05f;
        //CalculateRoomSize();
        //ConstructFloor();
    }

    private void Update()
    {
        //isEditing = blockTransform.editSize && GetComponent<Block>().currentBlockState == Block.blockState.selected;
        //Debug.Log($"isEditing: {isEditing}");
        EditSize();
    }

    private void EditSize()
    {
        ShowHandles(blockTransform.isEditing);
        if (blockTransform.isEditableSize)
        {
            // update size
            if (SeeIfHandleMoved())
            {
                CalculateRoomSize();
                ConstructFloor();
                UpdateHandlesPosition();
                CalculateCenter();
                //DeleteOverlapFinders();
            }
            //UpdateHandlesPosition();
            
        }
    }

    private void UpdateFloorInformation()
    {
        UpdateCollider();
        //DeleteOverlapFinders();
        
    }

    public void SetAreaType()
    {
        roomName = areaType.area;
        nameField.text = roomName;
    }

    private void SetHandles()
    {
        foreach (Handle handle in handles)
        {
            switch (handle.handleDir)
            {
                case Handle.handleDirection.north:
                    handleNorth = handle;
                    break;
                case Handle.handleDirection.east:
                    handleEast = handle;
                    break;
                case Handle.handleDirection.south:
                    handleSouth = handle;
                    break;
                case Handle.handleDirection.west:
                    handleWest = handle;
                    break;
            }
        }
        ShowHandles(false);
    }

    public void ShowHandles(bool showHandles)
    {
        foreach(Handle handle in handles)
        {
            handle.gameObject.SetActive(showHandles);
        }
    }

    /// <summary>
    /// Calculates the new room size with the distance of each handle position.
    /// </summary>
    private void CalculateRoomSize()
    {
        // width  x
        int width = Mathf.RoundToInt((handleWest.transform.position.x - handleEast.transform.position.x) / unitSize.x);
        // depth z
        int depth = Mathf.RoundToInt((handleSouth.transform.position.z - handleNorth.transform.position.z) / unitSize.y);
        //Debug.Log($"width: {width}, depth: {depth}");
        roomSize.x = Mathf.Abs(width);
        roomSize.y = Mathf.Abs(depth);
    }

    private void ConstructFloor()
    {
        DeleteUnits();
        previewBlock.meshesWithMaterials.Clear();
        unitFloorTiles.Clear();
        DeleteOverlapFinders();
        // The number starts at -1 because it creates an invisible perimeter of UnitFloors
        // that check if the next grid unit is free of objects.
        for (int x = -1; x < roomSize.x + 1; x++)
        {
            for (int y = -1; y < roomSize.y + 1; y++)
            {
                if (x == -1 && y == -1)
                    continue;
                else if (x == roomSize.x && y == -1)
                    continue;
                else if (x == -1 && y == roomSize.y)
                    continue;
                else if (x == roomSize.x && y == roomSize.y)
                    continue;
                else
                {
                    UnitFloor newUnit = Instantiate(floorUnit, unitsParent);
                    newUnit.gameObject.SetActive(true);
                    if (x == -1)
                    {
                        newUnit.OverlapFinder();
                        westOverlapFinders.Add(newUnit.GetComponentInChildren<OverlapFinder>());
                    }
                    else if(y == -1)
                    {
                        newUnit.OverlapFinder();
                        northOverlapFinders.Add(newUnit.GetComponentInChildren<OverlapFinder>());
                    }
                    else if(x == roomSize.x)
                    {
                        newUnit.OverlapFinder();
                        eastOverlapFinders.Add(newUnit.GetComponentInChildren<OverlapFinder>());
                    }
                    else if(y == roomSize.y)
                    {
                        newUnit.OverlapFinder();
                        southOverlapFinders.Add(newUnit.GetComponentInChildren<OverlapFinder>());
                    }
                    // todo adjust to every scale.
                    // set position or local position. first position starts at handlewest pos.x + adjustment.
                    float posX = handleWest.transform.position.x + x + 0.5f;
                    float posZ = handleNorth.transform.position.z - y - 0.5f;
                    newUnit.transform.position = new Vector3(posX, 0f, posZ);
                    if(newUnit.isOverlapFinder == false)
                    {
                        unitFloorTiles.Add(newUnit);
                        previewBlock.meshesWithMaterials.Add(newUnit.GetComponentInChildren<Renderer>());
                    }
                        
                }
                
            }
        }
        /*foreach (UnitFloor unitTile in unitFloorTiles)
        {

            if(!unitTile.isOverlapFinder)
                previewBlock.meshesWithMaterials.Add(unitTile.GetComponentInChildren<Renderer>());
        }*/
        //UpdateCollider();
    }

    private void DeleteUnits()
    {
        Debug.Log("Deleting Units");
        foreach(UnitFloor unit in unitFloorTiles)
        {
            Destroy(unit.gameObject);
        }
    }

    public void DeleteOverlapFinders()
    {
        if(northOverlapFinders.Count >= 1)
        {
            foreach (OverlapFinder overlapFinder in northOverlapFinders)
            {
                Destroy(overlapFinder.transform.parent.gameObject);
            }
        }
        northOverlapFinders.Clear();

        if (eastOverlapFinders.Count >= 1)
        {
            foreach (OverlapFinder overlapFinder in eastOverlapFinders)
            {
                Destroy(overlapFinder.transform.parent.gameObject);
            }
        }
        eastOverlapFinders.Clear();

        if (southOverlapFinders.Count >= 1)
        {
            foreach (OverlapFinder overlapFinder in southOverlapFinders)
            {
                Destroy(overlapFinder.transform.parent.gameObject);
            }
        }
        southOverlapFinders.Clear();

        if (westOverlapFinders.Count >= 1)
        {
            foreach (OverlapFinder overlapFinder in westOverlapFinders)
            {
                Destroy(overlapFinder.transform.parent.gameObject);
            }
        }
        westOverlapFinders.Clear();
    }

    private bool SeeIfHandleMoved()
    {
        bool handleMoved = false;
        foreach (Handle handle in handles)
        {
            if (handle.isActive && handle.dragging)
            {
                handleMoved = true;
                break;
            }   
        }
        return handleMoved;
    }

    /// <summary>
    /// Updates the position of handles that are not being dragged by the selector.
    /// </summary>
    private void UpdateHandlesPosition()
    {
        
        foreach (Handle handle in handles)
        {
            if (!handle.isActive)
            {
                Vector3 currentPos = handle.transform.position;
                if (handle.handleDir == Handle.handleDirection.north || handle.handleDir == Handle.handleDirection.south)
                {
                    float newPosX = ((handleWest.transform.position.x + handleEast.transform.position.x) / 2);
                    handle.transform.position = new Vector3(newPosX, currentPos.y, currentPos.z);
                    
                }
                if(handle.handleDir == Handle.handleDirection.west || handle.handleDir == Handle.handleDirection.east)
                {
                    float newPosZ = ((handleSouth.transform.position.z + handleNorth.transform.position.z) / 2);
                    handle.transform.position = new Vector3(currentPos.x, currentPos.y, newPosZ);
                    
                } 
            }
        }   
    }

    private void CalculateCenter()
    {
        float centerX = (handleWest.transform.localPosition.x + handleEast.transform.localPosition.x) / 2;
        float centerY = ((handleSouth.transform.localPosition.z + handleNorth.transform.localPosition.z) / 2);

        //float centerX = transform.TransformPoint(handleWest.transform.position + handleEast.transform.position).x;
        //float centerY = transform.TransformPoint(handleSouth.transform.position + handleNorth.transform.position).y;
        //centerY += adjustmentCenter;
        //centerX += adjustmentCenter;

        center = new Vector3(centerX, center.y, centerY);
        nameField.GetComponent<RoomNameLabel>().UpdateLabelPosition(center);
    }

    public void EditFloor(SelectEnterEventArgs args)
    {
        blockTransform.MakeBlockEditable(!blockTransform.isEditing);
        Debug.Log($"EditingFloor: {blockTransform.isEditing}");
        ShowHandles(blockTransform.isEditing);
        //DeleteOverlapFinders();
    }

    private void FindName()
    {

    }

    private void UpdateCollider()
    {
        /*CalculateCenter();
        BoxCollider boxCollider = GetComponent<Block>().colliders[0];
        boxCollider.center = center;
        boxCollider.size = new Vector3(roomSize.x, 0.09f, roomSize.y);
        */

        Block block = GetComponent<Block>();
        block.colliders.Clear();
        foreach(UnitFloor unit in unitFloorTiles)
        {
            //baseInteractable.colliders.Add(unit.GetComponentInChildren<Collider>());
            block.colliders.Add(unit.GetComponentInChildren<BoxCollider>());
        }
        block.EnableColliders(true);
    }

}
