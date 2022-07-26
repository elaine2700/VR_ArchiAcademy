using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Block))]
public class PreviewBlock : MonoBehaviour
{
    [SerializeField] Vector3 adjustments;
    [SerializeField] ThemeSettings themeSettings;
    [SerializeField] Material originalMaterial;
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
        //blockCollider = GetComponent<Block>().blockMainCollider;
        blockCollider = GetComponent<Block>().colliders[0];
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

    public void ShowOverlap(bool okPos)
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
