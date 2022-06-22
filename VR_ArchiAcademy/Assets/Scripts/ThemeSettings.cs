using UnityEngine;

[CreateAssetMenu(fileName = "ThemeSettings", menuName ="ScriptableObjects/ThemeSettingsData", order = 1)]
public class ThemeSettings : ScriptableObject
{
    public Material previewBlockMaterial;
    public Material overlapBlockMaterial;
    public Material hoveredBlockMaterial;
    public Material selectedBlockMaterial;

    public Material activeHandleMat;
    public Material inactiveHandleMat;
}
