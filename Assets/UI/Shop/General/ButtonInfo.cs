using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{

    public int ItemID;
    public TextMeshProUGUI PriceText;
    public GameObject ShopManager;

    // Update is called once per frame
    void Update()
    {
        int [,] shopItems = ShopManager.GetComponent<ShopManagerScript>().shopItems;
        Debug.Log(shopItems[2,1]);
        PriceText.text = "Price: $" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
        //Debug.Log(PriceText.text = "Price: $" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString());
    }
}

// playerprefs