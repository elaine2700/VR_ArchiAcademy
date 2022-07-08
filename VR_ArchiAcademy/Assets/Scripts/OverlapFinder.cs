using UnityEngine;

public class OverlapFinder : MonoBehaviour
{
    [SerializeField] Vector3 objectSize = new Vector3();
    [SerializeField] LayerMask findOverlapMask;

    public bool FindAvailablePosition()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Vector3 centerPiece = transform.position;
        Vector3 halfSizeOverlapBox = (objectSize / 2);
        //Check when there is a new collider coming into contact with the box
        bool isPlaceable = false;
        for (int i = 0; i < 2; i++)
        {
            Collider[] hitColliders = Physics.OverlapBox(centerPiece, halfSizeOverlapBox, transform.rotation, findOverlapMask);
            isPlaceable = hitColliders.Length == 0;
        }
        return isPlaceable;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, objectSize);
    }
}
