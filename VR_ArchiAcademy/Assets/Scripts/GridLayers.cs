using UnityEngine;

public class GridLayers : MonoBehaviour
{
    [SerializeField] GameObject folderFloor;
    [SerializeField] GameObject folderWalls;
    [SerializeField] GameObject folderFurniture;
 
    public GameObject ParentToCurrentLayer(int blocktype)
    {
        // function to organize blocks on a empty gameObject
        switch (blocktype)
        {
            case 1:
                return folderFloor;
            case 2:
                return folderWalls;
            case 3:
                return folderFurniture;
            default:
                return folderFloor;
        }
    }

    public void HideLayer(bool hide, int layerToHide)
    {
        switch (layerToHide)
        {
            case 1:
                folderFloor.SetActive(hide);
                break;
            case 2:
                folderWalls.SetActive(hide);
                break;
            case 3:
                folderFurniture.SetActive(hide);
                break;
            default:
                Debug.Log("Layer unreachable");
                break;
        }
    }

}
