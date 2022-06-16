using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Block))]
public class PreviewBlock : MonoBehaviour
{
    [SerializeField] Vector3 adjustments;
    ThemeSettings themeSettings;
    Scaler scaler;
    Collider blockCollider;
    ChangeMaterial materialChanger;

    public bool positionOk = false;
    List<Material> blockMaterials = new List<Material>();

    [SerializeField] LayerMask layerMasks;
    [SerializeField] GameObject wallMeshRef;
    public Vector3 blockSize; // todo set block size by collider size

    private void Awake()
    {
        scaler = FindObjectOfType<Scaler>();
        blockCollider = GetComponent<Collider>();
        blockSize = blockCollider.bounds.size;
        Debug.Log(blockSize);
    }
    
    private void Start()
    {
        themeSettings = FindObjectOfType<ThemeSettings>();
        RememberBlockMaterials();
        //meshRenderer = GetComponentInChildren<MeshRenderer>();
        ChangeMaterial(themeSettings.previewBlockMaterial);
        adjustments *= scaler.modelScale;
        materialChanger = GetComponent<ChangeMaterial>();
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
            ChangeMaterial(themeSettings.previewBlockMaterial);
        }
        else
        {
            ChangeMaterial(themeSettings.overlapBlockMaterial);
        }
    }

    public void CheckPosition(Vector3 placePosition)
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        //Vector3 centerPiece = new Vector3(placePosition.x, wallMeshRef.transform.position.y, placePosition.z);
        Vector3 centerPiece = new Vector3(transform.position.x, transform.position.y + (blockSize.y/2), transform.position.z);
        Vector3 halfSizeOverlapBox = (blockSize / 2);
        //Check when there is a new collider coming into contact with the box
        for (int i = 0; i < 2; i++)
        {
            Collider[] hitColliders = Physics.OverlapBox(centerPiece, halfSizeOverlapBox, Quaternion.identity, layerMasks);
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
            ChangeMaterial(themeSettings.hoveredBlockMaterial);
        else
            ReverseOriginalMaterials(blockMaterials);
            //meshRenderer.material = GetComponent<Block>().blockMaterial;
    }

    public void SelectingBlock(bool selected)
    {
        Debug.Log("Selecting block");
        if (selected)
            ChangeMaterial(themeSettings.selectedBlockMaterial);
        else
            ReverseOriginalMaterials(blockMaterials);
            //meshRenderer.material = GetComponent<Block>().blockMaterial;
    }

    private void ChangeMaterial(Material newMaterial)
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.material = newMaterial;
        }
    }

    private void ReverseOriginalMaterials(List<Material> originalMaterials)
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        int index = 0;
        foreach ( MeshRenderer renderer in meshRenderers)
        {
            renderer.material = originalMaterials[index];
            index++;
        }
    }

    private void RememberBlockMaterials()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            blockMaterials.Add(renderer.material);
        }
    }

}
