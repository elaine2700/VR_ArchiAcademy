using UnityEngine;

[CreateAssetMenu(fileName = "Grid Material Settings", menuName = "ScriptableObjects/Grid Material Settings")]
public class GridMaterialSettings : ScriptableObject
{
    [Header("Primary Grid")]
    public float primaryGridSize = 100f;
    public float primaryGridLineWidth = 0.0005f;
    public float checkerSize = 200f;

    [Header("Secondary Grid")]
    public bool useSecondaryGrid = true;
    public float secondaryGridSize = 1f;
    public float secondaryGridLineWidth = 0.00005f;
    public float secondaryGridFadeOffset = 100;
    public float secondaryGridFadeLenght = 8;
}
