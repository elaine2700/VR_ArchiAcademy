using UnityEngine;

public class PreviewBlock : MonoBehaviour
{
    [SerializeField] Vector3 adjustments;
    Block block;
    MeshRenderer meshRenderer;

    private void Start()
    {
        block = GetComponentInParent<Block>();
        Show(false);
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = block.previewMaterial;
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
