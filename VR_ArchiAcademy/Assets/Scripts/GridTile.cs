using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridTile : XRBaseInteractable
{
    [SerializeField] Vector2 gridSize;
    bool isPlaceable;
    Selector selector;
    

    private void Start()
    {
        isPlaceable = true;
        selector = FindObjectOfType<Selector>();
        gridSize *= 0.05f; // todo move scaleVar to other Script
    }

    public Vector3 SnapPosition(Vector3 hitPos)
    {
        // calculate snapping position
        Debug.Log("HitPos " + hitPos);
        float posX = Mathf.Round(hitPos.x/gridSize.x) * gridSize.x;
        float posZ = Mathf.Round(hitPos.z/gridSize.y) * gridSize.y + gridSize.y;
        
        Vector3 snapPos = new Vector3(posX, hitPos.y, posZ);
        Debug.Log("GridPos " + snapPos);
        return snapPos;
    }

    public void OnHovering()
    {
        Debug.Log("On Hovering");
    }
    
}
