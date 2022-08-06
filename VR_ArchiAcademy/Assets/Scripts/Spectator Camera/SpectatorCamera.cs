using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to control a camera different than the one inside the XR rig.
/// Its main purpose is to move the camera to different positions and rotate the view,
/// to see what the player is doing.
/// </summary>
public class SpectatorCamera : MonoBehaviour
{
    enum cameraMode {manual, waypoint, fpCamera};
    [SerializeField] cameraMode recordingMode = cameraMode.manual;

    Actions droneController;

    [Header("Manual")]
    [Range(0f, 20f)]
    [SerializeField] float movementSpeed = 2;

    [Range(0f, 20f)]
    [SerializeField] float rotationSpeed = 1;

    [Header("Waypoints")]
    [SerializeField] List<Transform> waypoints = new List<Transform>();
    [SerializeField] int waypointCounter = 0;
    //Transform currentWaypoint;
    [SerializeField] Transform nextWaypoint;
    [SerializeField] bool loop = false;

    [Header("First Person")]
    [SerializeField] Camera fpCamera;
    [SerializeField] List<GameObject> playerObjects = new List<GameObject>();
 
    Vector2 move;
    Vector2 rotation;
    float height;

    float xRotation = 0f;
    float yRotation = 0f;

    private void Awake()
    {
        droneController = new Actions();
        droneController.Camera.Move.performed += ctxt => move = ctxt.ReadValue<Vector2>();
        droneController.Camera.Move.canceled += cntxt => move = Vector2.zero;
        droneController.Camera.Look.performed += ctxt => rotation = ctxt.ReadValue<Vector2>();
        droneController.Camera.Look.canceled += ctxt => rotation = Vector2.zero;

        droneController.Camera.Fly.performed += ctxt => height = ctxt.ReadValue<float>();
        droneController.Camera.Fly.canceled += cftxt => height = 0f;

        droneController.Camera.ResetHorizontal.performed += _ => ResetHorizontal();
    }

    private void Start()
    {
        NextWaypoint();
        //nextWaypoint = waypoints[waypointCounter + 1];
        if(recordingMode == cameraMode.fpCamera)
        {
            foreach(GameObject playerObject in playerObjects)
            {
                playerObject.SetActive(false);
            }
        }

    }

    private void OnEnable()
    {
        droneController.Camera.Enable();

    }

    private void OnDisable()
    {
        droneController.Camera.Disable();

    }

    private void Update()
    {
        if(recordingMode == cameraMode.manual)
        {
            MoveDrone();
            RotateDrone();
            Fly();
        }
        if(recordingMode == cameraMode.waypoint)
        {
            FollowTarget(transform, nextWaypoint, true);
        }
        if(recordingMode == cameraMode.fpCamera)
        {
            FollowTarget(transform, fpCamera.transform, true);
        }
    }

    /// <summary>
    /// Moves the GameObject to the front, back, left and right.
    /// </summary>
    private void MoveDrone()
    {
        if (move == Vector2.zero)
            return;
        Debug.Log(move);

        if (move != Vector2.zero)
        {
            float moveX = move.x * movementSpeed * Time.deltaTime;
            float moveZ = move.y * movementSpeed * Time.deltaTime;
            Vector3 movPos = new Vector3(moveX, 0f, moveZ);

            transform.Translate(movPos, Space.Self);
        }
    }

    /// <summary>
    /// Rotates the view up, down, left and right.
    /// </summary>
    private void RotateDrone()
    {
        if (rotation == Vector2.zero)
            return;

        float rotationX = rotation.x * rotationSpeed * Time.deltaTime;
        float rotationY = rotation.y * rotationSpeed * Time.deltaTime;
        Debug.Log(rotation);

        xRotation -= rotationY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += rotationX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    /// <summary>
    /// It moves the object to different height. Up or down movement.
    /// </summary>
    private void Fly()
    {
        if (height == 0)
            return;
        float newHeight = height * movementSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * newHeight);
    }

    /// <summary>
    /// When pressing the ResetHorizontal button, it changes the object rotation horizontaly.
    /// </summary>
    private void ResetHorizontal()
    {
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
    }

    private void FollowTarget(Transform start, Transform to, bool followRotation)
    {
        transform.position = Vector3.Lerp(transform.position, to.position, Time.deltaTime * movementSpeed);
        if(followRotation)
            transform.rotation = Quaternion.Lerp(transform.rotation, to.rotation, Time.deltaTime * rotationSpeed);
    }

    public void NextWaypoint()
    {
        Debug.Log("Next waypoint");
        waypointCounter++;
        //currentWaypoint = nextWaypoint;
        if(waypointCounter == waypoints.Count)
        {
            Debug.Log("Finished Waypoints");
            //this.gameObject.SetActive(false);
            if (loop)
            {
                waypointCounter = -1;
                ResetWaypoints();
                NextWaypoint();
            }
            else
            {
                return;
            }
        }
        Debug.Log("Changing waypoint");
        nextWaypoint = waypoints[waypointCounter]; 
    }


    void ResetWaypoints()
    {
        foreach(Transform Waypoint in waypoints)
        {
            Waypoint.GetComponent<CameraWaypoint>().triggerEntered = false;
        }
    }
}
