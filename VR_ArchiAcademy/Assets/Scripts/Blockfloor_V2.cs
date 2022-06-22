using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockfloor_V2 : MonoBehaviour
{
    [SerializeField] Transform floorUnit;
    [SerializeField] Transform unitsParent;

    List<Transform> unitFloorTiles = new List<Transform>();
    
    Vector2 unitSize;

    Handle handleNorth;
    Handle handleEast;
    Handle handleSouth;
    Handle handleWest;

    Vector2 roomSize;

    [SerializeField] bool isEditing = false;

    [SerializeField] List<Handle> handles = new List<Handle>();

    private void Start()
    {
        SetHandles();
        unitSize = new Vector2(1, 1);
    }

    private void Update()
    {
        if (isEditing)
        {
            // update size
            ShowHandles(isEditing);
            CalculateRoomSize();
            ConstructFloor();
        }
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
            ShowHandles(false);
        }
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
        Debug.Log($"width: {width}, depth: {depth}");
        roomSize.x = Mathf.Abs(width);
        roomSize.y = Mathf.Abs(depth);
    }

    public void EditFloor(bool edit)
    {
        isEditing = edit;
    }

    private void ConstructFloor()
    {
        // todo performance if prevoius pos same as new pos dont call this function
        DeleteFloor();
        unitFloorTiles.Clear();
        Debug.Log("constructing Floor");
        for(int x = 0; x < roomSize.x; x++)
        {
            for (int y = 0; y < roomSize.y; y++)
            {
                // todo create unit
                // instantiate unit prefab
                Transform newUnit = Instantiate(floorUnit, unitsParent);
                // set position or local position. first position starts at handlewest pos.x + adjustment
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

}
