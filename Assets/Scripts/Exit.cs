using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public UIManager ui;
    public global global;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.Find("FPSPlayer").GetComponent<SC_FPSController>()._pages == 10)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            ui.NoExitText.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ui.NoExitText.enabled = false;
    }
}
