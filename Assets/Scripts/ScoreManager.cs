using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    // Start is called before the first frame update

    void Start()
    {
        score1.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["Team_1_score"].ToString();
        score2.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["Team_2_score"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score1.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["Team_1_score"].ToString();
        score2.text = (string)PhotonNetwork.CurrentRoom.CustomProperties["Team_2_score"].ToString();
    }
}
