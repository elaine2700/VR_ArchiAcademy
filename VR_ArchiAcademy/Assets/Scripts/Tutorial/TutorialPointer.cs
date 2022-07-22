using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TutorialPointer : MonoBehaviour
{
    [SerializeField] Image arrowImage;
    int animationIndex = 0;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowArrow(bool show)
    {
        arrowImage.enabled = show;
    }

    public void GoToNextPosition()
    {
        animationIndex++;
        animator.SetInteger("ArrowPlaceIndex", animationIndex);
    }
}
