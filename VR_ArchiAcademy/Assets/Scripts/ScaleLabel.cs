using UnityEngine;
using TMPro;

public class ScaleLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scaleTextField;
    Scaler scaler;

    private void Awake()
    {
        scaler = FindObjectOfType<Scaler>();
        if(scaler == null)
        {
            Debug.LogError("Scaler script is not in the scene");
        }
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
