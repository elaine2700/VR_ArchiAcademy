using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scaler : MonoBehaviour
{
    [SerializeField] int currentScale = 1;
    /// <summary>
    /// The current scale. Ex: 1 is 1:1, 2 is 1:2 ...
    /// </summary>
    public int CurrentScaleInverse { get { return currentScale; } }
    public List<int> scaleOptions = new List<int>();

    int indexCurrentScale = 0;
    float modelScale = 1f;
    /// <summary>
    /// The current scale in decimal. Eg. 0.5 is 1:2 ...
    /// </summary>
    public float ModelScale { get { return modelScale; } }

    private bool canChangeScale = true;

    Actions inputActions;
    public UnityEvent onChangeScale;

    private void Awake()
    {
        inputActions = new Actions();
        inputActions.Tools.ScaleWorld.performed += _ => NextScale();
        //Debug.Log("Scaler awake");
    }

    private void OnEnable()
    {
        inputActions.Tools.Enable();
    }

    private void OnDisable()
    {
        inputActions.Tools.Disable();
    }

    private void Start()
    {
        currentScale = 1;
    }

    private void ModifyingScale(int newScale)
    {
        if (canChangeScale)
        {
            currentScale = newScale;
            modelScale = 1f / currentScale;
            onChangeScale.Invoke();
        }
    }

    private void NextScale()
    {
        indexCurrentScale++;
        if(indexCurrentScale >= scaleOptions.Count)
        {
            indexCurrentScale = 0;
        }
        ModifyingScale(scaleOptions[indexCurrentScale]);
    }

    /// <summary>
    /// Enables or disables the ability to change scales at runtime.
    /// </summary>
    /// <param name="changeScale"></param>
    public void EnableChangeScale(bool changeScale)
    {
        canChangeScale = changeScale;
    }
}
