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
        Rotate();
    }

    //Preview the rotation
    void Rotate()
    {
        Vector3 newRotate = new Vector3(rotate.x, 0, rotate.y);
        if(newRotate != Vector3.zero)
            previewBlock.transform.rotation = Quaternion.LookRotation(newRotate, Vector3.up);
    }

    public void PlaceOnGrid(Vector3 newPos)
    {
        isPlaced = true;
        transform.position = gridPos;
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

    private void OnEnable()
    {
        actions.Interaction.Enable();
    }

    private void OnDisable()
    {
        actions.Interaction.Disable();
    }

}
