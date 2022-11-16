using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{

    public int [,] shopItems = new int[8,8];
    public float coins;
    public TextMeshProUGUI CoinsTxt;

    void Start()
    {
        CoinsTxt.text = "Coins: " + coins.ToString();

        //ID's
        shopItems[1,0] = 0;
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;
        shopItems[1,5] = 5;
        shopItems[1,6] = 6;
        shopItems[1,7] = 7;

        //Price
        shopItems[2,0] = 100;
        shopItems[2,1] = 120;
        shopItems[2,2] = 60;
        shopItems[2,3] = 50;
        shopItems[2,4] = 50;
        shopItems[2,5] = 120;
        shopItems[2,6] = 100;
        shopItems[2,7] = 80;

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

        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            CoinsTxt.text = "Coins: " + coins.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityText.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
            
        }
    }
}
