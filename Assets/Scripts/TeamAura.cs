using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamAura : MonoBehaviourPunCallbacks
{
    public ParticleSystem particleSystem;
    public Color team1Color = Color.blue;
    public Color team2Color = Color.red;
    public PhotonView photonView;
    public bool enabled;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
               photonView.RPC("activateAura", RpcTarget.All);
            
        }
    }

    [PunRPC]
    void activateAura() {
        
        if (photonView.IsMine)
        {
            if((string)photonView.Owner.CustomProperties["team"] == "team1") {
                particleSystem.startColor = Color.blue;
                
            } else {
            
                particleSystem.startColor = Color.red;
            
            }
            
        }

     
    }


}
