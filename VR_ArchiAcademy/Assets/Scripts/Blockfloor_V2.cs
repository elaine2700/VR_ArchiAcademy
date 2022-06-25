using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blockfloor_V2 : MonoBehaviour
{
    [SerializeField] Transform floorUnit;
    [SerializeField] Transform unitsParent;
    List<Transform> unitFloorTiles = new List<Transform>();
    [SerializeField] List<Handle> handles = new List<Handle>();
    [SerializeField] TextMeshPro nameField;
    AreaType areaType;
    string roomName;
    bool isPlaced = false;

    Vector2 roomSize;
    Vector2 unitSize;

    Handle handleNorth;
    Handle handleEast;
    Handle handleSouth;
    Handle handleWest;

    TransformBlock blockTransform;
    BlocksTracker blocksTracker;

    int count = 0;

    private void Start()
    {
        areaType = FindObjectOfType<AreaType>();
        blockTransform = GetComponent<TransformBlock>();
        SetHandles();
        blockTransform.MakeBlockEditable(false);
        ShowHandles(false);
        unitSize = new Vector2(1, 1);
        blocksTracker = FindObjectOfType<BlocksTracker>();
        blocksTracker.AddRoomToList(this);
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
    }

    private void ConstructFloor()
    {
        // todo performance if prevoius pos same as new pos dont call this function
        DeleteFloor();
        unitFloorTiles.Clear();
        //Debug.Log("constructing Floor");
        for(int x = 0; x < roomSize.x; x++)
        {
            for (int y = 0; y < roomSize.y; y++)
            {
                Transform newUnit = Instantiate(floorUnit, unitsParent);
                // todo adjust to every scale.
                // set position or local position. first position starts at handlewest pos.x + adjustment.
                float posX = handleWest.transform.position.x + x + 0.5f;
                float posZ = handleNorth.transform.position.z - y - 0.5f;
                newUnit.transform.position = new Vector3(posX, 0.05f, posZ);
                unitFloorTiles.Add(newUnit);
            }
        }
    }

    private void DeleteFloor()
    {
        foreach(Transform unit in unitFloorTiles)
        {
            Destroy(unit.gameObject);
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


}
