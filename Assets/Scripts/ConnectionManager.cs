using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public void CreateRoom()
    {
        Debug.Log(NetworkManager.Instance);
        NetworkManager.Instance.CreateSession("testRoom");
    }

    public void JoinRoom()
    {
        Debug.Log(NetworkManager.Instance);
        NetworkManager.Instance.JoinSession("testRoom");

    }
}
