using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridTile : XRBaseInteractable
{
    public Vector2 gridMinUnit;
    Selector selector;
    

    private void Start()
    {
        selector = FindObjectOfType<Selector>();
        gridMinUnit *= 0.05f; // todo move scaleVar to other Script
    }

    public Vector3 SnapPosition(Vector3 hitPos)
    {
        // calculate snapping position
        float posX = Mathf.Round(hitPos.x/gridMinUnit.x) * gridMinUnit.x;
        float posZ = Mathf.Round(hitPos.z/gridMinUnit.y) * gridMinUnit.y + gridMinUnit.y;
        
        Vector3 snapPos = new Vector3(posX, hitPos.y, posZ);
        return snapPos;
    }
    
}
