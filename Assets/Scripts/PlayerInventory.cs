using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;

public class PlayerInventory : MonoBehaviour
{
    private StarterAssetsInputs starterAssetsInputs;
    public float itemCooldown = 2;
    public float nextFlashbangTime = 0;
    public float nextSmokeTime = 0;
    public float nextExplosiveTrapTime = 0;
    public float nextPoisonTrapTime = 0;
    public float nextSlowTrapTime = 0;
    public delegate void FlashbangFired();
    public event FlashbangFired OnFlashbangFired;
    public delegate void SmokeFired();
    public event SmokeFired OnSmokeFired;
    public delegate void PlaceExplosiveTrapFired();
    public event PlaceExplosiveTrapFired OnPlaceExplosiveTrap;
    public delegate void PlacePoisonTrapFired();
    public event PlacePoisonTrapFired OnPlacePoisonTrap;
    public delegate void PlaceSlowTrapFired();
    public event PlaceSlowTrapFired OnPlaceSlowTrap;
    public PhotonView view;
    public GameObject smoke;
    public GameObject flashbang;
    public GameObject explosiveTrap;
    public GameObject poisonTrap;
    public GameObject slowTrap;
    public GameObject flashbangItem;
    public GameObject smokeItem;
    public GameObject explosiveTrapItem;
    public GameObject poisonTrapItem;
    public GameObject slowTrapItem;
    public GameObject flashbangItemBox;
    public GameObject smokeItemBox;
    public GameObject explosiveItemBox;
    public GameObject poisonTrapItemBox;
    public GameObject slowTrapItemBox;
    private Team trapTeam;


    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            if (starterAssetsInputs.throwFlashbang && flashbangItem.activeSelf && Time.time > nextFlashbangTime)
            {
                nextFlashbangTime = Time.time + itemCooldown;
                // Get the player's position and forward direction
                Vector3 playerPosition = transform.position;
                Vector3 playerForward = transform.forward;

                // Calculate the position to spawn the object
                float spawnDistance = 1f;
                float spawnHeight = 1f;
                Vector3 spawnPosition = playerPosition + playerForward * spawnDistance;
                RaycastHit hit;

                if (Physics.Raycast(spawnPosition, Vector3.down, out hit))
                {
                    spawnPosition.y = hit.point.y + spawnHeight;
                }

                // Instantiate the object and set its properties
                GameObject flashbangObject = PhotonNetwork.Instantiate(flashbang.name, spawnPosition, Quaternion.identity);
                FlashBang flashBang = flashbangObject.GetComponent<FlashBang>();

                if (flashBang != null)
                {
                    flashBang.SetThrowDirection(playerForward);
                }

                // Trigger the OnFlashbangFired event
                OnFlashbangFired();
            }

            if (starterAssetsInputs.throwSmoke && smokeItem.activeSelf && Time.time > nextSmokeTime)
            {
                nextSmokeTime = Time.time + itemCooldown;
                PhotonNetwork.Instantiate(smoke.name, transform.position, Quaternion.identity);
                OnSmokeFired();
            }

        if (starterAssetsInputs.placeExplosiveTrap && explosiveTrapItem.activeSelf && Time.time > nextExplosiveTrapTime)
        {
                nextExplosiveTrapTime = Time.time + itemCooldown;
            if (PhotonNetwork.LocalPlayer.CustomProperties["team"].ToString() == "team1")
            {
                GameObject instantiatedGameObject = PhotonNetwork.Instantiate(explosiveTrap.name, transform.position, Quaternion.identity, 0, new object[] { "team", "team1" });
            }
            else
            {
                GameObject instantiatedGameObject = PhotonNetwork.Instantiate(explosiveTrap.name, transform.position, Quaternion.identity, 0, new object[] { "team", "team2" });
            }
                OnPlaceExplosiveTrap();
        }


        if (starterAssetsInputs.placePoisonTrap && poisonTrapItem.activeSelf && Time.time > nextPoisonTrapTime)
        {
            nextPoisonTrapTime = Time.time + itemCooldown;
        if (PhotonNetwork.LocalPlayer.CustomProperties["team"].ToString() == "team1")
        {
            GameObject instantiatedGameObject = PhotonNetwork.Instantiate(poisonTrap.name, transform.position, Quaternion.identity, 0, new object[] { "team", "team1" });
        }
        else
        {
            GameObject instantiatedGameObject = PhotonNetwork.Instantiate(poisonTrap.name, transform.position, Quaternion.identity, 0, new object[] { "team", "team2" });
        }
            OnPlacePoisonTrap();
        }

        if (starterAssetsInputs.placeSlowTrap && slowTrapItem.activeSelf && Time.time > nextSlowTrapTime)
        {
            nextSlowTrapTime = Time.time + itemCooldown;
        if (PhotonNetwork.LocalPlayer.CustomProperties["team"].ToString() == "team1")
        {
            GameObject instantiatedGameObject = PhotonNetwork.Instantiate(slowTrap.name, transform.position, Quaternion.identity, 0, new object[] { "team", "team1" });
        }
        else
        {
            GameObject instantiatedGameObject = PhotonNetwork.Instantiate(slowTrap.name, transform.position, Quaternion.identity, 0, new object[] { "team", "team2" });
        }
            OnPlaceSlowTrap();
        }

            if (!flashbangItem.activeSelf)
            {
                flashbangItemBox.SetActive(true);
            }
            else
            {
                flashbangItemBox.SetActive(false);
            }

            if (!smokeItem.activeSelf)
            {
                smokeItemBox.SetActive(true);
            }
            else
            {
                smokeItemBox.SetActive(false);
            }

            if (!explosiveTrapItem.activeSelf)
            {
                explosiveItemBox.SetActive(true);
            }
            else
            {
                explosiveItemBox.SetActive(false);
            }

            if (!poisonTrapItem.activeSelf)
            {
                poisonTrapItemBox.SetActive(true);
            }
            else
            {
                poisonTrapItemBox.SetActive(false);
            }

            if (!slowTrapItem.activeSelf)
            {
                slowTrapItemBox.SetActive(true);
            }
            else
            {
                slowTrapItemBox.SetActive(false);
            }

        }
    }
}
