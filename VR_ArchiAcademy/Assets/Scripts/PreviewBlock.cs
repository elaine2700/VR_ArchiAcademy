using UnityEngine;

public class PreviewBlock : MonoBehaviour
{
    [SerializeField] Vector3 adjustments;
    Block block;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    private void Start()
    {
        block = GetComponentInParent<Block>();
        Show(false);
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = block.previewMaterial;
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshFilter.mesh = block.GetComponentInChildren<MeshFilter>().mesh;
        Debug.Log(meshRenderer.material.name);
        adjustments *= block.scaleVar;
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }

    public void AdjustPosition(Vector3 position)
    {
        transform.position = adjustments + position;
    }
}
