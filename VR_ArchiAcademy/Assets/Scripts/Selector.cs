using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : MonoBehaviour
{
    // this class selects objects to build and transform.

    [SerializeField] Vector3 offsetBlock = new Vector3();
    [SerializeField] GridTile gridTile;

    public Block blockToSpawn = null; // to use in Building mode
    public Block selectedBlock = null; // to use in Transforming mode
    public Block blockToMove = null;

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
        toolManager.OnToolBuild.AddListener(EnterBuildMode);
        toolManager.OnToolTransform.AddListener(EnterTransformMode);
        rayController.selectEntered.AddListener(PlaceBlock);
    }

    private void OnDisable()
    {
        toolManager.OnToolBuild.RemoveListener(EnterBuildMode);
        toolManager.OnToolTransform.RemoveListener(EnterTransformMode);
        rayController.selectEntered.RemoveListener(PlaceBlock);
    }

    private void Update()
    {
        isHovering = gridTile.OnGrid();

        if (toolManager.toolInUse == ToolManager.ToolSelection.build)
            BuildingMode();
        else if (toolManager.toolInUse == ToolManager.ToolSelection.transform)
            TransformingMode();
        else if (toolManager.toolInUse == ToolManager.ToolSelection.edit)
            EditMode();
        else if (toolManager.toolInUse == ToolManager.ToolSelection.delete)
            DeleteBlocks();
        else
            SelectMode();
    }

    private void EnterBuildMode()
    {
        Debug.Log("Entering Build Mode");
    }

    private void EnterTransformMode()
    {
        Debug.Log("Entering Transform Mode");
        blockToSpawn = null;
        if (!selectedBlock.IsPlaced)
        {
            Destroy(selectedBlock.gameObject);
            selectedBlock = null;
        }
    }

    private void BuildingMode()
    {
        if(selectedBlock != null)
        {
            if (!isHovering)
            {
                selectedBlock.transform.position = transform.position + offsetBlock;
            }
            else
            {
                if (rayController.TryGetHitInfo(out hitPosition, out var hitNormals, out _, out _))
                {
                    Vector3 rayHitPosition = hitPosition;
                    Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);
                    selectedBlock.SeeOnGrid(blockPos);
                }
            }
        }
    }

    private void TransformingMode()
    {
        if (selectedBlock != null)
        {
            if (selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
            {
                if (!isHovering)
                {
                    // see on controller
                    selectedBlock.transform.position = transform.position + offsetBlock;
                }
                else if (rayController.TryGetHitInfo(out hitPosition, out var hitNormals, out _, out _))
                {
                    // see over grid
                    Vector3 rayHitPosition = hitPosition;
                    Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);
                    selectedBlock.SeeOnGrid(blockPos);
                }
            }
        }
    }

    private void EditMode()
    {

    }

    private void SelectMode()
    {

    }

    public void PlaceBlock(SelectEnterEventArgs args)
    {
        Debug.Log("Selector placing block");
        //blockIsInScene = true;

        if (selectedBlock != null)
        {
            if (rayController.TryGetHitInfo(out hitPosition, out _, out _, out _))
            {
                // checks position of hit
                Vector3 rayHitPosition = hitPosition;
                Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);
                selectedBlock.PlaceOnGrid(blockPos);

                if (selectedBlock.GetComponent<Blockfloor_V2>())
                {
                    toolManager.ChangeTool(2);
                    ForgetBlock();
                }
                else if (selectedBlock.GetComponent<BlockWall>() || selectedBlock.GetComponent<BlockFurniture>())
                {
                    // check if available to place multiple objects. and keep in building mode if in building mode
                    if(toolManager.toolInUse == ToolManager.ToolSelection.build)
                    {
                        ChooseBlock(blockToSpawn, false);
                    }
                    if(toolManager.toolInUse == ToolManager.ToolSelection.transform)
                    {
                        ForgetBlock();
                    }
                }
            }
        }
    }

    public void ForgetBlock()
    {
        selectedBlock = null;
    }

    
    public void SetNewReference(Block newBlockReference)
    {
        if(selectedBlock!= null)
        {
            Destroy(selectedBlock.gameObject);
        }
        blockToSpawn = newBlockReference;
        ChooseBlock(blockToSpawn, false);
    }
    
    public void ChooseBlock(Block chosenBlock, bool isInScene)
    {
        if (isInScene)
        {
            selectedBlock = chosenBlock;
            blockToSpawn = null;
        }
        else
        {
            blockToSpawn = chosenBlock;
            selectedBlock = Instantiate(blockToSpawn);
        }
        selectedBlock.GetComponent<TransformBlock>().MakeBlockEditable(true);
        if (chosenBlock.blockMaincollider != null && selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
        {
            chosenBlock.blockMaincollider.enabled = false;
        }    
    }

    private void DeleteBlocks()
    {
        if (selectedBlock != null)
        {
            selectedBlock.Delete();
            selectedBlock = null;
        }
    }

}
