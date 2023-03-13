using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using TMPro;

// public enum TeamNumber
// {
//     TEAM1,
//     TEAM2
// }
public class MapAreaCollider : MonoBehaviour
{
    private List<GameObject> playersInside = new List<GameObject>();
    // private bool playerInside = false;
    private float progress;
    private ThirdPersonController player;
    private Animator animator;
    public Team team;
    private Slider progressImage;
    private bool gameWon = false;
    private StarterAssetsInputs _input;
    [SerializeField] GameObject mapAreaCapturingUI;
    [SerializeField] GameObject victoryUI;
    public Team baseTeam;
    public bool damageTaken;
    private PlayerAreaCapture playerAreaCapture;

    void Start() {

    }

    void Update()
    {
        // CaptureArea();

        // if(_input.captureBase) {
        //     Debug.Log(damageTaken);
        // }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<TeamTag>(out var teamTag))
        {
            if (teamTag.team == baseTeam)
            {
                playerAreaCapture = collider.GetComponent<PlayerAreaCapture>();

                if(playerAreaCapture != null){
                playerAreaCapture.playerInside = true;
                // _input = collider.GetComponent<StarterAssetsInputs>();
                // player = collider.GetComponent<ThirdPersonController>();
                // animator = collider.GetComponent<Animator>();
                // playerAreaCapture.playerInside = true;
                // progressImage = mapAreaCapturingUI.GetComponentInChildren<Slider>();
                // damageTaken = collider.GetComponent<hit>().damageTaken;
                // playerAreaCapture = collider.GetComponent<PlayerAreaCapture>();
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<TeamTag>(out var teamTag))
        {
            if (teamTag.team == baseTeam)
            {
                playerAreaCapture.playerInside = false;
            }
        }
    }

    // void CaptureArea()
    // {
    //     if (_input.captureBase && playerInside && !gameWon)
    //     {
    //         if(damageTaken) {
    //             StartCoroutine(EndDamageTaken());
    //         } else if (!damageTaken) {
    //             captureProcess();
    //         }

    //         if (progressImage.value == 1)
    //         {
    //             victoryUI.gameObject.SetActive(true);
    //             gameWon = true;
    //             Hide();
    //         }
    //     }
    //     else
    //     {
    //         Hide();
    //         animator.SetBool("CaptureArea", false);
    //         player.MoveSpeed = 2f;
    //         player.SprintSpeed = 5.335f;
    //         progress = 0;
    //     }
    // }

    // private void Show()
    // {
    //     mapAreaCapturingUI.gameObject.SetActive(true);
    // }

    // private void Hide()
    // {
    //     if (player != null)
    //     {
    //         mapAreaCapturingUI.gameObject.SetActive(false);
    //     }
    // }

    // private void captureProcess() {
    //     Show();
    //     float progressSpeed = 1f;
    //     progress += progressSpeed * Time.deltaTime;
    //     progressImage.value = progress / 5;
    //     player.MoveSpeed = 0f;
    //     player.SprintSpeed = 0f;
    //     animator.SetBool("CaptureArea", playerInside);
    // }

    // IEnumerator EndDamageTaken()
    // {
    //     yield return new WaitForSeconds(10.0f);
    //     damageTaken = false;
    // }
}
