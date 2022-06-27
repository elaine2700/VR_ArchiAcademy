using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class Blockfloor_V2 : MonoBehaviour
{
    [SerializeField] Transform floorUnit;
    [SerializeField] Transform unitsParent;
    [SerializeField] TextMeshPro nameField;
    [SerializeField] List<Handle> handles = new List<Handle>();

    [SerializeField] List<GameObject> unitFloorTiles = new List<GameObject>();
   
    string roomName;

    Vector2 roomSize;
    Vector2 unitSize;
    Vector3 center;

    Handle handleNorth;
    Handle handleEast;
    Handle handleSouth;
    Handle handleWest;

    GridLayers gridLayers;
    AreaType areaType;
    TransformBlock blockTransform;
    BlocksTracker blocksTracker;
    XRSimpleInteractable baseInteractable;
    PreviewBlock previewBlock;

    int count = 0;

    private void Awake()
    {
        baseInteractable = GetComponent<XRSimpleInteractable>();
    }

    private void OnEnable()
    {
        baseInteractable.selectEntered.AddListener(EditFloor);
    }

    private void OnDisable()
    {
        baseInteractable.selectEntered.RemoveListener(EditFloor);
    }

    private void Start()
    {
        gridLayers = FindObjectOfType<GridLayers>();
        transform.parent = gridLayers.ParentToCurrentLayer(1).transform;

        areaType = FindObjectOfType<AreaType>();
        blockTransform = GetComponent<TransformBlock>();
        previewBlock = GetComponent<PreviewBlock>();

        SetHandles();
        blockTransform.MakeBlockEditable(false);
        ShowHandles(false);
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
        ShowHandles(blockTransform.isEditing);
        if (blockTransform.isEditableSize)
        {
            // update size
            
            CalculateRoomSize();
            if (SeeIfHandleMoved())
            {
                //count++;
                //Debug.Log($"Construct count: {count}");
                ConstructFloor();
            }
            UpdateHandlesPosition();
            
        }
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

    private void ShowHandles(bool showHandles)
    {
        foreach(Handle handle in handles)
        {
            handle.gameObject.SetActive(showHandles);
        }
    }

    private void CalculateRoomSize()
    {
        // width  x
        int width = Mathf.RoundToInt((handleWest.transform.position.x - handleEast.transform.position.x) / unitSize.x);
        // depth z
        int depth = Mathf.RoundToInt((handleSouth.transform.position.z - handleNorth.transform.position.z) / unitSize.y);
        //Debug.Log($"width: {width}, depth: {depth}");
        roomSize.x = Mathf.Abs(width);
        roomSize.y = Mathf.Abs(depth);

        float centerX = (handleWest.transform.localPosition.x + handleEast.transform.localPosition.x) / 2;
        float centerY = (handleSouth.transform.localPosition.y + handleNorth.transform.localPosition.y) / 2;
        center = new Vector3(centerX, center.y, centerY);
    }

    private void ConstructFloor()
    {
        DeleteFloor();
        previewBlock.meshesWithMaterials.Clear();
        unitFloorTiles.Clear();

        for (int x = 0; x < roomSize.x; x++)
        {
            for (int y = 0; y < roomSize.y; y++)
            {
                Transform newUnit = Instantiate(floorUnit, unitsParent);
                newUnit.gameObject.SetActive(true);
                // todo adjust to every scale.
                // set position or local position. first position starts at handlewest pos.x + adjustment.
                float posX = handleWest.transform.position.x + x + 0.5f;
                float posZ = handleNorth.transform.position.z - y - 0.5f;
                newUnit.transform.position = new Vector3(posX, 0.05f, posZ);
                unitFloorTiles.Add(newUnit.gameObject);

            }
        }
        foreach (GameObject unitTile in unitFloorTiles)
        {
            previewBlock.meshesWithMaterials.Add(unitTile.GetComponentInChildren<Renderer>());
        }
        UpdateCollider();
    }

    private void DeleteFloor()
    {
        foreach(GameObject unit in unitFloorTiles)
        {
            Destroy(unit);
        }
    }

    private bool SeeIfHandleMoved()
    {
        bool handleMoved = false;
        foreach (Handle handle in handles)
        {
            if (handle.dragging)
                handleMoved = true;
        }
        return handleMoved;
    }

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

    private void EditFloor(SelectEnterEventArgs args)
    {
        blockTransform.MakeBlockEditable(!blockTransform.isEditing);
        ShowHandles(blockTransform.isEditing);
    }

    private void FindName()
    {

    }

    private void UpdateCollider()
    {
        BoxCollider boxCollider = GetComponent<Block>().blockMaincollider.GetComponent<BoxCollider>();
        boxCollider.center = center;
        boxCollider.size = new Vector3(roomSize.x, 0.09f, roomSize.y);
    }


}
