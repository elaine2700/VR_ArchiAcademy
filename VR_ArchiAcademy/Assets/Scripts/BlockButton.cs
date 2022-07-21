using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class BlockButton : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] Block blockPrefab;

    bool isPressed = false;
    public bool IsPressed { get { return isPressed; } }

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
        isPressed = true;
    }

    public void OnPointerExit (PointerEventData data)
    {
        Debug.Log("Deselected");
        isPressed = false;
    }

    public void ActivateButton(bool activate)
    {
        GetComponent<Button>().interactable = activate;
    }
}
