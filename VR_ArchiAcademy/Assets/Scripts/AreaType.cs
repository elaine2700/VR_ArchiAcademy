using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AreaType : MonoBehaviour
{
    string area;

    public void GetAreaType()
    {
        int indexList = GetComponent<TMP_Dropdown>().value;
        area = GetComponent<TMP_Dropdown>().options[indexList].text;
    }

    private void SendAreaName()
    {

    }
}
