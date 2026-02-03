using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRFlashlightController : MonoBehaviour
{
    public Light flashlight;
    public InputActionProperty toggleAction;

    private bool isOn = true;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponentInChildren<Light>();
        }
        if (flashlight != null)
        {
            flashlight.enabled = true;
            flashlight.renderMode = LightRenderMode.ForcePixel; // Force "Important" mode
        }
    }

    void Update()
    {
        if (toggleAction.action != null && toggleAction.action.WasPressedThisFrame())
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        isOn = !isOn;
        if (flashlight != null)
        {
            flashlight.enabled = isOn;
        }
    }
}
