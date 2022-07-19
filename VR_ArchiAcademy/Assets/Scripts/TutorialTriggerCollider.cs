using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerCollider : MonoBehaviour
{
    bool hasEntered = false;
    public bool HasEntered { get { return hasEntered; } }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Debug.Log($"{otherCollider.name} has entered");
        hasEntered = true;
    }
}
