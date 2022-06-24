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
    StateManager stateManager;

    private void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
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
        if (stateManager.globalState == StateManager.GlobalState.transforming)
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
                Debug.Log("Edit Floor");
                // keep this selected and make editable to modify floor
                stateManager.ChangeState(StateManager.GlobalState.transforming);

            }
            selector.DeselectBlock();
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
        if (blockTransform.isEditablePosition && isPlaced)
        {
            selector.ChooseBlock(this, true);
        }
    }

    public void ConfirmEdition()
    {
        stateManager.ChangeState(StateManager.GlobalState.selecting);
        GetComponent<TransformBlock>().MakeBlockEditable(false);
    }
}
