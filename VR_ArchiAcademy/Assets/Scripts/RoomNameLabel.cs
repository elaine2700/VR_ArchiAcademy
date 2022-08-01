using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNameLabel : MonoBehaviour
{
    public void UpdateLabelPosition(Vector3 newPos)
    {
        Vector3 updatedPosition = new Vector3(newPos.x, transform.position.y, newPos.z);
        transform.localPosition = updatedPosition;
    }
}
