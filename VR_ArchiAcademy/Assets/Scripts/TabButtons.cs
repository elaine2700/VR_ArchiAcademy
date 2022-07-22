using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TabGroup tabGroup;
    public Image background;
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    [SerializeField] string buttonName;
    public string ButtonName { get { return buttonName; } }
    bool isSelected = false;
    public bool IsSelected { get { return isSelected; } }
    bool isActive = true;
    public bool IsActive { get { return isActive; } set { isActive = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isActive)
            tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isActive)
            tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isActive)
            tabGroup.OnTabExit(this);
    }

    private void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void Select()
    {
        if (onTabSelected != null && isActive)
            onTabSelected.Invoke();
        isSelected = true;
    }

    public void Deselect()
    {
        if (onTabDeselected != null && isActive)
            onTabDeselected.Invoke();
        isSelected = false;
    }

    public void MakeNonInteractable()
    {
        background.color = tabGroup.tabDisabled;
        isActive = false;
        //this.enabled = false;
    }

    public void MakeInteractable()
    {
        isActive = true;
        background.color = tabGroup.tabIdle;
        //this.enabled = true;
    }
}
