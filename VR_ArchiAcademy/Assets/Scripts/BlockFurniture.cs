using UnityEngine;

public class BlockFurniture : MonoBehaviour
{
    GridLayers gridLayers;
    BlocksTracker blocksTracker;

    private void Start()
    {
        blocksTracker = FindObjectOfType<BlocksTracker>();
        blocksTracker.AddFurnitureToList(this);

        gridLayers = FindObjectOfType<GridLayers>();
        transform.parent = gridLayers.ParentToCurrentLayer(3).transform;
    }
}
