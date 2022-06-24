using UnityEngine;

public class TransformBlock : MonoBehaviour
{
    [SerializeField] bool canMove = false;
    [SerializeField] bool canRotate = false;
    [SerializeField] bool canChangeSize = false;
    bool placedOnce = false; // only for Blocks with Floor component

    public bool isEditing = false;

    bool editPosition = false;
    public bool isEditablePosition { get { return editPosition; } }
    bool editRotation = false;
    public bool isEditableRotation { get { return editRotation; } }
    bool editSize = false;
    public bool isEditableSize { get { return editSize; } }


    public void MakeBlockEditable(bool isEditable)
    {
        Debug.Log($"Position is Editable: { isEditablePosition}");
        isEditing = isEditable;
        if (canMove)
            editPosition = isEditing;
        if (canRotate)
            editRotation = isEditing;
        if (canChangeSize)
            editSize = isEditing;
        
    }
}
