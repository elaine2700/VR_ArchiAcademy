using UnityEngine;
//using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    bool isPlaced = false;
    public bool IsPlaced {get {return isPlaced;}}
    public BoxCollider blockMainCollider;
    public bool snap;
    public bool isEditing = false;

    Selector selector;
    PreviewBlock previewBlock;
    Scaler scaler;
    TransformBlock blockTransform;
    Rotate rotator;
    ToolManager toolManager;

    private void Awake()
    {
        previewBlock = GetComponent<PreviewBlock>();
        blockMainCollider.enabled = true;
        scaler = FindObjectOfType<Scaler>();
    }

    private void OnEnable()
    {
        scaler.onChangeScale.AddListener(UpdateScale);
    }

    private void OnDisable()
    {
        scaler.onChangeScale.RemoveListener(UpdateScale);
    }

    private void Start()
    {
        toolManager = FindObjectOfType<ToolManager>();
        blockTransform = GetComponent<TransformBlock>();
        selector = FindObjectOfType<Selector>();
        //blockMaterial = GetComponentInChildren<MeshRenderer>().material;
        rotator = GetComponent<Rotate>();
        blockMainCollider.enabled = false;
    }

    private void Update()
    {
        if (toolManager.toolInUse == ToolManager.ToolSelection.build || toolManager.toolInUse == ToolManager.ToolSelection.transform)
        {
            if (rotator == null)
                return;
            if (blockTransform.isEditableRotation)
                rotator.canRotate = true;
            else
                rotator.canRotate = false;
        }
    }

    public bool PlaceOnGrid(Vector3 newPos)
    {
        Debug.Log("Placing block");
        isPlaced = false;
        if (previewBlock.positionOk)
        {
            isPlaced = true;
            blockMainCollider.enabled = true;
            previewBlock.ReverseOriginalMaterials();
            blockTransform.MakeBlockEditable(false);
            
            if (GetComponent<Blockfloor_V2>())
            {
                toolManager.ChangeTool(2);
                EditBlock();
            }
        }
        else
        {
            //Debug.Log("Place not available");
        }
        return isPlaced;
    }

    public void SeeOnGrid(Vector3 hitPosition)
    {
        previewBlock.AdjustPosition(hitPosition);
        previewBlock.CheckPosition(hitPosition);
    }

    private void EditBlock()
    {
        // function called from XR event.
        //if(toolManager.toolInUse == ToolManager.ToolSelection.transform)
        //{
            Debug.Log("editing block");
            GetComponent<TransformBlock>().MakeBlockEditable(true);
            selector.ChooseBlock(this, true);
        //}
    }

    public void InteractWithBlock()
    {
        EditBlock();
        if(toolManager.toolInUse == ToolManager.ToolSelection.delete)
        {
            Delete();
        }
        blockMainCollider.enabled = true;
    }

    public void Delete()
    {

        if (toolManager.toolInUse != ToolManager.ToolSelection.delete)
            return;
        Debug.Log("deleting block");
        //selector.ChooseBlock(this, true);
        Destroy(this.gameObject);
        
        // todo particle systems maybe
        // todo audio
    }

    private void UpdateScale()
    {
        //float newScale = scaler.ModelScale;
        //transform.localScale = new Vector3(newScale, newScale, newScale);
    }

}
