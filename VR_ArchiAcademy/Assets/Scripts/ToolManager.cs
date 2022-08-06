using UnityEngine;
using UnityEngine.Events;

public class ToolManager : MonoBehaviour
{
    /// <summary>
    /// Select: waiting for a selection. Build: In building mode.
    /// Transform: editing position, rotation, or size.
    /// Edit: Change properties like materials or colors.
    /// Delete: waiting to select and object to delete.
    /// Explore: exploring the world.
    /// </summary>
    public enum ToolSelection { select, build, transform, edit, delete, explore};
    public ToolSelection toolInUse = ToolSelection.select;

    public UnityEvent OnToolSelect;
    public UnityEvent OnToolBuild;
    public UnityEvent OnToolTransform;
    public UnityEvent OnToolEdit;
    public UnityEvent OnToolDelete;
    public UnityEvent OnToolExplore;

    Actions inputActions;
    Scaler scaler;

    private void Awake()
    {
        inputActions = new Actions();
        inputActions.Tools.Swaptools.performed += _ => SwapTools();
        scaler = FindObjectOfType<Scaler>();
    }

    private void OnEnable()
    {
        inputActions.Tools.Enable();
    }

    private void OnDisable()
    {
        inputActions.Tools.Disable();
    }

    /// <summary>
    /// Changes the current tool used in application.
    /// Parameters can be 0 to 4. 0:select, 1:build, 2: Transform, 3:Edit, 4: delete, 5: Explore.
    /// </summary>
    /// <param name="newTool">Number to cast as enum ToolSelection</param>
    public void ChangeTool(int newTool)
    {
        if(scaler.CurrentScaleInverse != 1)
        {
            toolInUse = ToolSelection.select;
            return;
        }
        toolInUse = (ToolSelection)newTool;
        switch (toolInUse)
        {
            case ToolSelection.select:
                OnToolSelect.Invoke();
                break;
            case ToolSelection.transform:
                OnToolTransform.Invoke();
                break;
            case ToolSelection.build:
                OnToolBuild.Invoke();
                break;
            case ToolSelection.edit:
                //OnToolEdit.Invoke();
                break;
            case ToolSelection.explore:
                OnToolExplore.Invoke();
                break;
            case ToolSelection.delete:
                OnToolDelete.Invoke();
                break;
        }
    }

    public void SwapTools()
    {
        int nextToolIndex = (int)toolInUse + 1;
        if(nextToolIndex >= 6)
        {
            nextToolIndex = 0;
        }
        if(nextToolIndex == 3)
        {
            nextToolIndex = 4;
        }
        ChangeTool(nextToolIndex);
    }

}
