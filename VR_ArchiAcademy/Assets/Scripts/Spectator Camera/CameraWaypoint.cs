using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWaypoint : MonoBehaviour
{
    [SerializeField] SpectatorCamera recorder;
    bool triggerEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerEntered && other.CompareTag("Camera"))
        {
            Debug.Log("waypoint reached");
            recorder.NextWaypoint();
            triggerEntered = true;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
