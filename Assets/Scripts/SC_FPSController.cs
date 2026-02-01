using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    // singleton code
    private static SC_FPSController _instance;
    public static SC_FPSController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Player is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    // global variable
    [SerializeField] public int _pages;
    public void AddPage()
    {
        _pages++;
    }

    public int getPageNumber()
    {
        return _pages;
    }
    
    public float getStamina()
    {
        return stamina;
    }

    public bool getCanRun()
    {
        return canRun;
    }
    // controls
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    private float stamina = 100.0f;
    private bool canRun = true;

    CharacterController characterController;
    public Light flashlight;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        _pages = 0;

        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        UIManager.Instance.UpdateObjectiveText();

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && canRun;
        if (isRunning)
        {
            stamina -= 20.0f * Time.deltaTime;
            if (stamina <= 0)
            {
                canRun = false;
            }
        }
        else if (stamina < 100.0f)
        {
            stamina += (10.0f - (!canRun ? 5.0f : 0.0f)) * Time.deltaTime;
        }
        else
        {
            canRun = true;
        }
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            flashlight.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Left Click interaction
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 2.0f);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
}