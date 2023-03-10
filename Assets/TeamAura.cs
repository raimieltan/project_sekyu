using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamAura : MonoBehaviourPunCallbacks
{
    public ParticleSystem particleSystem;
    public Color team1Color = Color.red;
    public Color team2Color = Color.blue;
    public PhotonView photonView;
    public bool enabled;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if(enabled) {
                if ((string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == "team2")
                {
                    particleSystem.startColor = team1Color;
                }
                else if ((string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == "team1")
                {
                    particleSystem.startColor = team2Color;
                }
            }
                

            
        }
    }
}
