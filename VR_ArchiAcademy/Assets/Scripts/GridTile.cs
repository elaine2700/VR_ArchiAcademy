using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridTile : MonoBehaviour
{
    [SerializeField] Transform parentWorld;
    Scaler scaler;
    [SerializeField] List<GridMaterialSettings> gridMaterialSettings = new List<GridMaterialSettings>();
    int indexGridSettings = 9;
    Material gridMaterial;
    public Vector2 gridMinUnit;

    private void Awake()
    {
        scaler = FindObjectOfType<Scaler>();
        if (scaler == null)
            Debug.Log("Missing Scaler Script. It is not in the scene");
    }

    private void OnEnable()
    {
        scaler.onChangeScale.AddListener(UpdateScale);
    }

    private void OnDisable()
    {
        scaler.onChangeScale.RemoveListener(UpdateScale);
    }

    private void Start()
    {
        gridMaterial = GetComponentInChildren<Renderer>().material;
        UpdateScale();
    }

    public Vector3 SnapPosition(Vector3 hitPos, bool snapToGrid)
    {
        // calculate snapping position
        float posX;
        float posZ;
        if (snapToGrid)
        {
            posX = Mathf.Round(hitPos.x / gridMinUnit.x) * gridMinUnit.x;
            posZ = Mathf.Round(hitPos.z / gridMinUnit.y) * gridMinUnit.y;
        }
        else
        {
            posX = hitPos.x;
            posZ = hitPos.z;
        }
        
        Vector3 snapPos = new Vector3(posX, hitPos.y, posZ);
        return snapPos;
    }

    public bool OnGrid()
    {
        bool onGrid = false;
        if (GetComponent<XRSimpleInteractable>().isHovered)
        {
            onGrid = true;
        }
        else
        {
            onGrid = false;
        }
        return onGrid;
    }

    void UpdateScale()
    {
        float newScale = scaler.ModelScale;
        parentWorld.localScale = new Vector3(newScale, newScale, newScale);
        gridMinUnit = new Vector2(newScale, newScale);
        indexGridSettings++;
        if (indexGridSettings >= gridMaterialSettings.Count)
            indexGridSettings = 0;
        ChangeGridSettings();
    }

    private void ChangeGridSettings()
    {
        float primaryGridSize = 100;
        float primaryGridLineWidth = 0.01f;
        float checkerSize = 200;
        bool useSecondaryGrid = false;
        float secondaryGridSize = 20f;
        float secondaryGridLineWidth = 0.002f;
        float secondaryGridFadeOffset = 100;
        float secondaryGridFadeLenght = 8f;

        GridMaterialSettings currentGridSettings = gridMaterialSettings[indexGridSettings];

        primaryGridSize = currentGridSettings.primaryGridSize;
        primaryGridLineWidth = currentGridSettings.primaryGridLineWidth;
        checkerSize = currentGridSettings.checkerSize;
        useSecondaryGrid = currentGridSettings.useSecondaryGrid;
        secondaryGridSize = currentGridSettings.secondaryGridSize;
        secondaryGridLineWidth = currentGridSettings.secondaryGridLineWidth;
        secondaryGridFadeOffset = currentGridSettings.secondaryGridFadeOffset;
        secondaryGridFadeLenght = currentGridSettings.secondaryGridFadeLenght;

        gridMaterial.SetFloat("_Primary_grid_size__cm_", primaryGridSize);
        gridMaterial.SetFloat("_Primary_Grid_line_Width", primaryGridLineWidth);
        gridMaterial.SetFloat("_Checker_size__cm_", checkerSize);
        if (useSecondaryGrid)
            gridMaterial.SetInt("_Use_secondary_grid", 1);
        else
            gridMaterial.SetInt("_Use_secondary_grid", 0);
        gridMaterial.SetFloat("_Secondary_Grid_size__cm_", secondaryGridSize);
        gridMaterial.SetFloat("_Secondary_grid_line_width", secondaryGridLineWidth);
        gridMaterial.SetFloat("_Secondary_grid_fade_offset", secondaryGridFadeOffset);
        gridMaterial.SetFloat("_Secondary_grid_fade_length", secondaryGridFadeLenght);
    }
}
