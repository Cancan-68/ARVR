using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public UIManager ui;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("page got");
            if (VRPlayerManager.Instance != null)
            {
                VRPlayerManager.Instance.AddPage();
            }
            else
            {
                // Fallback for non-VR testing or if manager is missing
                Debug.LogError("VRPlayerManager instance not found!");
            }
            // SC_FPSController.Instance.AddPage();
            ui.pages.Remove(this);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }
}
