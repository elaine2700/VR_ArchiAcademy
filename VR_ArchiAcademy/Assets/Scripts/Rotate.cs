using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Transform rotateTarget;
    [SerializeField] private float rotateAmount = 45f;
    public bool canRotate;

    private bool upThresholdReached = false;
    private float rotateThreshold = 0.5f;//If the joystick is more than 50% up

    Actions actions;
    Vector2 joystickInput;

    private void Awake()
    {
        actions = new Actions();
        actions.Interaction.RotateBlock.performed += cntxt => joystickInput = cntxt.ReadValue<Vector2>();
        actions.Interaction.RotateBlock.canceled += cntxt => joystickInput = Vector2.zero;
    }

    private void OnEnable()
    {
        actions.Interaction.Enable();
    }

    private void OnDisable()
    {
        actions.Interaction.Disable();
    }

    private void Start()
    {
        canRotate = false;
        if(rotateTarget == null)
        {
            rotateTarget = transform;
        }
    }

    void Update()
    {
        if(canRotate)
            Rotating();
    }

    private void Rotating()
    {
        //joystickInput //This is whatever joystick input you're reading from. Value should be from -1 to 1
        if (joystickInput.x > rotateThreshold)
        {
            if (!upThresholdReached)
            {
                upThresholdReached = true;
                RotateUp();
            }
        }
        else
        {
            upThresholdReached = false;
        }
    }

    private void RotateUp()
    {
        Debug.Log("Rotating!");
        rotateTarget.Rotate(0, rotateAmount, 0);
    }

    private void RotateBlock()
    {
        // testing another form of rotation
        Debug.Log("Rotate");
        Debug.Log("JoystickInput: " + joystickInput);
        //float rotationX = Mathf.Round(joystickInput.x); // todo test
        //float rotationY = Mathf.Round(joystickInput.y); // todo test
        float rotationX = Mathf.Round(joystickInput.x / rotateAmount) * rotateAmount;//todo test
        float rotationY = Mathf.Round(joystickInput.y / rotateAmount) * rotateAmount;// todo tes
        Vector3 newRotation = new Vector3(rotationX, 0, rotationY);
        if (joystickInput != Vector2.zero)
            rotateTarget.transform.rotation = Quaternion.LookRotation(newRotation, Vector3.up);
    }
}
