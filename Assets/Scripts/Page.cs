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
        if (other.tag == "Player" && (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E)))
        {
            Debug.Log("page got");
            SC_FPSController.Instance.AddPage();
            ui.pages.Remove(this);
            Destroy(this.gameObject);
            UIManager.Instance.DisablePressE();
        }
        else if (other.tag == "Player")
        {
            UIManager.Instance.EnablePressE();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.DisablePressE();
        }
    }
}
