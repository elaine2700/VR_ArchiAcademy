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

    // It gets Position from GridTile position, and Parents the block with its respective folder
    public void PlaceOnGrid(Vector3 newPos, GameObject newParent)
    {
        isPlaced = true;
        transform.position = newPos;
        transform.parent = newParent.transform;
            
    }
}
