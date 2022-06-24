using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall : MonoBehaviour
{
    GridLayers gridLayers;

    private void Start()
    {
        gridLayers = FindObjectOfType<GridLayers>();
        transform.parent = gridLayers.ParentToCurrentLayer(2).transform;
    }

    
}
