using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ShopManagerScript : MonoBehaviour
{

    public int [,] shopItems = new int[8,8];
    public float coins;
    public TextMeshProUGUI CoinsTxt;
    public int id;
    public TextMeshProUGUI FlashbangQuantityTxt;
    public TextMeshProUGUI SmokeQuantityTxt;
    public TextMeshProUGUI PoisonQuantityTxt;
    public TextMeshProUGUI SlowQuantityTxt;
    public TextMeshProUGUI ExplosiveQuantityTxt;
    public GameObject FlashBang;
    public GameObject Smoke;
    public GameObject Poison;
    public GameObject Slow;
    public GameObject Explosive;
    public Health playerHealth;
    public GameObject Buy1;
    public GameObject Buy2;
    public GameObject Buy3;
    public GameObject Buy4;
    public GameObject Buy5;
    public GameObject Buy6;
    public GameObject Buy7;
    public GameObject Buy8;
    public PhotonView view;
    int itemQntyTxt;

    void Awake()
    {
        coins = 1000;
        CoinsTxt.text = "Coins: " + coins.ToString();
        //itemQntyTxt = int.Parse(ItemQntyText.text);
        
        //ID's
        shopItems[1,0] = 0; // 100 armor 
        shopItems[1,1] = 1; // 68 armor
        shopItems[1,2] = 2; // 41 armor
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;
        shopItems[1,5] = 5;
        shopItems[1,6] = 6;
        shopItems[1,7] = 7;

        //Price
        shopItems[2,0] = 200;
        shopItems[2,1] = 150;
        shopItems[2,2] = 60;
        shopItems[2,3] = 150;
        shopItems[2,4] = 120;
        shopItems[2,5] = 120;
        shopItems[2,6] = 120;
        shopItems[2,7] = 120;

        // Quantity
        shopItems[3,0] = 0;
        shopItems[3,1] = 0;
        shopItems[3,2] = 0;
        shopItems[3,3] = 0;
        shopItems[3,4] = 0;
        shopItems[3,5] = 0;
        shopItems[3,6] = 0;
        shopItems[3,7] = 0;
    }

    // Update is called once per frame
    public void Buy()
    {
        
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        // Debug.Log("WHO IS THIS: " + ButtonRef);

        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 0) {
                playerHealth.AddArmor(100);
            }

            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 1) {
                playerHealth.AddArmor(68);
            }

            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 7) {
                playerHealth.AddArmor(41);
            }

            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            Debug.Log(shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]);
            CoinsTxt.text = "Coins: " + coins.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityText.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        } 
        
        if(coins < shopItems[2,0])
        {
            Buy1.SetActive(false);
        }

        if(coins < shopItems[2,1])
        {
            Buy2.SetActive(false);
        }

        if(coins < shopItems[2,2])
        {
            Buy3.SetActive(false);
        }

        if(coins < shopItems[2,3])
        {
            Buy4.SetActive(false);
        }

        if(coins < shopItems[2,4])
        {
            Buy5.SetActive(false);
        }

        if(coins < shopItems[2,5])
        {
            Buy6.SetActive(false);
        }

        if(coins < shopItems[2,6])
        {
            Buy7.SetActive(false);
        }
        if(coins < shopItems[2,7])
        {
            Buy8.SetActive(false);
        }
    }

    void Update()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if(Input.GetKeyDown("1"))
        {
            if(shopItems[3,3] > 0)
            {
                
                shopItems[3,3]--;
                FlashbangQuantityTxt.text = shopItems[3,3].ToString();  
            }

            if(shopItems[3,3] == 0)
            {
                FlashBang.SetActive(false);
            }
        }

        if(Input.GetKeyDown("2"))
        {
            if(shopItems[3,2] > 0)
            {   
                shopItems[3,2]--;
                SmokeQuantityTxt.text = shopItems[3,2].ToString();
            }

            if(shopItems[3,2] == 0)
            {
                Smoke.SetActive(false);
            }
        }

        if(Input.GetKeyDown("3"))
        {
            if(shopItems[3,4] > 0)
            {
                
                shopItems[3,4]--;
                ExplosiveQuantityTxt.text = shopItems[3,4].ToString();    
            }

            if(shopItems[3,4] == 0)
            {
                Explosive.SetActive(false);
            }
            
        }

        if(Input.GetKeyDown("4"))
        {
            if(shopItems[3,5] > 0)
            {
                shopItems[3,5]--;
                PoisonQuantityTxt.text = shopItems[3,5].ToString();
            }

            if(shopItems[3,5] == 0)
            {
                Poison.SetActive(false);
            }
        }

        if(Input.GetKeyDown("5"))
        {
            if(shopItems[3,6] > 0)
            {
                shopItems[3,6]--;
                SlowQuantityTxt.text = shopItems[3,6].ToString();
            }

            if(shopItems[3,6] == 0)
            {
                Slow.SetActive(false);
            }
        }

    }
}
