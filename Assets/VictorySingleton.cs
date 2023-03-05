using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VictorySingleton : MonoBehaviour
{
    public static VictorySingleton Instance { get; private set; }

    private void Awake()
    {

        Instance=this;
    }


    [PunRPC]
    public void VictorySetActive()
    {
        gameObject.SetActive(true);
    }
}
