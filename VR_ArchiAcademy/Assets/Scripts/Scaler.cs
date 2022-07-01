using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Scaler : MonoBehaviour
{
    [SerializeField] int currentScale = 1;
    public List<int> scaleOptions = new List<int>();

    int indexCurrentScale = 0;
    float modelScale = 1f;
    public float ModelScale { get { return modelScale; } }

    Actions inputActions;
    public UnityEvent onChangeScale;

    private void Awake()
    {
        inputActions = new Actions();
        inputActions.Interaction.ScaleWorld.performed += _ => NextScale();
        Debug.Log("Scaler awake");
    }

    private void OnEnable()
    {
        inputActions.Interaction.Enable();
    }

    private void OnDisable()
    {
        inputActions.Interaction.Disable();
    }

    private void Start()
    {
        currentScale = 1;
    }

    private void ModifyingScale(int newScale)
    {
        currentScale = newScale;
        modelScale = 1f / currentScale;
        onChangeScale.Invoke();
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
}
