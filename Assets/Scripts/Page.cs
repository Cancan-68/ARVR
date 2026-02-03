using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public UIManager ui;
    private SC_FPSController player_class;

    // Start is called before the first frame update
    void Start()
    {
        player_class = GameObject.Find("XRNetwork(Clone)").GetComponent<SC_FPSController>();
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
            player_class.AddPage();
            ui.pages.Remove(this);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }
}
