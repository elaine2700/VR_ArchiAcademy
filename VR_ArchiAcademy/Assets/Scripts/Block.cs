using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    bool isPlaced = false;
    Selector selector;

    private void Start()
    {
        selector = FindObjectOfType<Selector>();
        transform.localScale *= 0.05f; // todo set 0.05 as variable of scale of grid
    }

    private void Update()
    {
        // if not placed
        if (isPlaced)
        {
            return;
        }
        // update location to follow selector
        transform.position = selector.transform.position;
    }

    private void PlaceOnGrid()
    {

    }
}
