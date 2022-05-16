using UnityEngine;

public class GridTile : MonoBehaviour
{
    GridLayers gridLayers;

    bool isPlaceable;
    Selector selector;

    private void Start()
    {
        isPlaceable = true;
        gridLayers = GetComponentInParent<GridLayers>();
        selector = FindObjectOfType<Selector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        // todo Change function to OnSelected (XR Toolkit)

        // todo check active layer
        // todo if layer wall is active and

        // todo if there is a Click Input

        // send place block on grid position
        if (other.GetComponent<Block>())
        {
            if(Input.GetMouseButton(0)) // Button 0 is primary button
            PlaceOnTile(other.GetComponent<Block>());
        }
        
        
        
    }

    private void PlaceOnTile(Block block)
    {
        if(!isPlaceable) { return; }
        selector.SetSelector(false);
        block.PlaceOnGrid(transform.position, gridLayers.ParentToCurrentLayer());
        
        isPlaceable = false;
    }
}
