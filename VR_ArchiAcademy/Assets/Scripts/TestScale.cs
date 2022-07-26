using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScale : MonoBehaviour
{
    /*[SerializeField] Scaler scaleAdjuster;
    [SerializeField] Transform target;

    private void OnEnable()
    {
        scaleAdjuster.onChangeScale.AddListener(UpdateScale);
    }

    private void OnDisable()
    {
        scaleAdjuster.onChangeScale.RemoveListener(UpdateScale);
    }

    private void UpdateScale()
    {
        float newScale = scaleAdjuster.ModelScale;
        target.localScale = new Vector3(newScale, newScale, newScale);
    }*/

    [SerializeField] ControllerMaterial controller;
    [SerializeField] bool stopBlinking = false;

    private void Start()
    {
        StartCoroutine(controller.BlinkThumstick());
    }

    private void Update()
    {
        if (stopBlinking)
        {
            controller.StopBlinking();
        }
    }
}
