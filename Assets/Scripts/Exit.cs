using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
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
        if (SC_FPSController.Instance._pages == 10)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            UIManager.Instance.NoExitText.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.NoExitText.enabled = false;
    }
}
