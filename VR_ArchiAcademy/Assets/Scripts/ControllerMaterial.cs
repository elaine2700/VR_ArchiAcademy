using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMaterial : MonoBehaviour
{
    [SerializeField] Renderer gripButton;
    [SerializeField] Renderer triggerButton;
    [SerializeField] Renderer thumbstickButton;

    [SerializeField] Color blinkingColor;
    Color normalColor = Color.white;

    private bool blink = false;

    IEnumerator BlinkButton(float blinkingSpeed, Renderer renderer)
    {
        //float endTime = Time.time + time;
        blink = true;
        while (blink)
        {
            renderer.material.color = blinkingColor;
            yield return new WaitForSeconds(blinkingSpeed);
            renderer.material.color = normalColor;
            yield return new WaitForSeconds(blinkingSpeed);
            Debug.Log("blinking");
        }
        renderer.material.color = normalColor;
        Debug.Log("Stop blinking");
    }

    public IEnumerator BlinkGripButton()
    {
        yield return BlinkButton(0.4f, gripButton);
    }

    public IEnumerator BlinkTriggerButton()
    {
        yield return BlinkButton(0.4f, triggerButton);
    }

    public IEnumerator BlinkThumstick()
    {
        yield return BlinkButton(0.4f, thumbstickButton);
    }

    public void StopBlinking()
    {
        blink = false;
    }

}
