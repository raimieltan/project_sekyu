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
    [SerializeField] GameObject drawUI;


    void Start()
    {
        Cursor.visible = true;
        ExitGames.Client.Photon.Hashtable currentProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        score1.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["Team_1_score"].ToString();
        score2.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["Team_2_score"].ToString();
        if((int)PhotonNetwork.CurrentRoom.CustomProperties["Team_1_score"] == (int)PhotonNetwork.CurrentRoom.CustomProperties["Team_2_score"] && (int)currentProperties["game_rounds"] >= 5)
        {
            AudioManager.instance.PlayDefeatSound();
            drawUI.SetActive(true);
        }
        else if((string)PhotonNetwork.CurrentRoom.CustomProperties["WinningTeamID"] == (string)PhotonNetwork.LocalPlayer.CustomProperties["team"]) {
            AudioManager.instance.PlayVictorySound();
            victoryUI.SetActive(true);
            defeatUI.SetActive(false);
            drawUI.SetActive(false);
        }
        else if((string)PhotonNetwork.CurrentRoom.CustomProperties["WinningTeamID"] != (string)PhotonNetwork.LocalPlayer.CustomProperties["team"]){
            AudioManager.instance.PlayDefeatSound();
            defeatUI.SetActive(true);
            victoryUI.SetActive(false);
            drawUI.SetActive(false);
        }
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    public void onclickClose () {
        PhotonNetwork.LoadLevel("Lobby");
    }
}
