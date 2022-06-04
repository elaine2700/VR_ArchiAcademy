using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButton : MonoBehaviour
{
    public Block blockPrefab;

    Selector selector;
    AreaType areaType;

    private void Start()
    {
        selector = FindObjectOfType<Selector>();
        areaType = FindObjectOfType<AreaType>();
    }

    public void ChooseBlock()
    {
        selector.ChooseBlock(blockPrefab, false);
    }

    // when button is used to select BlockFloor
    public void ChooseFloor()
    {
        if (areaType.SetAreaName())
        {
            ChooseBlock();
        }

    }
    
}
