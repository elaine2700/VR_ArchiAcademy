using System.Collections.Generic;
using UnityEngine;

public class BlocksTracker : MonoBehaviour
{
    // todo Hide List on Inspector
    public List<Blockfloor_V2> rooms = new List<Blockfloor_V2>();
    public List<BlockWall> blockWalls = new List<BlockWall>();
    public List<BlockFurniture> blockFurnitures = new List<BlockFurniture>();

    public void AddRoomToList(Blockfloor_V2 blockFloor)
    {
        rooms.Add(blockFloor);
    }

    public void AddWallToList(BlockWall blockWall)
    {
        blockWalls.Add(blockWall);
    }

    public void AddFurnitureToList(BlockFurniture blockFurniture)
    {
        blockFurnitures.Add(blockFurniture);
    }

    public void MakeFloorsNonEditable()
    {
        foreach (Blockfloor_V2 blockFloor in rooms)
        {
            blockFloor.GetComponent<TransformBlock>().MakeBlockEditable(false);
        }
    }
}
