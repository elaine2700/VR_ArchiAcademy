using UnityEngine;

public class TransformBlock : MonoBehaviour
{
    public bool isEditing = false;

    [SerializeField] bool canMove = false;
    [SerializeField] bool canRotate = false;
    [SerializeField] bool canChangeSize = false;

    ToolManager toolManager;

    [SerializeField] bool editPosition = false;
    public bool isEditablePosition { get { return editPosition; } }
    [SerializeField] bool editRotation = false;
    public bool isEditableRotation { get { return editRotation; } }
    [SerializeField] bool editSize = false;
    public bool isEditableSize { get { return editSize; } }

    private void Awake()
    {
        toolManager = FindObjectOfType<ToolManager>();
    }

    private void OnEnable()
    {
        toolManager.OnToolSelect.AddListener(MakeBlockNonEditable);
    }

    private void OnDisable()
    {
        toolManager.OnToolSelect.RemoveListener(MakeBlockNonEditable);
    }

    public void MakeBlockEditable(bool isEditable)
    {
        //Debug.Log($"Size is Editable: { isEditableSize}");
        isEditing = isEditable;
        if (canMove)
            editPosition = isEditing;
        if (canRotate)
            editRotation = isEditing;
        if (canChangeSize)
            editSize = isEditing;
    }

    public void MakeBlockNonEditable()
    {
        MakeBlockEditable(false);
    }

}
