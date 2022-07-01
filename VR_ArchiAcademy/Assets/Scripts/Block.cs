using UnityEngine;
//using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    bool isPlaced = false;
    public bool IsPlaced {get {return isPlaced;}}
    public Collider blockMaincollider;
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
        blockMaincollider.enabled = true;
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
        blockMaincollider.enabled = false;
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
            blockTransform.MakeBlockEditable(false);
            
            if (GetComponent<Blockfloor_V2>())
            {
                toolManager.ChangeTool(2);
                EditBlock();
            }
            else
            {
                //selector.DeselectBlock();
            }
        }
        else
        {
            //Debug.Log("Place not available");
        }
    }

    public void SeeOnGrid(Vector3 hitPosition)
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
    }

    public void Delete()
    {
        Destroy(gameObject);
        // todo particle systems maybe
        // todo audio
    }

    private void UpdateScale()
    {
        transform.localScale *= scaler.ModelScale;
    }

}
