using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : MonoBehaviour
{
    [SerializeField] Vector3 offsetBlock = new Vector3();
    public Block selectedBlock = null;
    XRRayInteractor rayController;
    bool isHovering = false;
    [SerializeField] GridTile gridTile;
    Vector3 hitPosition;

    private void Awake()
    {
        rayController = GetComponentInParent<XRRayInteractor>();
        gridTile = FindObjectOfType<GridTile>();
    }

    private void Update()
    {
        if(gridTile != null)
        {
            isHovering = gridTile.OnGrid();
        }
        
        if (selectedBlock == null)
            return;
        if (!isHovering)
        {
            if(selectedBlock != null)
            {
                selectedBlock.transform.position = transform.position + offsetBlock ;
            }
            return;
        }    
        if (rayController.TryGetHitInfo(out hitPosition, out var hitNormals, out _, out _))
        {
            if (hitPosition != null)
            {
                Vector3 rayHitPosition = hitPosition;
                Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);

                selectedBlock.PreviewPosGrid(blockPos);

            }
        }
    }

    public void PlaceBlock(SelectEnterEventArgs args)
    {
        //Debug.Log("Placing Block");
        //Debug.Log(selectedBlock.name);
        if (selectedBlock == null)
        {
            //Debug.Log("Null block selection");
            return;
        }

        if (rayController.TryGetHitInfo(out hitPosition, out _, out _, out _))
        {
            Vector3 rayHitPosition = hitPosition;
            Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);
            //selectedBlock.PreviewPosGrid(blockPos);
            selectedBlock.PlaceOnGrid(blockPos);
        }
    }

    public void DeselectBlock()
    {
        selectedBlock = null;
    }

    // Gets the Prefab information from BlockButton script.
    // or from Block.EditBlock()
    public void ChooseBlock(Block chosenBlock, bool isInScene)
    {
        if (selectedBlock != null) { return; }
        Vector3 blockPos = transform.position + offsetBlock;
        if (isInScene)
        {
            selectedBlock = chosenBlock;
        }
        else
        {
            selectedBlock = Instantiate(chosenBlock, blockPos, Quaternion.identity);
        }
        if(chosenBlock.blockMaincollider != null)
        {
            //Debug.Log("Disabling collider");
            chosenBlock.blockMaincollider.enabled = false;
        }
            
    }

}
