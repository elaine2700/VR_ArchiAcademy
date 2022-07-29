using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsField;
    float fps;

    private void Update()
    {
        fps = 1/Time.deltaTime;
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        fpsField.text = $"FPS: {fps.ToString()}";
            
    }
}
