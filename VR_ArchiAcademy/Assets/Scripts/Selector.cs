using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : MonoBehaviour
{
    public Vector3 offsetBlock = new Vector3();
    Block selectedBlock = null;
    XRRayInteractor rayController;
    bool isHovering = false;
    GridTile gridTile;

    private void Awake()
    {
        rayController = GetComponentInParent<XRRayInteractor>();
    }

    public void PlaceBlock(SelectEnterEventArgs args)
    {
        Debug.Log("Placing Block");
        Debug.Log(selectedBlock.name);
        if (selectedBlock == null)
        {
            Debug.Log("Null block selection");
            return;
        }

        // selects a GridTile and places the selected Block
        /*Debug.Log("placing");
        if (args.interactableObject.GetType() == typeof(GridTile))
        {
            Debug.Log("grid Selected");
            GridTile gridTile = args.interactableObject.transform.GetComponent<GridTile>();
            selectedBlock.PlaceOnGrid(gridTile.transform.position);
            Debug.Log("placed on Grid");
            Debug.Log(gridTile.transform.position);
        }*/

        // todo test this new Grid Placement
        if (rayController.TryGetHitInfo(out var hitPosition, out var hitNormal, out _, out _))
        {
            Vector3 rayHitPosition = hitPosition;
            Vector3 blockPos = gridTile.SnapPosition(rayHitPosition);
            selectedBlock.PreviewPosGrid(blockPos);
            selectedBlock.PlaceOnGrid(blockPos);
        }



        DeselectBlock();


    }

    private void Update()
    {
        if (!isHovering)
            return;
        if (rayController.TryGetHitInfo(out var hitPosition, out var hitNormal, out _, out _))
        {
            Vector3 rayHitPosition = hitPosition;
            Vector3 blockPos = gridTile.SnapPosition(rayHitPosition);
            selectedBlock.PreviewPosGrid(blockPos);
            //selectedBlock.PlaceOnGrid(blockPos);
        }
    }

    public void DeselectBlock()
    {
        selectedBlock = null;
    }

    public void HoveringOnGrid(HoverEnterEventArgs args)
    {
        if (!selectedBlock)
            return;
        isHovering = true;
        Debug.Log("hovering");

        args.interactableObject.transform.TryGetComponent<GridTile>(out gridTile);
        
        // todo set block to null not working
        /*if (args.interactableObject.GetType() == typeof(GridTile))
        {
            Debug.Log("hovering");
            GridTile gridTile = args.interactableObject.transform.GetComponent<GridTile>();
            selectedBlock.PreviewPos(gridTile);
        }*/
    }

    // Gets the Prefab information from BlockButton script.
    public void ChooseBlock(Block chosenBlock)
    {
        if (selectedBlock != null) { return; }
        Vector3 blockPos = transform.position + offsetBlock;
        
        selectedBlock = Instantiate(chosenBlock, blockPos, Quaternion.identity);
    }

}
