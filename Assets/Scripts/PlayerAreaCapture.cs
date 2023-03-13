using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class PlayerAreaCapture : MonoBehaviour
{
    public bool playerInside = false;
    private float progress;
    private ThirdPersonController player;
    private Animator animator;
    public Team team;
    private Slider progressImage;
    private bool gameWon = false;
    private StarterAssetsInputs _input;
    [SerializeField] GameObject mapAreaCapturingUI;
    [SerializeField] GameObject victoryUI;
    [SerializeField] GameObject defeatUI;
    [SerializeField] GameObject captureBaseUI;
    public Team baseTeam;
    public bool damageTaken;
    private Team tagPlayer;
    private Team winningTeamID;
    public PhotonView view;
    public GameObject pressF;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<ThirdPersonController>();
        _input = GetComponent<StarterAssetsInputs>();
        tagPlayer = GetComponent<TeamTag>().team;
        progressImage = mapAreaCapturingUI.GetComponentInChildren<Slider>();

    }

    void Update()
    {
        CaptureArea();
        damageTaken = GetComponent<hit>().damageTaken;

        if(view.IsMine && playerInside && !_input.captureBase && !gameWon)
        {
            pressF.SetActive(true);
        }
        else
        {
            pressF.SetActive(false);
        }

    }

    void CaptureArea()
    {
        if (_input.captureBase && playerInside && !gameWon && !damageTaken)
        {
            CaptureProcess();

            if (progressImage.value == 1)
            {


            ExitGames.Client.Photon.Hashtable currentProperties = PhotonNetwork.CurrentRoom.CustomProperties;

            string winningTeamName = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"];


            currentProperties["WinningTeamID"] = winningTeamName;
            PhotonNetwork.CurrentRoom.SetCustomProperties(currentProperties);
            PhotonNetwork.CurrentRoom.CustomProperties["WinningTeamID"] = winningTeamName;


                view.RPC("activateCanvas", RpcTarget.All);

                gameWon = true;
                Hide();
            }
        }
        else
        {
            Hide();
            animator.SetBool("CaptureArea", false);
            player.MoveSpeed = 2f;
            player.SprintSpeed = 5.335f;
            progress = 0;
        }
    }
    [PunRPC]
    void activateCanvas() {

        ExitGames.Client.Photon.Hashtable currentProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        string winningTeamName = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"];
        if((string)PhotonNetwork.CurrentRoom.CustomProperties["WinningTeamID"] == (string)PhotonNetwork.LocalPlayer.CustomProperties["team"]) {

            victoryUI.SetActive(true);

        } else {

            defeatUI.SetActive(true);

        }

        // currentProperties.Add("Team_1_score", 0);
        // currentProperties.Add("Team_2_score", 0);
     if((string)PhotonNetwork.CurrentRoom.CustomProperties["WinningTeamID"] == "team1") {
        int currentScore = (int)PhotonNetwork.CurrentRoom.CustomProperties["Team_1_score"];
        currentProperties["Team_1_score"] = currentScore + 1;

     }
     else {
        int currentScore = (int)PhotonNetwork.CurrentRoom.CustomProperties["Team_2_score"];
        currentProperties["Team_2_score"] = currentScore + 1;
     }

     int rounds = (int)currentProperties["game_rounds"];
     currentProperties["game_rounds"] = rounds + 1;
     PhotonNetwork.CurrentRoom.SetCustomProperties(currentProperties);
      StartCoroutine(DelayedFunction(5f));


    }


IEnumerator DelayedFunction(float delayTime)
{
    yield return new WaitForSeconds(delayTime);

    int team1Score = (int)PhotonNetwork.CurrentRoom.CustomProperties["Team_1_score"];
    int team2Score = (int)PhotonNetwork.CurrentRoom.CustomProperties["Team_2_score"];
    int totalRounds = team1Score + team2Score;

    if (team1Score >= 3 || team2Score >= 3 || totalRounds >= 5)
    {
        PhotonNetwork.LoadLevel("ScoreBoard");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

    }
    else
    {
        PhotonNetwork.LoadLevel("Game");
    }
}


    private void Show()
    {
        mapAreaCapturingUI.gameObject.SetActive(true);
    }

    private void Hide()
    {
        mapAreaCapturingUI.gameObject.SetActive(false);
        pressF.SetActive(false);
    }

    private void CaptureProcess() {
        if(!damageTaken) {
            Show();
            float progressSpeed = 1f;
            progress += progressSpeed * Time.deltaTime;
            progressImage.value = progress / 5;
            player.MoveSpeed = 0f;
            player.SprintSpeed = 0f;
            animator.SetBool("CaptureArea", playerInside);
        }
    }

}
