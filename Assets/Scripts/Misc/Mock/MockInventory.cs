using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MockInventory : MonoBehaviour
{
    public float itemCooldown = 3;

    private float nextItem1Time = 0;
    private float nextItem2Time = 0;
    private float nextItem3Time = 0;


    public Image item1Image;
    public Image item2Image;
    public Image item3Image;



    void Start()
    {
        item1Image.fillAmount = 0;
    }

    void Update()
    {
        if (Time.time > nextItem1Time)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nextItem1Time = Time.time + itemCooldown;
                item1Image.fillAmount = 1;
            }
        }
        else
        {
            item1Image.fillAmount -= 1 / itemCooldown * Time.deltaTime;

            if (item1Image.fillAmount <= 0)
            {
                item1Image.fillAmount = 0;
            }
        }

        if (Time.time > nextItem2Time)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nextItem2Time = Time.time + itemCooldown;
                item2Image.fillAmount = 1;
            }
        }
        else
        {
            item2Image.fillAmount -= 1 / itemCooldown * Time.deltaTime;

            if (item2Image.fillAmount <= 0)
            {
                item2Image.fillAmount = 0;
            }
        }

        if (Time.time > nextItem3Time)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nextItem3Time = Time.time + itemCooldown;
                item3Image.fillAmount = 1;
            }
        }
        else
        {
            item3Image.fillAmount -= 1 / itemCooldown * Time.deltaTime;

            if (item3Image.fillAmount <= 0)
            {
                item3Image.fillAmount = 0;
            }
        }


    }
}
