using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class LeaderboardManager : MonoBehaviourPunCallbacks
{
    public static LeaderboardManager manager;

    public List<KeyValuePair<int, LeaderboardData>> playerData =
        new List<KeyValuePair<int, LeaderboardData>>();

    void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        // else if (manager != this)
        // {
        //     Destroy(gameObject);
        // }
    }

    public void InitializeLeaderboardData()
    {
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        foreach (Photon.Realtime.Player player in players)
        {
            LeaderboardData leaderboardData = new LeaderboardData();

            KeyValuePair<int, LeaderboardData> playerLeaderboardData = new KeyValuePair<
                int,
                LeaderboardData
            >(player.ActorNumber, leaderboardData);

            playerData.Add(playerLeaderboardData);
        }
    }

    public void RefreshLeaderboardData()
    {
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        if (players.Length == playerData.Count)
        {
            foreach (Photon.Realtime.Player player in players)
            {
                Debug.Log(player.CustomProperties["team"].ToString());
                LeaderboardData leaderboardData = new LeaderboardData();

                if (player.CustomProperties.ContainsKey("currentScore"))
                    leaderboardData.currentScore = (float)player.CustomProperties["currentScore"];
                if (player.CustomProperties.ContainsKey("killCount"))
                    leaderboardData.killCount = (float)player.CustomProperties["killCount"];
                if (player.CustomProperties.ContainsKey("deathCount"))
                    leaderboardData.deathCount = (float)player.CustomProperties["deathCount"];
                if (player.CustomProperties.ContainsKey("killStreak"))
                    leaderboardData.killStreak = (float)player.CustomProperties["killStreak"];

                KeyValuePair<int, LeaderboardData> playerLeaderboardData = new KeyValuePair<
                    int,
                    LeaderboardData
                >(player.ActorNumber, leaderboardData);

                int playerIndex = playerData.FindIndex(
                    (playerData) => playerData.Key == player.ActorNumber
                );

                playerData[playerIndex] = playerLeaderboardData;
            }

            foreach (KeyValuePair<int, LeaderboardData> player in playerData)
            {
                print("player actor number " + player.Key);
                print("currentScore " + player.Value.currentScore);
                print("killCount " + player.Value.killCount);
                print("deathCount " + player.Value.deathCount);
                print("killStreak " + player.Value.killStreak);
            }
        }
    }

    public void SetPlayerLeaderboardData(int playerId, LeaderboardData leaderboardData)
    {
        int playerIndex = playerData.FindIndex((player) => player.Key == playerId);

        ExitGames.Client.Photon.Hashtable customProperties =
            new ExitGames.Client.Photon.Hashtable();
        customProperties["currentScore"] = leaderboardData.currentScore;
        customProperties["killCount"] = leaderboardData.killCount;
        customProperties["deathCount"] = leaderboardData.deathCount;
        customProperties["killStreak"] = leaderboardData.killStreak;

        PhotonNetwork.PlayerList
            .ToList()
            .Find((player) => player.ActorNumber == playerId)
            .SetCustomProperties(customProperties);
    }

    public LeaderboardData GetPlayerLeaderboardData(int playerId)
    {
        return playerData.Find((player) => player.Key == playerId).Value;
    }

    public override void OnPlayerPropertiesUpdate(
        Photon.Realtime.Player targetPlayer,
        ExitGames.Client.Photon.Hashtable changedProps
    )
    {
        RefreshLeaderboardData();
    }
}

