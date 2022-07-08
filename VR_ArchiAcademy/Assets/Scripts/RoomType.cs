using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomType : MonoBehaviour
{
    [SerializeField] RectTransform areaLabel;
    [SerializeField] RectTransform contentLabel;

    public string area;

    int indexList;
    
    public void GetAreaType()
    {
        indexList = GetComponent<TMP_Dropdown>().value;
        area = GetComponent<TMP_Dropdown>().options[indexList].text;
    }

    // called from Block Button Script
    public bool SetAreaName()
    {
        if(area == null)
        {
            Debug.LogError("Choose an area type");
            return false;
        }

        var newAreaLabel = Instantiate(areaLabel, contentLabel.transform);
        newAreaLabel.GetComponentInChildren<TextMeshProUGUI>().text = area;
        return true;
    }

    public void ResetOptions()
    {
        indexList = 0;
    }
}
