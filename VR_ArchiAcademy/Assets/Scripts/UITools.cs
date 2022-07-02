using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITools : MonoBehaviour
{
    ToolManager toolManager;
    [SerializeField] Image toolPictureField;

    private void Start()
    {
        toolManager = FindObjectOfType<ToolManager>();
    }
    
    public void ChangeToolPicture(Sprite newSprite)
    {
        toolPictureField.sprite = newSprite;
    }

}
