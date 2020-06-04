using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float moveSmoothing = 5f;
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float rotationAmount = 2f;
    [SerializeField] private Vector3 zoomAmount;
    [SerializeField] private float worldEdgeBuffer;

    private Vector3 newPosition;
    private Quaternion newRotation;
    private Vector3 newZoom;
    private Vector3 startRotatePosition;
    private Vector3 currentRotatePosition;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        mainCamera = Camera.main;
        newZoom = mainCamera.transform.localPosition;
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            newZoom += -Input.mouseScrollDelta.y * zoomAmount;
        }

        if(Input.GetMouseButtonDown(2))
        {
            startRotatePosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            Cursor.visible = false; 
            currentRotatePosition = Input.mousePosition;
            Vector3 difference = startRotatePosition - currentRotatePosition;

            startRotatePosition = currentRotatePosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
        if(Input.GetMouseButtonUp(2))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }

    void HandleMovementInput()
    {
        float movementSpeed = moveSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = moveSpeed * speedMultiplier;
        }
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            newPosition += (transform.right * movementSpeed);
        }
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            newPosition += (transform.right * -movementSpeed);
        }
        if(Input.GetAxisRaw("Vertical") > 0)
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if(Input.GetAxisRaw("Vertical") < 0)
        {
            newPosition += (transform.forward * -movementSpeed);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
            
        }
        if(Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
    
        newPosition = ConstrainPosition(newPosition);
        newZoom = ConstrainZoom(newZoom);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime *  moveSmoothing);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime *  moveSmoothing);
        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, newZoom, Time.deltaTime *  moveSmoothing);
    }

    Vector3 ConstrainPosition(Vector3 position)
    {
        float newX = Mathf.Clamp(position.x, worldEdgeBuffer, WorldManager.WorldDimensions().x - worldEdgeBuffer);
        float newZ = Mathf.Clamp(position.z, worldEdgeBuffer, WorldManager.WorldDimensions().z - worldEdgeBuffer);

        return new Vector3(newX, position.y, newZ);
    }

    Vector3 ConstrainZoom(Vector3 position)
    {
        float newY = Mathf.Clamp(position.y, 10f, 70f);
        float newZ = Mathf.Clamp(position.z, -70f, -10f);

        return new Vector3(0, newY, newZ);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
