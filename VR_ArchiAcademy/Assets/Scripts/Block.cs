using UnityEngine;
//using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    [SerializeField] bool isPlaced = false;
    [Tooltip("1 = Floor, 2 = Wall, 3 = Furniture")]
    [SerializeField] int typeOfBlock;
    public bool snap;
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
        if (!isPlaced)
        {
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
            transform.rotation = previewBlock.transform.rotation;
            GetComponent<Collider>().enabled = true;
            selector.DeselectBlock();
            //GetComponentInChildren<MeshRenderer>().material = blockMaterial;
            rotator.canRotate = false;
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
        isPlaced = !isPlaced;
        if (blockTransform.editPosition)
        {
            selector.ChooseBlock(this, true);
        }
    }


}
