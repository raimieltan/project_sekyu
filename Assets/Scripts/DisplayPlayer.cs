using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class DisplayPlayer : MonoBehaviourPunCallbacks
{
 
    public TextMeshProUGUI numberOfPlayers;
    public TextMeshProUGUI tubigPlayers;
    public TextMeshProUGUI dugoPlayers;
    public GameObject startButton;
    ExitGames.Client.Photon.Hashtable character = new ExitGames.Client.Photon.Hashtable();

    void Start () {
        numberOfPlayers.text = PhotonNetwork.PlayerList.Length + "/6";
        PhotonNetwork.AutomaticallySyncScene = true;
        if((int)PhotonNetwork.LocalPlayer.ActorNumber == (int)PhotonNetwork.CurrentRoom.CustomProperties["room_creator"] * -1) {
            startButton.SetActive(true);
        }
        UpdatePlayerList();
    }
    void Update()
    {
        numberOfPlayers.text = PhotonNetwork.PlayerList.Length + "/6";
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    void UpdatePlayerList()
    {
        List<Player> players = PhotonNetwork.PlayerList.ToList();
        tubigPlayers.text = "";
        dugoPlayers.text = "";
        foreach (Player player in players)
        {
            if(player.CustomProperties["team"] != null) {
               
                if((string)player.CustomProperties["team"] == "team1") {
                 
                    tubigPlayers.text += player.NickName + "\n";
                } else if((string)player.CustomProperties["team"] == "team2") {
                    dugoPlayers.text += player.NickName + "\n";
                }
                
            }
        
        }
        
    }

    public void ChooseTeam1() {
        tubigPlayers.text = "";
        dugoPlayers.text = "";
        character["team"] = "team1";
        PhotonNetwork.SetPlayerCustomProperties(character);
        UpdatePlayerList();
    }

    public void ChooseTeam2() {
        tubigPlayers.text = "";
        dugoPlayers.text = "";
        Debug.Log("Clicked");
        character["team"] = "team2";
        PhotonNetwork.SetPlayerCustomProperties(character);
        UpdatePlayerList();
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

