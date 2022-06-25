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

    // todo Open UI page depending on active layer
    // todo Opening a page sets the active layer

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void Select()
    {
        if (onTabSelected != null)
            onTabSelected.Invoke();
    }

    public void Deselect()
    {
        if (onTabDeselected != null)
            onTabDeselected.Invoke();
    }
}
