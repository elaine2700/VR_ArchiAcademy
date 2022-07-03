using UnityEngine;
using TMPro;

public class ScaleLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scaleTextField;
    Scaler scaler;

    private void Awake()
    {
        scaler = FindObjectOfType<Scaler>();
    }

    private void Start()
    {
        UpdateLabel();
    }

    private void OnEnable()
    {
        scaler.onChangeScale.AddListener(UpdateLabel);
    }

    private void OnDisable()
    {
        scaler.onChangeScale.RemoveListener(UpdateLabel);
    }

    private void UpdateLabel()
    {
        int currentScale = scaler.CurrentScaleInverse;
        string newScaleText = $"1:{currentScale}";
        scaleTextField.text = newScaleText;
    }
}
