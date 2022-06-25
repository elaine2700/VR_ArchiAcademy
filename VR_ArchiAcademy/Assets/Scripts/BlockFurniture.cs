using UnityEngine;

public class BlockFurniture : MonoBehaviour
{
    GridLayers gridLayers;

    private void Start()
    {
        gridLayers = FindObjectOfType<GridLayers>();
        transform.parent = gridLayers.ParentToCurrentLayer(3).transform;
    }
}
