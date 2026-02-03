using UnityEngine;
using Photon.Pun;
using System.IO;

public class NetworkPlayerManager : MonoBehaviourPunCallbacks
{
    [Header("Spawn Settings")]
    public string playerPrefabName = "XRNetwork"; // Must be in Resources folder
    public string slendermanPrefabName = "SlenderNetwork";
    public string pagePrefabName = "PageNetwork";

    [Header("Spawn Points")]
    public Transform[] playerSpawnPoints;
    public Transform slendermanSpawnPoint;
    public Transform[] pageSpawnPoints;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();

            // Only the Master Client (Host) should spawn the monster and items
            if (PhotonNetwork.IsMasterClient)
            {
                SpawnGameElements();
            }
        }
    }

    void SpawnPlayer()
    {
        int randomPoint = Random.Range(0, playerSpawnPoints.Length);
        Transform spawn = playerSpawnPoints[randomPoint];

        // Instantiate the VR player across the network
        PhotonNetwork.Instantiate(playerPrefabName, spawn.position, spawn.rotation);
    }

    void SpawnGameElements()
    {
        // Spawn Slenderman
        PhotonNetwork.Instantiate(slendermanPrefabName, slendermanSpawnPoint.position, slendermanSpawnPoint.rotation);

        // Spawn 10 Pages at random locations
        for (int i = 0; i < 10; i++)
        {
            Transform pageSpawn = pageSpawnPoints[i % pageSpawnPoints.Length];
            PhotonNetwork.Instantiate(pagePrefabName, pageSpawn.position, pageSpawn.rotation);
        }
    }
}