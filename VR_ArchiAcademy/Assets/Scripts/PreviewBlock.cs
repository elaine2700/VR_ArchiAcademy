using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Block))]
public class PreviewBlock : MonoBehaviour
{
    [SerializeField] Vector3 adjustments;
    [SerializeField] ThemeSettings themeSettings;
    [SerializeField] Material originalMaterial;
    [SerializeField] LayerMask findOverlapMask;
    public Transform meshesParent;

    Scaler scaler;
    BoxCollider blockCollider;

    public bool positionOk = false;
    public List<Renderer> meshesWithMaterials = new List<Renderer>();
    public Vector3 blockSize = new Vector3(); // Set block size by collider size.
    Vector3 blockCenter;

    private void Awake()
    {
        scaler = FindObjectOfType<Scaler>();
        blockCollider = GetComponent<Block>().blockMainCollider;
    }

    private void OnEnable()
    {
        blockSize = blockCollider.size;
        blockCenter = blockCollider.center;
        scaler.onChangeScale.AddListener(UpdateScale);
    }

    private void OnDisable()
    {
        scaler.onChangeScale.RemoveListener(UpdateScale);
    }

    private void Start()
    {
        ChangingMaterial(themeSettings.previewBlockMaterial, meshesWithMaterials);
        UpdateScale();
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
            ChangingMaterial(themeSettings.previewBlockMaterial, meshesWithMaterials);
        }
        else
        {
            ChangingMaterial(themeSettings.overlapBlockMaterial, meshesWithMaterials);
        }
    }

    public void ChangingMaterial(Material newMaterial, List<Renderer> currentRenderers)
    {
        if(currentRenderers.Count >= 1)
        {
            foreach (MeshRenderer renderer in currentRenderers)
            {
                renderer.material = newMaterial;
            }
        }
    }

    public void ChangingMaterial(Material newMaterial, Renderer currentRenderer)
    {
        currentRenderer.material = newMaterial;
    }

    public void CheckPosition(Vector3 placePosition)
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        //Vector3 centerPiece = new Vector3(placePosition.x, wallMeshRef.transform.position.y, placePosition.z);
        //Vector3 objectCenter = new Vector3(1,1,1);
        Vector3 centerPiece = new Vector3(transform.position.x + blockCenter.x,
            transform.position.y + (blockSize.y / 2),
            transform.position.z + blockCenter.z);

        Vector3 halfSizeOverlapBox = (blockSize / 2);
        //Check when there is a new collider coming into contact with the box
        for (int i = 0; i < 2; i++)
        {
            Collider[] hitColliders = Physics.OverlapBox(centerPiece, halfSizeOverlapBox, transform.rotation, findOverlapMask);
            bool isPlaceable = hitColliders.Length == 0;
            ShowOverlap(isPlaceable);
            if (isPlaceable)
                break;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 centerPiece = new Vector3(transform.position.x + blockCenter.x,
            transform.position.y + (blockSize.y/2),
            transform.position.z + blockCenter.z);
        Gizmos.DrawWireCube(centerPiece, blockSize);
    }

    // to use on blockFloor
    public void SetBlockSize(Vector3 updatedSize)
    {
        blockSize.x = updatedSize.x;
        blockSize.z = updatedSize.z;
    }

    public void HoveredBlock(bool hovered)
    {
        // Called when the Block is hovered by selector
        if (hovered)
            ChangingMaterial(themeSettings.hoveredBlockMaterial, meshesWithMaterials);
        else
            ReverseOriginalMaterials();
            //meshRenderer.material = GetComponent<Block>().blockMaterial;
    }

    public void SelectingBlock(bool selected)
    {
        if (selected)
            ChangingMaterial(themeSettings.selectedBlockMaterial, meshesWithMaterials);
        else
            ReverseOriginalMaterials();
    }

    public void ReverseOriginalMaterials()
    {
        if(meshesWithMaterials.Count >= 1)
        {
            foreach (MeshRenderer renderer in meshesWithMaterials)
            {
                renderer.material = originalMaterial;
            }
        }
    }

    private void UpdateScale()
    {
        adjustments *= scaler.ModelScale;
    }
}
