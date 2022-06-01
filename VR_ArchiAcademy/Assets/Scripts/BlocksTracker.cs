using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksTracker : MonoBehaviour
{
    List<BlockFloor> areas = new List<BlockFloor>();

    public void AddAreaToList(BlockFloor blockFloor)
    {
        areas.Add(blockFloor);
    }
}
