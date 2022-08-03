using UnityEngine;

/// <summary>
/// Class used to control a camera different than the one inside the XR rig.
/// Its main purpose is to move the camera to different positions and rotate the view,
/// to see what the player is doing.
/// </summary>
public class SpectatorCamera : MonoBehaviour
{
    Actions droneController;

    [Range(0f, 20f)]
    [SerializeField] float movementSpeed = 2;

    [Range(0f, 20f)]
    [SerializeField] float rotationSpeed = 1;
 
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
        MoveDrone();
        RotateDrone();
        Fly();
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
}