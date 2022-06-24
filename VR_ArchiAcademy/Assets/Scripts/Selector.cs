using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : MonoBehaviour
{
    [SerializeField] Vector3 offsetBlock = new Vector3();
    [SerializeField] GridTile gridTile;

    public Block selectedBlock = null;
    bool blockIsInScene = false;
    bool isHovering = false;
    Vector3 hitPosition;

    StateManager stateManager;
    XRRayInteractor rayController;

    private void Awake()
    {
        stateManager = FindObjectOfType<StateManager>();
        rayController = GetComponentInParent<XRRayInteractor>();
        gridTile = FindObjectOfType<GridTile>();
    }

    private void Update()
    {
        
        isHovering = gridTile.OnGrid();
  
        if(stateManager.globalState == StateManager.GlobalState.selecting && selectedBlock == null)
        {
            return;
        }
        if (!isHovering)
        {
            if(selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
                selectedBlock.transform.position = transform.position + offsetBlock ;
            return;
        }    
        else if (rayController.TryGetHitInfo(out hitPosition, out var hitNormals, out _, out _))
        {
            if (hitPosition != null && (selectedBlock.GetComponent<TransformBlock>().isEditablePosition || !blockIsInScene))
            {
                Vector3 rayHitPosition = hitPosition;
                Vector3 blockPos = gridTile.SnapPosition(rayHitPosition, selectedBlock.snap);

                selectedBlock.PreviewPosGrid(blockPos);
            }
        }
    }

    // Called from XR Event
    public void PlaceBlock(SelectEnterEventArgs args)
    {
        Debug.Log("Selector placing block");
        blockIsInScene = true;
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
        stateManager.ChangeState(StateManager.GlobalState.selecting);
        ForgetBlock();
    }

    public void ForgetBlock()
    {
        selectedBlock = null;
    }

    // Gets the Prefab information from BlockButton script.
    public void ChooseBlock(Block chosenBlock, bool isInScene)
    {
        if (stateManager.globalState != StateManager.GlobalState.selecting)
            return;

        blockIsInScene = isInScene;
        //if (selectedBlock != null) { return; }
        
        stateManager.ChangeState(StateManager.GlobalState.transforming);

        if (isInScene)
        {
            selectedBlock = chosenBlock;
        }
        else
        {
            Vector3 blockPos = transform.position + offsetBlock;
            selectedBlock = Instantiate(chosenBlock, blockPos, Quaternion.identity);
        }
        selectedBlock.GetComponent<TransformBlock>().MakeBlockEditable(true);
        if (chosenBlock.blockMaincollider != null && selectedBlock.GetComponent<TransformBlock>().isEditablePosition)
        {
            //Debug.Log("Disabling collider");
            chosenBlock.blockMaincollider.enabled = false;
        }    
    }

}
