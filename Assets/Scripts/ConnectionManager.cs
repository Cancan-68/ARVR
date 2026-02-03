using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    public void CreateRoom()
    {
        Debug.Log(inputField);
        Debug.Log(NetworkManager.Instance);
        NetworkManager.Instance.CreateSession(inputField.text);
    }

    public void JoinRoom()
    {
        Debug.Log(inputField);
        Debug.Log(NetworkManager.Instance);
        NetworkManager.Instance.JoinSession(inputField.text);

    }
}
