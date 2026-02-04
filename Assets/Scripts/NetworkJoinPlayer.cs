using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public NetworkObject playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        // SEUL le serveur exécute ce code
        if (Runner.IsServer)
        {
            Debug.Log($"Spawning player for: {player}");

            // On spawn l'objet, mais on donne l'Autorité d'Input au client concerné
            Runner.Spawn(playerPrefab, Vector3.up, Quaternion.identity, player);
        }
    }
}