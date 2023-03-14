using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayer : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI numberOfPlayers;

    public TextMeshProUGUI tubigPlayers;

    public TextMeshProUGUI dugoPlayers;

    public TextMeshProUGUI characterNameText;

    public TextMeshProUGUI ability1Text;

    public TextMeshProUGUI ability2Text;

    public GameObject teamBanner;

    public Material team1BannerMaterial;

    public Material team2BannerMaterial;

    public GameObject errorPanel;

    public GameObject startButton;

    private Renderer bannerRenderer;

    public bool forTest;

    ExitGames.Client.Photon.Hashtable
        character = new ExitGames.Client.Photon.Hashtable();

    void Start()
    {
        // Get the current renderer component of the game object
        bannerRenderer = teamBanner.GetComponent<Renderer>();

        numberOfPlayers.text = PhotonNetwork.PlayerList.Length + "/6";
        PhotonNetwork.AutomaticallySyncScene = true;
        if (
            (int) PhotonNetwork.LocalPlayer.ActorNumber ==
            (int) PhotonNetwork.CurrentRoom.CustomProperties["room_creator"] *
            -1
        )
        {
            startButton.SetActive(true);
        }

        character["chosen"] = 0;
        character["team"] = "team1";

        UpdatePlayerList();
    }

    void Update()
    {
        numberOfPlayers.text = PhotonNetwork.PlayerList.Length + "/6";
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

void UpdatePlayerList()
{
    List<Player> players = PhotonNetwork.PlayerList.ToList();
    tubigPlayers.text = "";
    dugoPlayers.text = "";
    foreach (Player player in players)
    {
        if (player.CustomProperties["team"] != null)
        {
            string team = (string)player.CustomProperties["team"];
            string nickname = player.NickName.Substring(0, Mathf.Min(player.NickName.Length, 2)).ToUpper();
            if (team == "team1")
            {
                tubigPlayers.text += nickname + "   ";
            }
            else if (team == "team2")
            {
                dugoPlayers.text += nickname + "   ";
            }
        }
    }
}

    public void ChooseTeam1()
    {
        tubigPlayers.text = "";
        dugoPlayers.text = "";
        character["team"] = "team1";
        bannerRenderer.material = team1BannerMaterial;
        PhotonNetwork.SetPlayerCustomProperties (character);
        UpdatePlayerList();
    }

    public void ChooseTeam2()
    {
        tubigPlayers.text = "";
        dugoPlayers.text = "";
        character["team"] = "team2";
        bannerRenderer.material = team2BannerMaterial;
        PhotonNetwork.SetPlayerCustomProperties (character);
        UpdatePlayerList();
    }

    public void ChooseTikbalang()
    {
        character["chosen"] = 0;

        characterNameText.text = "TIKBALANG";
        ability1Text.text = "KIDLAT";
        ability2Text.text = "BURNING RAGE";

        PhotonNetwork.SetPlayerCustomProperties (character);
    }

    public void ChooseMangkukulam()
    {
        character["chosen"] = 1;

        characterNameText.text = "MANGKUKULAM";
        ability1Text.text = "BANGON";
        ability2Text.text = "MOMO";

        PhotonNetwork.SetPlayerCustomProperties (character);
    }

    public void ChooseNuno()
    {
        character["chosen"] = 2;

        characterNameText.text = "NUNO";
        ability1Text.text = "TAKIPSILIM";
        ability2Text.text = "MALIKMATA";

        PhotonNetwork.SetPlayerCustomProperties (character);
    }

    public void ChooseEncanto()
    {
        character["chosen"] = 3;

        characterNameText.text = "DIWATA";
        ability1Text.text = "LUNAS";
        ability2Text.text = "HYPNOSIS";

        PhotonNetwork.SetPlayerCustomProperties (character);
    }

    public void StartGame()
    {
        // Check if both teams have players
        int team1Count = 0;
        int team2Count = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties["team"] != null)
            {
                if ((string) player.CustomProperties["team"] == "team1")
                {
                    team1Count++;
                }
                else if ((string) player.CustomProperties["team"] == "team2")
                {
                    team2Count++;
                }
            }
        }
        if (!forTest && (team1Count == 0 || team2Count == 0))
        {
            errorPanel.SetActive(true);
            StartCoroutine(HideErrorMessage());
            return;
        }

        // Start the game
        PhotonNetwork.SetPlayerCustomProperties (character);
        ExitGames.Client.Photon.Hashtable currentProperties =
            PhotonNetwork.CurrentRoom.CustomProperties;
        currentProperties.Add("WinningTeamID", "");
        PhotonNetwork.CurrentRoom.SetCustomProperties (currentProperties);
        PhotonNetwork.LoadLevel("Game");
    }

    private IEnumerator HideErrorMessage()
    {
        yield return new WaitForSeconds(3f);
        errorPanel.SetActive(false);
    }
}