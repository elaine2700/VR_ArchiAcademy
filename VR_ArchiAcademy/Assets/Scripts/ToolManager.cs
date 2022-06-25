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

    /// <summary>
    /// Changes the current tool used in application. Parameters can be 0 to 4. 0:select, 1:build, 2: Edit, 3:explore, 4: delete.
    /// </summary>
    /// <param name="newTool">Number to cast as enum ToolSelection</param>
    public void ChangeTool(int newTool)
    {
        toolInUse = (ToolSelection)newTool;
        switch (toolInUse)
        {
            case ToolSelection.select:
                OnToolSelect.Invoke();
                break;
            case ToolSelection.build:
                OnToolBuild.Invoke();
                break;
            case ToolSelection.edit:
                OnToolEdit.Invoke();
                break;
            case ToolSelection.explore:
                OnToolExplore.Invoke();
                // todo set all block to not editable
                break;
            case ToolSelection.delete:
                OnToolDelete.Invoke();
                break;
        }
    }


}
