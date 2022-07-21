using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialEvents : MonoBehaviour
{
    public UnityEvent NextStep = new UnityEvent();

    public void NextInstruction()
    {
        NextStep.Invoke();
    }
}
