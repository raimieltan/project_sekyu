using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using TMPro;
using Photon.Pun;

public class OpenShop : MonoBehaviour
{
    private StarterAssetsInputs starterAssetsInputs;
    public GameObject Inventory;
    public GameObject BuyPhase;
    private bool canOpenInventory = true; // flag to check if the inventory can be opened
    public TextMeshProUGUI timerText;
    private float timer = 0f;
    private PhotonView photonView;
    
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        BuyPhase.SetActive(true);
        StartCoroutine(StartTimer());
        photonView = GetComponent<PhotonView>();
    }

    IEnumerator StartTimer()
    {
        float timeRemaining = 29f;

        // Loop until the time remaining reaches 0
        while (timeRemaining > 0)
        {
            // Update the timer text UI
            timerText.text = "0:"+timeRemaining.ToString("0");

            // Wait for one second before subtracting from the time remaining
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }

        // Disable the BuyPhase UI
        BuyPhase.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime; // update the timer
        
        if (photonView.IsMine && canOpenInventory && !Inventory.activeSelf && Keyboard.current.bKey.wasPressedThisFrame)
        {
            Debug.Log("openshop");
            Inventory.SetActive(true);
        }
        else if (Inventory.activeSelf && Keyboard.current.bKey.wasPressedThisFrame)
        {
            Inventory.SetActive(false);
        }
        
        if (timer >= 29f) 
        {
            canOpenInventory = false; // set flag to false to prevent inventory from being opened
            Inventory.SetActive(false); // close inventory
        }

    }
}
