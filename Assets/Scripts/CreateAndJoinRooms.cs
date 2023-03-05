using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameInput;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    public void CreateRoom()
    {
        string mes = "Trying to create a room";
        Debug.Log(mes);
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roompos = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 6 };
        roompos.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roompos.CustomRoomProperties.Add("offsetX", Random.Range(0f, 9999f));
        roompos.CustomRoomProperties.Add("offsetY", Random.Range(0f, 9999f));
        roompos.CustomRoomProperties.Add("Team_1_score", 0);
        roompos.CustomRoomProperties.Add("Team_2_score", 0);
        roompos.CustomRoomProperties.Add("game_rounds", 0);
        PhotonNetwork.CreateRoom("Room" + randomRoomName.ToString(), roompos);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = nameInput.text;
        
        PhotonNetwork.LoadLevel("Room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        string mes = "Create Room Failed";
        Debug.Log(mes);
        CreateRoom();
    }
}
