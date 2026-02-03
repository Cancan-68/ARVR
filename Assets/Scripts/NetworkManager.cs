using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;

    [Header("Room Settings")]
    [SerializeField] private byte maxPlayers = 2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ConnectToPhoton();
    }

    private void ConnectToPhoton()
    {
        if (PhotonNetwork.IsConnected)
            return;

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();

        Debug.Log("Connexion à Photon...");
    }

    // =========================
    // UI CALLS
    // =========================

    public void CreateSession(string roomName)
    {
        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogWarning("Nom de room invalide");
            return;
        }

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = maxPlayers,
            IsVisible = true,
            IsOpen = true
        };

        PhotonNetwork.CreateRoom(roomName, options);
        Debug.Log($"Création de la room : {roomName}");
    }

    public void JoinSession(string roomName)
    {
        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogWarning("Nom de room invalide");
            return;
        }

        PhotonNetwork.JoinRoom(roomName);
        Debug.Log($"Tentative de rejoindre : {roomName}");
    }

    // =========================
    // PHOTON CALLBACKS
    // =========================

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connecté au Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby rejoint");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room créée avec succès");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Room jointe : {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"Joueurs : {PhotonNetwork.CurrentRoom.PlayerCount}");

        // Ici tu peux charger la scène de jeu
        PhotonNetwork.LoadLevel("ForestSceneVR");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Join failed : {message}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Create failed : {message}");
    }
}