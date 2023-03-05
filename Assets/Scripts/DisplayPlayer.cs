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

    // Start is called before the first frame update

    void Start () {
        
        numberOfPlayers.text = PhotonNetwork.PlayerList.Length + "/6";
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.AutomaticallySyncScene = true;
    
    }
    void Update()
    {
        // PhotonNetwork.CurrentRoom.Players.ForEach(x => Debug.Log(x.Key));

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
        currentProperties.Add("Team_1_score", 0);
        currentProperties.Add("Team_2_score", 0);
        currentProperties.Add("WinningTeamID", "");
        PhotonNetwork.CurrentRoom.SetCustomProperties(currentProperties);
        PhotonNetwork.LoadLevel("Game");
    }


}
