using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridTile : XRBaseInteractable
{
    bool isPlaceable;
    Selector selector;

    private void Start()
    {
        isPlaceable = true;
        selector = FindObjectOfType<Selector>();
    }

    private void PlaceOnTile(Block block)
    {
        if(!isPlaceable) { return; }
        block.PlaceOnGrid(transform.position);
        isPlaceable = false;
    }

    public void OnHovering()
    {
        Debug.Log("On Hovering");
    }
    
}
