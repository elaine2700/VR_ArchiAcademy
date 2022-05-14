using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButton : MonoBehaviour
{
    public Block blockPrefab;

    Selector selector;

    private void Start()
    {
        selector = FindObjectOfType<Selector>();
    }

    public void ChooseBlock()
    {
        selector.ChooseBlock(blockPrefab);
    }
}
