using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    // public float movementSpeed = 0.2f;
    // public float movementTime;
    float rotationAmount = 0.2f;
    float pitchAmount = 0.2f;
    // public Vector3 zoomAmount = new Vector3(0f, 0.2f, -0.2f);

    // public Vector3 newPosition;
    Quaternion newRotation;
    Quaternion newPitch;    
    // public Vector3 newZoom;
    void Start()
    {
        // newPosition = transform.position;
        newRotation = transform.rotation;
        newPitch = transform.rotation;
        // newZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        // if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        // {
        //     newPosition += (transform.forward * movementSpeed);
        // }

        // if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        // {
        //     newPosition += (transform.forward * -movementSpeed);
        // }

        // if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        // {
        //     newPosition += (transform.right * movementSpeed);
        // }

        // if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        // {
        //     newPosition += (transform.right * -movementSpeed);
        // }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            newPitch *= Quaternion.Euler(Vector3.right * pitchAmount);
        }

        if (Input.GetKey(KeyCode.C))
        {
            newPitch *= Quaternion.Euler(Vector3.right * -pitchAmount);
        }

        // if (Input.GetKey(KeyCode.R))
        // {
        //     newZoom += zoomAmount;
        // }
        // if (Input.GetKey(KeyCode.F))
        // {
        //     newZoom -= zoomAmount;
        // }

        transform.rotation = newRotation * newPitch;
        // transform.position = newPosition;
        // cameraTransform.localPosition = newZoom;

    }
}
