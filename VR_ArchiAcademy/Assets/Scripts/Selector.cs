using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : MonoBehaviour
{
    public Vector3 offsetBlock = new Vector3();
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
            return;
        if (rayController.TryGetHitInfo(out hitPosition, out _, out _, out _))
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
        Debug.Log(selectedBlock.name);
        if (selectedBlock == null)
        {
            Debug.Log("Null block selection");
            return;
        }

        // todo test this new Grid Placement
        if (rayController.TryGetHitInfo(out hitPosition, out var hitNormal, out _, out _))
        {
            Vector3 rayHitPosition = hitPosition;
            Vector3 blockPos = gridTile.SnapPosition(rayHitPosition);
            selectedBlock.PreviewPosGrid(blockPos);
            selectedBlock.PlaceOnGrid(blockPos);
             //todo when to deselect block
        }

        //DeselectBlock();

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
        //Debug.Log("hovering");

        args.interactableObject.transform.TryGetComponent<GridTile>(out gridTile);
    }

    public void NotHovering()
    {
        isHovering = false;
        //Debug.Log("not hovering");
    }

    // Gets the Prefab information from BlockButton script.
    public void ChooseBlock(Block chosenBlock)
    {
        if (selectedBlock != null) { return; }
        Vector3 blockPos = transform.position + offsetBlock;
        selectedBlock = Instantiate(chosenBlock, blockPos, Quaternion.identity);
        if(chosenBlock.GetComponent<Collider>())
            chosenBlock.GetComponent<Collider>().enabled = false;
    }

}
