using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Material previewMaterial;

    bool isPreview = false;
    bool isPlaced = false;
    bool touchingGrid = false;
    Selector selector;
    Vector3 gridPos;

    private void Start()
    {
        gridPos = new Vector3();
        selector = FindObjectOfType<Selector>();
        transform.localScale *= 0.05f; // todo set 0.05 as variable of scale of grid
    }

    private void Update()
    {
        // if not placed
        if (isPlaced)
        {
            return;
        }

        if (isPreview && touchingGrid)
        {
            GetComponentInChildren<MeshRenderer>().enabled = true;
            transform.position = gridPos;
        }
        else
        {
            // update location to follow selector
            transform.position = selector.transform.position;
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }      
    }

    // It gets Position from GridTile position, and Parents the block with its respective folder
    public void PlaceOnGrid(Vector3 newPos, GameObject newParent)
    {
        isPlaced = true;
        transform.position = gridPos;
        transform.parent = newParent.transform;
        if (isPreview)
        {
            Destroy(gameObject);
        }
    }

    public void SetAsPreview()
    {
        isPreview = true;
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = previewMaterial;
        meshRenderer.enabled = false;
    }

    public void GetGridPos(Vector3 selectedGridPos, bool onGrid)
    {
        touchingGrid = onGrid;
        if(onGrid)
        gridPos = selectedGridPos;
    }

}
