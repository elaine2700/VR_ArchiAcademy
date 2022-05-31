using UnityEngine;

public class PreviewBlock : MonoBehaviour
{
    [SerializeField] Vector3 adjustments;

    ThemeSettings themeSettings;

    public bool positionOk = false;
    Block block;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    [SerializeField] LayerMask wallLayerMask;
    [SerializeField] GameObject wallMeshRef; 

    private void Start()
    {
        themeSettings = FindObjectOfType<ThemeSettings>();
        block = GetComponentInParent<Block>();
        Show(false);
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = themeSettings.previewBlockMaterial;
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshFilter.mesh = block.GetComponentInChildren<MeshFilter>().mesh;
        adjustments *= block.scaleVar;
    }

    private void Update()
    {
        Debug.Log("Position ok? " + positionOk);
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }

    public void AdjustPosition(Vector3 position)
    {
        // preview in possible new position
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
        
        Vector3 centerPiece = new Vector3(placePosition.x, wallMeshRef.transform.position.y, placePosition.z);
        Vector3 sizeOverlapBox = wallMeshRef.transform.localScale / 2 * 0.05f;
        //Check when there is a new collider coming into contact with the box
        for (int i = 0; i < 10; i++)
        {
            Collider[] hitColliders = Physics.OverlapBox(centerPiece, sizeOverlapBox, Quaternion.identity, wallLayerMask);
            bool isPlaceable = hitColliders.Length == 0;
            ShowOverlap(isPlaceable);
            if (isPlaceable)
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 centerPiece = new Vector3(transform.position.x, wallMeshRef.transform.position.y, transform.position.z);
        Gizmos.DrawWireCube(centerPiece, new Vector3(1,3,1) * 0.05f);
    }
}
