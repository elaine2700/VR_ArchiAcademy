using UnityEngine;
using UnityEngine.Events;

public class Scaler : MonoBehaviour
{
    public float modelScale = 0.05f;

    UnityEvent onChangeScale;

    public void ModifyingScale(float newScale)
    {
        modelScale = newScale;
        onChangeScale.Invoke();
    }
}
