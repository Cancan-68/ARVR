using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class AutoLauncher : MonoBehaviour
{
    private NetworkRunner _runner;

    [SerializeField] private string _roomName = "FixedRoomName";
    [SerializeField] private int _gameSceneIndex = 1; // L'index de ta scène de jeu dans Build Settings

    async void Start()
    {
        // 1. Création du NetworkRunner
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // 2. Configuration du chargement de scène
        // Nécessaire pour que Photon puisse synchroniser le changement de scène
        var sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();

        // 3. Lancement automatique
        var result = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = _roomName,
            Scene = SceneRef.FromIndex(_gameSceneIndex),
            SceneManager = sceneManager
        });

        if (result.Ok)
        {
            Debug.Log($"Connecté à la salle : {_roomName}");
        }
        else
        {
            Debug.LogError($"Erreur : {result.ShutdownReason}");
        }
    }
}
