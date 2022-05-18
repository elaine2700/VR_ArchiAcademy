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
        // todo Change function to OnHover (XR Toolkit)
        // todo PreviewModel
        // todo Instantiate two Models to selector, one with transparent color. Hidden until it hovers something that snaps to every tile it hovers.


        // todo Change function to OnSelected (XR Toolkit)
        // send block to grid tile position
        if (other.GetComponent<Block>())
        {
            other.GetComponent<Block>().GetGridPos(transform.position, true);
            if(Input.GetMouseButton(0)) // Button 0 is primary button
            PlaceOnTile(other.GetComponent<Block>());

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Block>())
        {
            other.GetComponent<Block>().GetGridPos(transform.position, false);
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
