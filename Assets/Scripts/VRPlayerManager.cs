using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerManager : MonoBehaviour
{
    private static VRPlayerManager _instance;
    public static VRPlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("VRPlayerManager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [Header("Locomotion Settings")]
    public int _pages = 0; // Restored variable
    public UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement.ContinuousMoveProvider moveProvider;
    public UnityEngine.InputSystem.InputActionProperty sprintAction;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaRegenRate = 15f;
    public float staminaDrainRate = 20f;
    [SerializeField] private float currentStamina;
    private bool isRunning = false;

    public void AddPage()
    {
        _pages++;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateObjectiveText();
        }
    }

    public int getPageNumber()
    {
        return _pages;
    }

    public float getStamina()
    {
        return currentStamina;
    }

    public bool getCanRun()
    {
        return currentStamina > 0;
    }

    void Start()
    {
        _pages = 0;
        currentStamina = maxStamina;
    }

    void Update()
    {
        HandleStamina();
    }

    void HandleStamina()
    {
        // Check if Sprint button (Left Trigger) is pressed
        bool isSprintPressed = sprintAction.action != null && sprintAction.action.ReadValue<float>() > 0.1f;

        // Determine if we are actually running (button pressed + moving + stamina available)
        // Note: We assume we are moving if speed > 0, but ideally we'd check actual velocity. 
        // For simplicity, we just check if sprint is held and we have stamina.
        
        if (isSprintPressed && currentStamina > 0)
        {
            isRunning = true;
            currentStamina -= staminaDrainRate * Time.deltaTime;
            
            if (moveProvider != null)
            {
                moveProvider.moveSpeed = runSpeed;
            }
        }
        else
        {
            isRunning = false;
            currentStamina += staminaRegenRate * Time.deltaTime;
            
            if (moveProvider != null)
            {
                moveProvider.moveSpeed = walkSpeed;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }
}
