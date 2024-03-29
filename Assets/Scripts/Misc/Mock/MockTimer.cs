using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MockTimer : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    public TMP_Text timerText;

    public TMP_Text roundText;

    public float timeLimit = 300f;

    float currentTime;
    private bool timerIsRunning = true;
    public GameObject draw;
    

    void Start()
    {
        if (photonView.IsMine) {
            // Only the owner of the object sets the initial time
            currentTime = timeLimit;
        }
    }

    void Update()
    {
        if (photonView.IsMine) {
            roundText.text = "Round " + PhotonNetwork.CurrentRoom.CustomProperties["game_rounds"].ToString();
            if (timerIsRunning)
            {
                currentTime -= 1 * Time.deltaTime;

                int minutes = Mathf.FloorToInt(currentTime / 60F);
                int seconds = Mathf.FloorToInt(currentTime - minutes * 60);

                if (currentTime <= 0)
                {
                    seconds = 0;
                    minutes = 0;
                    timerIsRunning = false;
                    draw.SetActive(true);
                    ExitGames.Client.Photon.Hashtable currentProperties = PhotonNetwork.CurrentRoom.CustomProperties;
                    int rounds = (int)currentProperties["game_rounds"];
                    currentProperties["game_rounds"] = rounds + 1;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(currentProperties);

                    if(rounds >= 5)
                    {
                        PhotonNetwork.LoadLevel("ScoreBoard");
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;

                    }
                    else
                    {
                        PhotonNetwork.LoadLevel("Game");
                    }
                    
                }

                string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);

                timerText.text = formattedTime;

                // Send the timer value to other clients
                photonView.RPC("SyncTimer", RpcTarget.OthersBuffered, currentTime);
            }
        } else {
            // Update the timer value based on the received value from the owner
            int minutes = Mathf.FloorToInt(currentTime / 60F);
            int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
            string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = formattedTime;
        }
    }

    // This method is called by Photon to synchronize variables across networked clients
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send the timer value to other clients
            stream.SendNext(currentTime);
        }
        else if (stream.IsReading)
        {
            // Receive the timer value from the owner
            currentTime = (float)stream.ReceiveNext();
        }
    }

    // This method is called by the owner to synchronize the timer value with other clients
    [PunRPC]
    void SyncTimer(float time)
    {
        currentTime = time;
    }
}
