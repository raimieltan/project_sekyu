using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnetToServer : MonoBehaviourPunCallbacks
{
    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connecting to master");
        PhotonNetwork.JoinLobby();
    }

    
    public override void OnJoinedLobby(){
        SceneManager.LoadScene("Lobby");
    }
}
