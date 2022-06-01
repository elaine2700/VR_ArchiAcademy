using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : MonoBehaviour
{
    [SerializeField] Vector3 offsetBlock = new Vector3();
    [SerializeField] Block selectedBlock = null;
    XRRayInteractor rayController;
    bool isHovering = false;
    GridTile gridTile;
    Vector3 hitPosition;

    private void Awake()
    {
        rayController = GetComponentInParent<XRRayInteractor>();
    }

    private void Update()
    {
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
                Vector3 blockPos = gridTile.SnapPosition(rayHitPosition);

                selectedBlock.PreviewPosGrid(blockPos);

            }
        }
    }

    public void PlaceBlock(SelectEnterEventArgs args)
    {
        Debug.Log("Placing Block");
        //Debug.Log(selectedBlock.name);
        if (selectedBlock == null)
        {
            Debug.Log("Null block selection");
            return;
        }

        if (rayController.TryGetHitInfo(out hitPosition, out _, out _, out _))
        {
            Vector3 rayHitPosition = hitPosition;
            Vector3 blockPos = gridTile.SnapPosition(rayHitPosition);
            //selectedBlock.PreviewPosGrid(blockPos);
            selectedBlock.PlaceOnGrid(blockPos);
        }

        //DeselectBlock();

    }

    public void DeselectBlock()
    {
        selectedBlock = null;
    }

    public void HoveringOnGrid(HoverEnterEventArgs args)
    {
        /*if (selectedBlock == null)
            return;*/
        isHovering = true;

        args.interactableObject.transform.TryGetComponent<GridTile>(out gridTile);
    }

    public void NotHovering()
    {
        isHovering = false;
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
        if(chosenBlock.GetComponent<Collider>())
            chosenBlock.GetComponent<Collider>().enabled = false;
    }

}
