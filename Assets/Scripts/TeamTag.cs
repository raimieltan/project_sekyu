using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public enum Team
{
    TEAM1,
    TEAM2
}
public class TeamTag : MonoBehaviour
{
    public Team team;
    public PhotonView view;

    void Awake() {
        if( view.IsMine && PhotonNetwork.IsConnected == true )
	    {
            string chosenTeam = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"];
            if(chosenTeam == "team1") {
                team = Team.TEAM1;
            }else {
                team = Team.TEAM2;
            }
	            
	    }
    }
    public bool isAlly(Team compareTo)
    {
        return compareTo == team;
    }

    public bool isEnemy(Team compareTo)
    {
        return compareTo != team;
    }
}
