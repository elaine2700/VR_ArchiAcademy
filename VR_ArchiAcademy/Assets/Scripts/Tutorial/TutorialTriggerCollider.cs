using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerCollider : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    bool hasEntered = false;
    public bool HasEntered { get { return hasEntered; } }

    private void Start()
    {
        arrow.SetActive(false);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Debug.Log($"{otherCollider.name} has entered");
        Entered();
    }

    private void Entered()
    {
        hasEntered = true;
        arrow.SetActive(false);
        Destroy(this.gameObject, 2f);
    }

    public void ShowArrow()
    {
        arrow.SetActive(true);
    }

}
