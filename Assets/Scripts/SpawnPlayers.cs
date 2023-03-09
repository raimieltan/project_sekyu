using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviour
{

    public GameObject[] playerPrefab;


    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    public float minZ;
    public float maxZ;
    Vector3 randomPosition;
    private LeaderboardManager leaderboardManager;

    private void Start() {
        Debug.Log((int)PhotonNetwork.LocalPlayer.CustomProperties["chosen"]);
        string chosenTeam = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"];
        if(chosenTeam == "team1") {
            randomPosition = new Vector3(Random.Range(-13.2f, 9f), Random.Range(5, 5), Random.Range(27f, 35f));
        }else {
            randomPosition = new Vector3(Random.Range(-13.2f, 9f), Random.Range(5, 5), Random.Range(135f, 150f));
        }
        PhotonNetwork.Instantiate(playerPrefab[(int)PhotonNetwork.LocalPlayer.CustomProperties["chosen"]].name, randomPosition, Quaternion.identity);

        leaderboardManager = GetComponent<LeaderboardManager>();
        leaderboardManager.InitializeLeaderboardData();
    }

}
