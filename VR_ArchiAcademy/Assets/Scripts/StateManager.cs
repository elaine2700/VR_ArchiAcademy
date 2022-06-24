using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum GlobalState { selecting, transforming, editing, exploring };
    public GlobalState globalState = GlobalState.selecting;

    BlocksTracker blocksTracker;

    private void Start()
    {
        blocksTracker = FindObjectOfType<BlocksTracker>();
    }

    public void ChangeState(GlobalState newState)
    {
        switch (newState)
        {
            case GlobalState.selecting:
                globalState = newState;
                blocksTracker.MakeFloorsNonEditable();
                GetComponent<Selector>().ForgetBlock();
                break;
            case GlobalState.transforming:
                globalState = newState;
                break;
            case GlobalState.editing:
                globalState = newState;
                
                break;
            case GlobalState.exploring:
                globalState = newState;
                // todo set all block to not editable
                break;
            default:
                globalState = newState;
                break;
        }
    }

    public void ChangeToSelect()
    {
        ChangeState(GlobalState.selecting);
    }
}
