using UnityEngine;
//using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    [SerializeField] bool isPlaced = false;
    public Collider blockMaincollider;
    public bool snap;
    public bool isEditing = false;
    //public bool edit = false;
    //public Material blockMaterial;

    Selector selector;
    PreviewBlock previewBlock;
    Scaler scaler;
    TransformBlock blockTransform;
    Rotate rotator;
    ToolManager toolManager;

    private void Start()
    {
        toolManager = FindObjectOfType<ToolManager>();
        blockTransform = GetComponent<TransformBlock>();
        previewBlock = GetComponentInChildren<PreviewBlock>();
        selector = FindObjectOfType<Selector>();
        scaler = FindObjectOfType<Scaler>();
        transform.localScale *= scaler.modelScale;
        //blockMaterial = GetComponentInChildren<MeshRenderer>().material;
        rotator = GetComponent<Rotate>();
    }

    private void Update()
    {
        if (toolManager.toolInUse == ToolManager.ToolSelection.edit || toolManager.toolInUse == ToolManager.ToolSelection.build)
        {
            if (rotator == null)
                return;
            rotator.canRotate = true;
        }
    }

    public void PlaceOnGrid(Vector3 newPos)
    {
        Debug.Log("Placing block");
        if (previewBlock.positionOk)
        {
            isPlaced = true;
            // todo test without this line.
            // transform.rotation = previewBlock.transform.rotation;
            blockMaincollider.enabled = true;
            previewBlock.ReverseOriginalMaterials();
            if(rotator!= null)
                rotator.canRotate = false;
            
            if (GetComponent<Blockfloor_V2>())
            {
                toolManager.ChangeTool(2);
                EditBlock();
            }
            else
            {
                selector.DeselectBlock();
            }
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

    public void EditBlock()
    {
        // function called from XR event.
        Debug.Log("editing block");
        GetComponent<TransformBlock>().MakeBlockEditable(true);
        selector.ChooseBlock(this, true);
        if (blockTransform.isEditableSize)
        {
            Debug.Log("Floor is editable");
        }
    }

}
