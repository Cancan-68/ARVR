using UnityEngine;
using Fusion;
using Unity.XR.CoreUtils;

public class NetworkPlayerSetup : NetworkBehaviour
{
    [Header("Local Components to Disable")]
    public GameObject xrOrigin; // L'objet qui contient la caméra et les controllers
    public AudioListener audioListener;

    public override void Spawned()
    {
        // HasInputAuthority est VRAI uniquement pour le joueur qui possède ce prefab
        if (Object.HasInputAuthority)
        {
            // C'est MOI : Je garde ma caméra et mes contrôles actifs
            Debug.Log("Local Player Spawned - Keeping controls active");
        }
        else
        {
            // C'est un AUTRE joueur : Je désactive sa caméra et son XR Origin sur mon écran
            Debug.Log("Remote Player Spawned - Disabling their camera and controls");

            if (xrOrigin != null) xrOrigin.SetActive(false);
            if (audioListener != null) audioListener.enabled = false;
        }
    }
}