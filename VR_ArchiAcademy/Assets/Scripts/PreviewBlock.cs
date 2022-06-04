using UnityEngine;

public class PreviewBlock : MonoBehaviour
{
    [SerializeField] Vector3 adjustments;

    ThemeSettings themeSettings;
    Scaler scaler;

    public bool positionOk = false;
    Block block;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    [SerializeField] LayerMask wallLayerMask;
    [SerializeField] GameObject wallMeshRef;
    [SerializeField] Vector3 blockSize;

    private void Start()
    {
        themeSettings = FindObjectOfType<ThemeSettings>();
        scaler = FindObjectOfType<Scaler>();
        block = GetComponentInParent<Block>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = themeSettings.previewBlockMaterial;
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshFilter.mesh = block.GetComponentInChildren<MeshFilter>().mesh;
        adjustments *= scaler.modelScale;
    }

    private void Update()
    {
        Debug.Log("Position ok? " + positionOk);
    }

    public void AdjustPosition(Vector3 position)
    {
        transform.position = adjustments + position;
    }

    private void ShowOverlap(bool okPos)
    {
        positionOk = okPos;
        if (positionOk)
        {
            meshRenderer.material = themeSettings.previewBlockMaterial;
        }
        else
        {
            meshRenderer.material = themeSettings.overlapBlockMaterial;
        }
    }

    public void CheckPosition(Vector3 placePosition)
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        //Vector3 centerPiece = new Vector3(placePosition.x, wallMeshRef.transform.position.y, placePosition.z);
        Vector3 centerPiece = new Vector3(transform.position.x, transform.position.y + (blockSize.y/2), transform.position.z);
        Vector3 halfSizeOverlapBox = blockSize / 2 * scaler.modelScale;
        //Check when there is a new collider coming into contact with the box
        for (int i = 0; i < 10; i++)
        {
            Collider[] hitColliders = Physics.OverlapBox(centerPiece, halfSizeOverlapBox, Quaternion.identity, wallLayerMask);
            bool isPlaceable = hitColliders.Length == 0;
            ShowOverlap(isPlaceable);
            if (isPlaceable)
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 centerPiece = new Vector3(transform.position.x, transform.position.y + (blockSize.y/2), transform.position.z);
        Gizmos.DrawWireCube(centerPiece, blockSize * scaler.modelScale);
    }

    public void EditBlockPlace()
    {
        // put in edition mode
        // change material to previewMaterial
        // Preview new pos in Grid
        // Set position 
    }
}
