using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridTile : XRBaseInteractable
{
    [SerializeField] Vector2 gridSize;
    Selector selector;
    

    private void Start()
    {
        selector = FindObjectOfType<Selector>();
        gridSize *= 0.05f; // todo move scaleVar to other Script
    }

    public Vector3 SnapPosition(Vector3 hitPos)
    {
        // calculate snapping position
        float posX = Mathf.Round(hitPos.x/gridSize.x) * gridSize.x;
        float posZ = Mathf.Round(hitPos.z/gridSize.y) * gridSize.y + gridSize.y;
        
        Vector3 snapPos = new Vector3(posX, hitPos.y, posZ);
        return snapPos;
    }
    
}
