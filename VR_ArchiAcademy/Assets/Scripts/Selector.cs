using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : MonoBehaviour
{
    // this class selects objects to build and transform.

    [SerializeField] Vector3 offsetBlock = new Vector3();
    [SerializeField] GridTile gridTile;
    [SerializeField] LayerMask onSelectedMask;
    [SerializeField] LayerMask normalStateMask;
    [SerializeField] LayerMask uiMask;

    public Block blockToSpawn = null; 
    public Block selectedBlock = null;

    bool isHovering = false;
    Vector3 hitPosition;

    ToolManager toolManager;
    XRRayInteractor rayController;

    private void Awake()
    {
        toolManager = GetComponent<ToolManager>();
        rayController = GetComponentInParent<XRRayInteractor>();
        gridTile = FindObjectOfType<GridTile>();
    }

    private void OnEnable()
    {
        toolManager.OnToolSelect.AddListener(EnterSelectMode);
        toolManager.OnToolBuild.AddListener(EnterBuildMode);
        toolManager.OnToolTransform.AddListener(EnterTransformMode);
        toolManager.OnToolDelete.AddListener(EnterDeleteMode);
        toolManager.OnToolEdit.AddListener(EnterEditMode);

        rayController.selectExited.AddListener(PlaceBlock);
    }

    private void OnDisable()
    {
        toolManager.OnToolSelect.RemoveListener(EnterSelectMode);
        toolManager.OnToolBuild.RemoveListener(EnterBuildMode);
        toolManager.OnToolTransform.RemoveListener(EnterTransformMode);
        toolManager.OnToolDelete.RemoveListener(EnterDeleteMode);
        toolManager.OnToolEdit.RemoveListener(EnterEditMode);

        rayController.selectExited.RemoveListener(PlaceBlock);
        rayController.selectEntered.RemoveAllListeners();
    }

    private void Update()
    {
        isHovering = gridTile.OnGrid();

        if (toolManager.toolInUse == ToolManager.ToolSelection.build)
            Building();   
        else if (toolManager.toolInUse == ToolManager.ToolSelection.transform)
            Transforming();
        else
            EnterSelectMode();
    }

    private void EnterBuildMode()
    {
        Debug.Log("Entering Build Mode");
    }

    private void EnterTransformMode()
    {
        Debug.Log("Entering Transform Mode");
        blockToSpawn = null;
        if (selectedBlock != null)
        {
            if (!selectedBlock.IsPlaced)
            {
                Destroy(selectedBlock.gameObject);
                selectedBlock = null; 
            }
        }
        rayController.raycastMask = normalStateMask;
    }

    private void EnterEditMode()
    {
        rayController.raycastMask = normalStateMask;
    }

    private void EnterSelectMode()
    {
        rayController.raycastMask = normalStateMask;
        selectedBlock = null;
    }

    private void EnterDeleteMode()
    {
        selectedBlock = null;
        rayController.raycastMask = normalStateMask;
    }

    private void Building()
    {
        if(selectedBlock != null)
        {
            MoveBlock();
        }
    }

    private void Transforming()
    {
        if (selectedBlock != null)
        {
            if (selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
            {
                MoveBlock();
            }
        }
        else
        {
            rayController.raycastMask = normalStateMask;
        }
    }

    private void MoveBlock()
    {
        // Move Selected Object around
        //Debug.Log("Moving block on grid");
        if (!isHovering)
        {
            // see on controller
            selectedBlock.GetComponent<PreviewBlock>().meshesParent.gameObject.SetActive(false);
            //selectedBlock.transform.position = transform.position + offsetBlock;
            rayController.raycastMask = uiMask;
        }
        else
        {
            selectedBlock.GetComponent<PreviewBlock>().meshesParent.gameObject.SetActive(true);
            rayController.raycastMask = onSelectedMask;
            // Get Hit Position
            if (rayController.TryGetHitInfo(out hitPosition, out var hitNormals, out _, out _))
            {
                Vector3 rayHitPosition = hitPosition;
                Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);
                selectedBlock.SeeOnGrid(blockPos);
            }
        }
    }

    private void PlaceBlock(SelectExitEventArgs args)
    {
        PositionBlock();
    }

    private void RepositionBlock(SelectEnterEventArgs args)
    {
        if (toolManager.toolInUse != ToolManager.ToolSelection.transform)
            return;
        Debug.Log("Selector repositioning block");
        PositionBlock();
    }

    private void PositionBlock()
    {
        if (selectedBlock != null)
        {
            // Get Hit Position
            if (rayController.TryGetHitInfo(out hitPosition, out _, out _, out _))
            {
                // checks position of hit
                Vector3 rayHitPosition = hitPosition;
                Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);
                if (selectedBlock.PlaceOnGrid(blockPos))
                {
                    //selectedBlock.blockMainCollider.enabled = true;
                    selectedBlock.EnableColliders(true);
                    if (selectedBlock.GetComponent<Blockfloor_V2>())
                    {
                        toolManager.ChangeTool(2);
                        ForgetBlock();
                    }
                    else if (selectedBlock.GetComponent<BlockWall>() || selectedBlock.GetComponent<BlockFurniture>())
                    {
                        // check if available to place multiple objects. and keep in building mode if in building mode
                        if (toolManager.toolInUse == ToolManager.ToolSelection.build)
                        {
                            SelectBlock(selectedBlock, false);
                        }
                        if (toolManager.toolInUse == ToolManager.ToolSelection.transform)
                        {
                            ForgetBlock();
                        }
                    }
                }

            }
            if (toolManager.toolInUse == ToolManager.ToolSelection.edit)
            {
                //selectedBlock.blockMainCollider.enabled = true;
                selectedBlock.EnableColliders(true);
            }
        }
    }

    public void ForgetBlock()
    {
        selectedBlock.isEditing = false;
        selectedBlock = null;
        rayController.raycastMask = normalStateMask;
    }
    
    public void SetNewReference(Block newBlockReference)
    {
        if(selectedBlock!= null)
        {
            Destroy(selectedBlock.gameObject);
        }
        blockToSpawn = newBlockReference;
        SelectBlock(blockToSpawn, false);
    }
    
    public void SelectBlock(Block chosenBlock, bool isInScene)
    {
        if (isInScene)
        {
            blockToSpawn = null;
            selectedBlock = chosenBlock;
        }
        else
        {
            blockToSpawn = chosenBlock;
            Vector3 spawnPos = transform.position + offsetBlock;
            selectedBlock = Instantiate(blockToSpawn, spawnPos, Quaternion.identity);
        }
        selectedBlock.GetComponent<TransformBlock>().MakeBlockEditable(true);
        chosenBlock.isEditing = true;
        if (selectedBlock.colliders != null) //&& selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
        {
            Debug.Log("disabling collider");
            //selectedBlock.blockMainCollider.enabled = false;
            selectedBlock.EnableColliders(false);
        }
        rayController.raycastMask = onSelectedMask;
    }

    private void DeleteBlocks(SelectEnterEventArgs args)
    {
        if(toolManager.toolInUse == ToolManager.ToolSelection.delete)
        {
            selectedBlock.Delete();
            EnterDeleteMode();
        }    
    }
}
