using UnityEngine;

public class BlockTransform : MonoBehaviour
{
    public bool editPosition = false;
    public bool editRotation = false;
    public bool editSize = false;

    public void EditPosition()
    {
        editPosition = !editPosition;
    }

    public void EditRotation()
    {
        editRotation = !editRotation;
    }

    public void EditSize()
    {
        editSize = !editSize;
    }
}