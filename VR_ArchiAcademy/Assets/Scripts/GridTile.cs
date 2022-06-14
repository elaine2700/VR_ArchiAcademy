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

    public Vector3 SnapPosition(Vector3 hitPos, bool snapToGrid)
    {
        // calculate snapping position
        float posX;
        float posZ;
        if (snapToGrid)
        {
            posX = Mathf.Round(hitPos.x / gridMinUnit.x) * gridMinUnit.x;
            posZ = Mathf.Round(hitPos.z / gridMinUnit.y) * gridMinUnit.y;
        }
        else
        {
            posX = hitPos.x;
            posZ = hitPos.z;
        }
        
        Vector3 snapPos = new Vector3(posX, hitPos.y, posZ);
        return snapPos;
    }

    public bool OnGrid()
    {
        bool onGrid = false;
        if (isHovered)
        {
            onGrid = true;
        }
        return onGrid;
    }

}
