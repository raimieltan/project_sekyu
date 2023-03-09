using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class DisplayPlayer : MonoBehaviourPunCallbacks
{
 
    public TextMeshProUGUI numberOfPlayers;
    public GameObject startButton;
    ExitGames.Client.Photon.Hashtable character = new ExitGames.Client.Photon.Hashtable();

    void Start () {
        numberOfPlayers.text = PhotonNetwork.PlayerList.Length + "/6";
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("________Actor");
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
        Debug.Log("________Room");
        Debug.Log((int)PhotonNetwork.CurrentRoom.CustomProperties["room_creator"] * -1);
        if((int)PhotonNetwork.LocalPlayer.ActorNumber == (int)PhotonNetwork.CurrentRoom.CustomProperties["room_creator"] * -1) {
            startButton.SetActive(true);
        }
    }
    void Update()
    {
        numberOfPlayers.text = PhotonNetwork.PlayerList.Length + "/6";
    }

    public void ChooseTeam1() {
        character["team"] = "team1";
        PhotonNetwork.SetPlayerCustomProperties(character);
    }

    public void ChooseTeam2() {
        character["team"] = "team2";
        PhotonNetwork.SetPlayerCustomProperties(character);
    }



    public void ChooseTikbalang() {
        character["chosen"] = 0;
        PhotonNetwork.SetPlayerCustomProperties(character);
    }

    public void ChooseMangkukulam() {
        character["chosen"] = 1;
      
        PhotonNetwork.SetPlayerCustomProperties(character);
     
    }

    public void ChooseNuno() {
        character["chosen"] = 2;
      
        PhotonNetwork.SetPlayerCustomProperties(character);
     
    }

    public void ChooseEncanto() {
        character["chosen"] = 3;
      
        PhotonNetwork.SetPlayerCustomProperties(character);
     
    }

    public void StartGame() {
        PhotonNetwork.SetPlayerCustomProperties(character);
        ExitGames.Client.Photon.Hashtable currentProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        currentProperties.Add("WinningTeamID", "");
        PhotonNetwork.CurrentRoom.SetCustomProperties(currentProperties);
        PhotonNetwork.LoadLevel("Game");
    }


}

