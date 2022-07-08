using UnityEngine;
using UnityEngine.UI;

public class BlockButton : MonoBehaviour
{
    [SerializeField] Block blockPrefab;

    Selector selector;

    private void Awake()
    {
        selector = FindObjectOfType<Selector>();
        //areaType = FindObjectOfType<RoomType>();
    }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(ChooseBlock);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(ChooseBlock);
    }

    private void ChooseBlock()
    {
        // choosing a new block always sets the toolInUse to build
        selector.SetNewReference(blockPrefab);
        selector.GetComponent<ToolManager>().ChangeTool(1);
    }
    
}
