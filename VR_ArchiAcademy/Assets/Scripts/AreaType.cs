using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AreaType : MonoBehaviour
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
            Debug.Log("Choose an area type");
            return false;
        }

        Debug.Log("Instantiating label");
        var newAreaLabel = Instantiate(areaLabel, contentLabel.transform);
        Debug.Log(newAreaLabel.transform.parent);
        Debug.Log(newAreaLabel.transform.position);
        newAreaLabel.GetComponentInChildren<TextMeshProUGUI>().text = area;
        //ResetOptions();
        return true;
    }

    public void ResetOptions()
    {
        indexList = 0;
    }
}
