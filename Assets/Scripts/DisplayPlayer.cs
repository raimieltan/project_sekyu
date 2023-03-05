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
    ExitGames.Client.Photon.Hashtable character = new ExitGames.Client.Photon.Hashtable();

    void Start () {
        numberOfPlayers.text = PhotonNetwork.PlayerList.Length + "/6";
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.AutomaticallySyncScene = true;
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
        PhotonNetwork.LoadLevel("Game");
    }


}
