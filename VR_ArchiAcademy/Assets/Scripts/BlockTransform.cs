using UnityEngine;

public class BlockTransform : MonoBehaviour
{
    public bool editPosition = true;
    public bool editRotation = true;
    public bool editSize = true;

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
