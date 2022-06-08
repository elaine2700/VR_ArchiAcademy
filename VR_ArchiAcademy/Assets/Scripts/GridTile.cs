using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridTile : XRBaseInteractable
{
    public Vector2 gridMinUnit;
    Scaler scaler;

    private void Start()
    {
        scaler = FindObjectOfType<Scaler>();
        gridMinUnit *= scaler.modelScale;
    }

    public Vector3 SnapPosition(Vector3 hitPos)
    {
        // calculate snapping position
        float posX = Mathf.Round(hitPos.x/gridMinUnit.x) * gridMinUnit.x;
        float posZ = Mathf.Round(hitPos.z/gridMinUnit.y) * gridMinUnit.y;
        
        Vector3 snapPos = new Vector3(posX, hitPos.y, posZ);
        return snapPos;
    }
    
}
