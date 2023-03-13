using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Photon.Pun;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    [SerializeField] private Transform entryTeam2Template;
    [SerializeField] private Transform entryTeam2Container;
    private List<PlayerScoreEntry> playerScoreEntryList;
    private List<PlayerScoreEntry> playerScoreTeam2EntryList;
    private List<Transform> playerScoreTransformList;
    private List<Transform> playerScoreTeam2TransformList;
    private List<Photon.Realtime.Player> team1Players;
    private List<Photon.Realtime.Player> team2Players;

    void Start()
    {
        entryTemplate.gameObject.SetActive(false);
        entryTeam2Template.gameObject.SetActive(false);
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        team1Players = new List<Photon.Realtime.Player>();
        team2Players = new List<Photon.Realtime.Player>();
        foreach (Photon.Realtime.Player player in players)
        {
            if (player.CustomProperties.ContainsKey("team") && player.CustomProperties["team"].ToString() == "team1")
            {
                team1Players.Add(player);
            } else if(player.CustomProperties.ContainsKey("team") && player.CustomProperties["team"].ToString() == "team2") {
                team2Players.Add(player);
            }
        }

        if(team1Players.Count == 3)
        {
            entryTeam2Container.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -111f);
        } else if(team1Players.Count == 2) {
            entryTeam2Container.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -75f);
        }

        playerScoreEntryList = new List<PlayerScoreEntry>();
        foreach (Photon.Realtime.Player player in team1Players)
        {
            PlayerScoreEntry playerScoreEntry = new PlayerScoreEntry {
                name = player.NickName,
                score = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).currentScore,
                kills = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).killCount,
                deaths = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).deathCount,
                streak = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).killStreak,
            };
            playerScoreEntryList.Add(playerScoreEntry);
        }

        playerScoreTransformList = new List<Transform>();
        foreach(PlayerScoreEntry playerScoreEntry in playerScoreEntryList)
        {
            CreateScoreboardEntryTransform(playerScoreEntry, entryContainer, playerScoreTransformList, entryTemplate);
        }

        playerScoreTeam2EntryList = new List<PlayerScoreEntry>();
        foreach (Photon.Realtime.Player player in team2Players)
        {
            PlayerScoreEntry playerScoreEntry = new PlayerScoreEntry {
                name = player.NickName,
                score = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).currentScore,
                kills = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).killCount,
                deaths = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).deathCount,
                streak = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).killStreak,
            };
            playerScoreTeam2EntryList.Add(playerScoreEntry);
        }

        playerScoreTeam2TransformList = new List<Transform>();
        foreach(PlayerScoreEntry playerScoreEntry in playerScoreTeam2EntryList)
        {
            CreateScoreboardEntryTransform(playerScoreEntry, entryTeam2Container, playerScoreTeam2TransformList, entryTeam2Template);
        }
    }

    private void GetPlayersByTeam(string team, List<Photon.Realtime.Player> players)
    {
        Photon.Realtime.Player[] playersList = PhotonNetwork.PlayerList;

        foreach (Photon.Realtime.Player player in playersList)
        {
            if (player.CustomProperties.ContainsKey("team") && player.CustomProperties[team].ToString() == "team1")
            {
                players.Add(player);
            }
        }
    }

    private void CreateScoreboardEntryTransform(PlayerScoreEntry playerScoreEntry, Transform container, List<Transform> transformList, Transform entryTemplateNeutral)
    {
        float templateHeight = 35f;
        Transform entryTransform = Instantiate(entryTemplateNeutral, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0f, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        entryTransform.Find("scoreText").GetComponent<Text>().text = playerScoreEntry.score.ToString();
        entryTransform.Find("nameText").GetComponent<Text>().text = playerScoreEntry.name;
        entryTransform.Find("killsText").GetComponent<Text>().text = playerScoreEntry.kills.ToString();
        entryTransform.Find("deathsText").GetComponent<Text>().text = playerScoreEntry.deaths.ToString();
        entryTransform.Find("streakText").GetComponent<Text>().text = playerScoreEntry.streak.ToString();

        transformList.Add(entryTransform);
    }

    public void UpdateScoreboard()
    {
        foreach (Transform t in playerScoreTransformList)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in playerScoreTeam2TransformList)
        {
            Destroy(t.gameObject);
        }

        playerScoreTeam2TransformList.Clear();
        playerScoreTeam2EntryList.Clear();
        playerScoreTransformList.Clear();
        playerScoreEntryList.Clear();

        foreach (Photon.Realtime.Player player in team1Players)
        {
            PlayerScoreEntry playerScoreEntry = new PlayerScoreEntry {
                name = player.NickName,
                score = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).currentScore,
                kills = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).killCount,
                deaths = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).deathCount,
                streak = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).killStreak,
            };
            playerScoreEntryList.Add(playerScoreEntry);
        }

        foreach(PlayerScoreEntry playerScoreEntry in playerScoreEntryList)
        {
            CreateScoreboardEntryTransform(playerScoreEntry, entryContainer, playerScoreTransformList, entryTemplate);
        }

        foreach (Photon.Realtime.Player player in team2Players)
        {
            PlayerScoreEntry playerScoreEntry = new PlayerScoreEntry {
                name = player.NickName,
                score = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).currentScore,
                kills = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).killCount,
                deaths = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).deathCount,
                streak = LeaderboardManager.manager.GetPlayerLeaderboardData(player.ActorNumber).killStreak,
            };
            playerScoreTeam2EntryList.Add(playerScoreEntry);
        }

        foreach(PlayerScoreEntry playerScoreEntry in playerScoreTeam2EntryList)
        {
            CreateScoreboardEntryTransform(playerScoreEntry, entryTeam2Container, playerScoreTeam2TransformList, entryTeam2Template);
        }
    }

    private class PlayerScoreEntry
    {
        public string name;
        public float score;
        public float kills;
        public float deaths;
        public float streak;
    }
}

