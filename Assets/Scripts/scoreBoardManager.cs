using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;
using UnityEngine.SceneManagement;

public class scoreBoardManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    [SerializeField] GameObject victoryUI;
    [SerializeField] GameObject defeatUI;

    void Start()
    {
        score1.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["Team_1_score"].ToString();
        score2.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["Team_2_score"].ToString();
        if(PhotonNetwork.CurrentRoom.CustomProperties["WinningTeamID"] == PhotonNetwork.LocalPlayer.CustomProperties["team"] as string) {
            AudioManager.instance.PlayVictorySound();
            victoryUI.SetActive(true);
            
        } else {
            AudioManager.instance.PlayDefeatSound();
            defeatUI.SetActive(true);
          
        }
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    public void onclickClose () {
        PhotonNetwork.LoadLevel("Lobby");
       
    }
}
