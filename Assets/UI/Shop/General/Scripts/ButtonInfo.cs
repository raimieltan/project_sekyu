using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{

    public int ItemID;
    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI QuantityText;
    public TextMeshProUGUI HUDQuantityText;
    public GameObject ShopManager;

    // Update is called once per frame
    void Update()
    {
        int [,] shopItems = ShopManager.GetComponent<ShopManagerScript>().shopItems;
        PriceText.text = "Price: $" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
        QuantityText.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();
        HUDQuantityText.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();

    }
}

// playerprefs