using UnityEngine;
//using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    public enum blockState {selected, placed};
    public blockState currentBlockState = blockState.selected;

    [SerializeField] bool isPlaced = false;
    [Tooltip("1 = Floor, 2 = Wall, 3 = Furniture")]
    [SerializeField] int typeOfBlock;
    public Collider blockMaincollider;
    public bool snap;
    public bool isEditing = false;
    //public bool edit = false;
    //public Material blockMaterial;

    Selector selector;
    GridLayers gridLayers;
    PreviewBlock previewBlock;
    Scaler scaler;
    BlockTransform blockTransform;
    Rotate rotator;

    private void Start()
    {
        blockTransform = FindObjectOfType<BlockTransform>();
        previewBlock = GetComponentInChildren<PreviewBlock>();
        gridLayers = FindObjectOfType<GridLayers>();
        selector = FindObjectOfType<Selector>();
        scaler = FindObjectOfType<Scaler>();
        transform.localScale *= scaler.modelScale;
        //blockMaterial = GetComponentInChildren<MeshRenderer>().material;
        rotator = GetComponent<Rotate>();
    }

    private void Update()
    {
        if (currentBlockState == blockState.selected)
        {
            if (rotator == null)
                return;
            rotator.canRotate = true;
        }
    }

    public void PlaceOnGrid(Vector3 newPos)
    {
        if (previewBlock.positionOk)
        {
            //Debug.Log("Placing");
            isPlaced = true;
            transform.parent = gridLayers.ParentToCurrentLayer(typeOfBlock).transform;
            // todo test without this line.
            // transform.rotation = previewBlock.transform.rotation;
            blockMaincollider.enabled = true;
            selector.DeselectBlock();
            currentBlockState = blockState.placed;
            previewBlock.ReverseOriginalMaterials();
            if(rotator!= null)
                rotator.canRotate = false;
            // only if Block is Floor
            
        }
        else
        {
            //Debug.Log("Place not available");
        }
    }

    public void PreviewPosGrid(Vector3 hitPosition)
    {
        previewBlock.AdjustPosition(hitPosition);
        previewBlock.CheckPosition(hitPosition);
    }

    public void MoveBlock()
    {
        // function called from XR event.
        // Dont call this on floor.
        Debug.Log("Moving block");
        /*if (currentBlockState == blockState.selected)
            currentBlockState = blockState.placed;
        else if (currentBlockState == blockState.placed)
            currentBlockState = blockState.selected;
        */
        EditBlock();
        if (blockTransform.editPosition && currentBlockState == blockState.selected)
        {
            selector.ChooseBlock(this, true);
        }
    }

    public void EditBlock()
    {
        currentBlockState = blockState.selected;
    }

    public void SetStateToPlace()
    {
        currentBlockState = blockState.placed;
    }

}
