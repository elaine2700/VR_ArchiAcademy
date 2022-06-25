using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : MonoBehaviour
{
    // this class selects objects to build and edit and wait.

    [SerializeField] Vector3 offsetBlock = new Vector3();
    [SerializeField] GridTile gridTile;

    public Block blockRefToPlace = null;
    public Block selectedBlock = null;
    //bool blockIsInScene = false;
    bool isHovering = false;
    bool isBuilding = false;
    bool isEditing = false;
    
    Vector3 hitPosition;

    ToolManager toolManager;
    XRRayInteractor rayController;

    private void Awake()
    {
        toolManager = FindObjectOfType<ToolManager>();
        rayController = GetComponentInParent<XRRayInteractor>();
        gridTile = FindObjectOfType<GridTile>();
    }

    private void OnEnable()
    {
        toolManager.OnToolBuild.AddListener(SelectSomething);
        toolManager.OnToolTransform.AddListener(EditSomething);
    }

    private void OnDisable()
    {
        toolManager.OnToolBuild.RemoveListener(SelectSomething);
        toolManager.OnToolTransform.RemoveListener(EditSomething);
    }

    private void Update()
    {
        isHovering = gridTile.OnGrid();

        if (isBuilding && selectedBlock != null)
        {
            if (!isHovering)
            {
                if (selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
                    selectedBlock.transform.position = transform.position + offsetBlock;
            }
            else if (rayController.TryGetHitInfo(out hitPosition, out var hitNormals, out _, out _))
            {
                Vector3 rayHitPosition = hitPosition;
                Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);
                selectedBlock.PreviewPosGrid(blockPos); 
            }

        }
        else if (isEditing && selectedBlock != null)
        {
            if (selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
            {
                if (!isHovering)
                {
                    if (selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
                        selectedBlock.transform.position = transform.position + offsetBlock;
                }
                else if (rayController.TryGetHitInfo(out hitPosition, out var hitNormals, out _, out _))
                {
                    Vector3 rayHitPosition = hitPosition;
                    Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);
                    selectedBlock.PreviewPosGrid(blockPos);
                }
            }
        }
    }

    private void SelectSomething()
    {
        selectedBlock = null;
        isBuilding = true;
        isEditing = false;
    }

    private void EditSomething()
    {
        blockRefToPlace = null;
        isBuilding = false;
        isEditing = true;
    }

    // Called from XR Event
    public void PlaceBlock(SelectEnterEventArgs args)
    {
        Debug.Log("Selector placing block");
        //blockIsInScene = true;
        if (selectedBlock == null)
        {
            return;
        }
        if (rayController.TryGetHitInfo(out hitPosition, out _, out _, out _))
        {
            Vector3 rayHitPosition = hitPosition;
            Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);
            selectedBlock.PlaceOnGrid(blockPos);
        }
    }

    public void DeselectBlock()
    { 
        //toolManager.ChangeTool(0);
        ForgetBlock();
    }

    public void ForgetBlock()
    {
        selectedBlock = null;
    }

    // Gets the Prefab information from BlockButton script.
    public void ChooseBlock(Block chosenBlock, bool isInScene)
    {
        if (toolManager.toolInUse != ToolManager.ToolSelection.build)
            return;

        if (isInScene)
        {
            selectedBlock = chosenBlock;
            blockRefToPlace = null;
        }
        else
        {
            //Vector3 blockPos = transform.position + offsetBlock;
            //selectedBlock = Instantiate(chosenBlock, blockPos, Quaternion.identity);
            blockRefToPlace = chosenBlock; 
            selectedBlock = Instantiate(blockRefToPlace);
        }
        selectedBlock.GetComponent<TransformBlock>().MakeBlockEditable(true);
        if (chosenBlock.blockMaincollider != null && selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
        {
            chosenBlock.blockMaincollider.enabled = false;
        }    
    }

}
