using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : MonoBehaviour
{
    public Vector3 offsetBlock = new Vector3();
    Block selectedBlock = null;

    public void PlaceBlock(SelectEnterEventArgs args)
    {
        
        if (!selectedBlock)
            return;
        // selects a GridTile and places the selected Block
        Debug.Log("placing");
        if (args.interactableObject.GetType() == typeof(GridTile))
        {
            Debug.Log("placing2");
            GridTile gridTile = args.interactableObject.transform.GetComponent<GridTile>();
            selectedBlock.PlaceOnGrid(gridTile.transform.position);
            Debug.Log("placing3");
        }
        DeselectBlock();
    }

    public void DeselectBlock()
    {
        selectedBlock = null;
    }

    public void HoveringOnGrid(HoverEnterEventArgs args)
    {
        if (!selectedBlock)
            return;
        // todo set block to null not working
        if(args.interactableObject.GetType() == typeof(GridTile))
        {
            Debug.Log("hovering");
            GridTile gridTile = args.interactableObject.transform.GetComponent<GridTile>();
            selectedBlock.PreviewPos(gridTile);
        }
    }

    // Gets the Prefab information from BlockButton script.
    public void ChooseBlock(Block chosenBlock)
    {
        if (selectedBlock != null) { return; }
        Vector3 blockPos = transform.position + offsetBlock;
        
        selectedBlock = Instantiate(chosenBlock, blockPos, Quaternion.identity);
    }

}
