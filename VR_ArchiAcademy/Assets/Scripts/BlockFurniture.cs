using UnityEngine;

public class BlockFurniture : MonoBehaviour
{
    GridLayers gridLayers;
    BlocksTracker blocksTracker;
    PreviewBlock previewBlock;
    OverlapFinder overlap;

    private void Awake()
    {
        previewBlock = GetComponent<PreviewBlock>();
        overlap = GetComponentInChildren<OverlapFinder>();
        overlap.UpdateSize(previewBlock.blockSize);
    }
    private void Start()
    {
        

        blocksTracker = FindObjectOfType<BlocksTracker>();
        blocksTracker.AddFurnitureToList(this);

        gridLayers = FindObjectOfType<GridLayers>();
        transform.parent = gridLayers.ParentToCurrentLayer(3).transform;
    }
}
