using UnityEngine;
using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    public Material previewMaterial;
    public float scaleVar = 0.05f;

    bool isPlaced = false;
    Selector selector;
    Vector3 gridPos;
    GridLayers gridLayers;
    PreviewBlock previewBlock;

    Actions actions;

    Vector2 rotate;
    float rotationIncrements = 90f;

    private void Awake()
    {
        actions = new Actions();

        actions.Interaction.RotateBlock.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        actions.Interaction.RotateBlock.canceled += cntxt => rotate = Vector2.zero;
    }

    private void Start()
    {
        previewBlock = GetComponentInChildren<PreviewBlock>();
        gridLayers = FindObjectOfType<GridLayers>();
        gridPos = new Vector3();
        selector = FindObjectOfType<Selector>();
        transform.localScale *= scaleVar;
    }

    private void Update()
    {
        if(!isPlaced)
            Rotate();
    }

    //Preview the rotation
    void Rotate()
    {
        //float rotationX = Mathf.Round(rotate.x);
        //float rotationY = Mathf.Round(rotate.y);
        float rotationX = Mathf.Round(rotate.x/rotationIncrements)*rotationIncrements;//todo test
        float rotationY = Mathf.Round(rotate.y/rotationIncrements)*rotationIncrements;// todo test
        Vector3 newRotation = new Vector3(rotationX, 0, rotationY);
        if(newRotation != Vector3.zero)
            previewBlock.transform.rotation = Quaternion.LookRotation(newRotation, Vector3.up);
    }

    public void PlaceOnGrid(Vector3 newPos)
    {
        isPlaced = true;
        transform.position = newPos;
        transform.parent = gridLayers.ParentToCurrentLayer().transform;
        previewBlock.Show(false);
        transform.rotation = previewBlock.transform.rotation;
    }

    public void PreviewPos(GridTile gridTile)
    {
        previewBlock.Show(gridTile);
        if (gridTile != null)
            previewBlock.AdjustPosition(gridTile.transform.position);
        transform.position = selector.transform.position;
    }

    public void PreviewPosGrid(Vector3 hitPosition)
    {
        previewBlock.Show(true);
        transform.position = hitPosition;
    }

    private void OnEnable()
    {
        actions.Interaction.Enable();
    }

    private void OnDisable()
    {
        actions.Interaction.Disable();
    }

}
