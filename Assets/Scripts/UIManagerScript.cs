using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManagerScript : MonoBehaviour
{

    public static UIManagerScript instance;
    public GameObject victory;
    public GameObject defeat;

void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(gameObject);
    }
}




    public void SetCanvasActive(bool isActive)
    {
        victory.SetActive(true);
    }


    public void SetCanvasDefeatActive(bool isActive)
    {
        defeat.SetActive(true);
    }

    


}
