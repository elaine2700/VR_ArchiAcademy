using UnityEngine;
//using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    bool isPlaced = false;
    //public bool edit = false;
    Material blockMaterial;
    
    Selector selector;
    GridLayers gridLayers;
    PreviewBlock previewBlock;
    Scaler scaler;
    BlockTransform blockTransform;

    Actions actions;

    Vector2 rotate;
    float rotationIncrements = 1/90f;

    private void Awake()
    {
        actions = new Actions();

        actions.Interaction.RotateBlock.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        actions.Interaction.RotateBlock.canceled += cntxt => rotate = Vector2.zero;
    }

    private void Start()
    {
        blockTransform = FindObjectOfType<BlockTransform>();
        previewBlock = GetComponentInChildren<PreviewBlock>();
        gridLayers = FindObjectOfType<GridLayers>();
        selector = FindObjectOfType<Selector>();
        scaler = FindObjectOfType<Scaler>();
        transform.localScale *= scaler.modelScale;
        blockMaterial = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Update()
    {
        if (!isPlaced)
        {
            Rotate();
        }
    }

    //Preview the rotation
    void Rotate()
    {
        //float rotationX = Mathf.Round(rotate.x); // todo test
        //float rotationY = Mathf.Round(rotate.y); // todo test
        float rotationX = Mathf.Round(rotate.x/rotationIncrements)*rotationIncrements;//todo test
        float rotationY = Mathf.Round(rotate.y/rotationIncrements)*rotationIncrements;// todo test
        Vector3 newRotation = new Vector3(rotationX, 0, rotationY);
        if(newRotation != Vector3.zero)
            previewBlock.transform.rotation = Quaternion.LookRotation(newRotation, Vector3.up);
    }

    public void PlaceOnGrid(Vector3 newPos)
    {
        if (previewBlock.positionOk)
        {
            //Debug.Log("Placing");
            isPlaced = true;
            //edit = false;
            //transform.position = newPos;
            transform.parent = gridLayers.ParentToCurrentLayer().transform;
            //previewBlock.Show(false);
            transform.rotation = previewBlock.transform.rotation;
            GetComponent<Collider>().enabled = true;
            selector.DeselectBlock();
            GetComponentInChildren<MeshRenderer>().material = blockMaterial;
        }
        else
        {
            //Debug.Log("Place not available");
        }
    }

    public void PreviewPosGrid(Vector3 hitPosition)
    {
        //previewBlock.Show(true);
        //transform.position = selector.transform.position;
        previewBlock.AdjustPosition(hitPosition);
        previewBlock.CheckPosition(hitPosition);
    }

    private void OnEnable()
    {
        actions.Interaction.Enable();
    }

    private void OnDisable()
    {
        actions.Interaction.Disable();
    }

    public void MoveBlock()
    {
        // todo test
        //Debug.Log("Editing Block Place");
        // function called from XR event.
        
        isPlaced = !isPlaced;
        if (blockTransform.editPosition)
        {
            selector.ChooseBlock(this, true);
        }

        // todo return to place if selected another without placing this one
    }

    


}
