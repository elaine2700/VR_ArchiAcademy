using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayers : MonoBehaviour
{
    [SerializeField] GameObject folderFloor;
    [SerializeField] GameObject folderWalls;
    [SerializeField] GameObject folderFurniture;

    // todo if layer wall is active 

    public GameObject ParentToCurrentLayer()
    {
        // function to organize blocks on a empty gameObject
        return folderWalls;
    }
}
