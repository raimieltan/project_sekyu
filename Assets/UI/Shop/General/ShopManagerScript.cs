using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{

    public int [,] shopItems = new int[6,6];
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
    


    
    }

    void Update(){
        //Price
        shopItems[2,0] = 100;
        shopItems[2,1] = 120;
        shopItems[2,2] = 60;
        shopItems[2,3] = 50;
        shopItems[2,4] = 50;
        shopItems[2,5] = 120;
    }

    // Update is called once per frame
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            CoinsTxt.text = "Coins: " + coins.ToString();
        }
    }
}
