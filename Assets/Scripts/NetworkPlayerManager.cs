using UnityEngine;
using Fusion;
using System.Collections;
using System.Threading.Tasks; // Important pour Task

public class NetworkPlayerManager : MonoBehaviour
{
    [Header("Prefabs")]
    public NetworkObject playerPrefab;
    public NetworkObject slendermanPrefab;
    public NetworkObject pagePrefab;

    [Header("Spawn Points")]
    public Transform[] playerSpawnPoints;
    public Transform slendermanSpawnPoint;
    public Transform[] pageSpawnPoints;

    private NetworkRunner _runner;

    async void Start()
    {
        _runner = FindFirstObjectByType<NetworkRunner>();

        if (_runner == null) return;

        // Attendre que le Runner soit prêt
        while (!_runner.IsCloudReady)
        {
            await Task.Yield();
        }

        await SpawnPlayer();

        if (_runner.IsServer || _runner.IsSharedModeMasterClient)
        {
            await SpawnGameElements();
        }
    }

    async Task SpawnPlayer()
    {
        int randomPoint = Random.Range(0, playerSpawnPoints.Length);
        Transform spawn = playerSpawnPoints[randomPoint];

        // FIX: Utilisation de SpawnAsync au lieu de Spawn
        await _runner.SpawnAsync(playerPrefab, spawn.position, spawn.rotation, _runner.LocalPlayer);
        Debug.Log("Joueur spawn avec succès !");
    }

    async Task SpawnGameElements()
    {
        // On attend que le monstre soit spawn
        await _runner.SpawnAsync(slendermanPrefab, slendermanSpawnPoint.position, slendermanSpawnPoint.rotation, null);

        // On spawn les pages
        for (int i = 0; i < 10; i++)
        {
            Transform pageSpawn = pageSpawnPoints[i % pageSpawnPoints.Length];
            // Ici on ne met pas forcément de await si on veut qu'elles spawn toutes en même temps
            _runner.SpawnAsync(pagePrefab, pageSpawn.position, pageSpawn.rotation, null);
        }
        Debug.Log("Éléments de jeu spawn avec succès !");
    }
}