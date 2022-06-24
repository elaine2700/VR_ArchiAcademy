using System.Collections.Generic;
using UnityEngine;

public class BlocksTracker : MonoBehaviour
{
    List<Blockfloor_V2> rooms = new List<Blockfloor_V2>();

    public void AddRoomToList(Blockfloor_V2 blockFloor)
    {
        rooms.Add(blockFloor);
    }

    public void MakeFloorsNonEditable()
    {
        foreach (Blockfloor_V2 blockFloor in rooms)
        {
            blockFloor.GetComponent<TransformBlock>().MakeBlockEditable(false);
        }
    }
}
