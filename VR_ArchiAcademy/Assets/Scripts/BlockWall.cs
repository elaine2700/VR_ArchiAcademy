using UnityEngine;

public class BlockWall : MonoBehaviour
{
    GridLayers gridLayers;
    BlocksTracker blocksTracker;

    private void Start()
    {
        blocksTracker = FindObjectOfType<BlocksTracker>();
        blocksTracker.AddWallToList(this);

        gridLayers = FindObjectOfType<GridLayers>();
        transform.parent = gridLayers.ParentToCurrentLayer(2).transform;
    }

    
}
