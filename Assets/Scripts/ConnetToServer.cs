<<<<<<< HEAD
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
=======
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
>>>>>>> cd138fd7313d94b6aef7dfe6e4aa75f427263a05
