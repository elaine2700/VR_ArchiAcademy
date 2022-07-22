using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class TutorialButton : MonoBehaviour
{
    Button button;
    bool isPressed = false;
    public bool IsPressed { get { return isPressed; } }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ButtonWasPressed);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void ButtonWasPressed()
    {
        isPressed = true;
    }

    public void ShowButton(bool show)
    {
        isPressed = false;
        gameObject.SetActive(show);
    }

    /*public void OnPointerExit(PointerEventData data)
    {
        isPressed = false;
    }*/


}
