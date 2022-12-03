using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class OpenShop : MonoBehaviour
{
    public GameObject Inventory;
    //private StarterAssetsInputs starterAssetsInputs;
    // Start is called before the first frame update
    void Start()
    {
        // starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!Inventory.activeSelf & Input.GetKeyDown(KeyCode.B)) 
        {
            Debug.Log("openshop");
            Inventory.SetActive(true);

        }
        else if(Inventory.activeSelf & Input.GetKeyDown(KeyCode.B))
        {
            Inventory.SetActive(false);
        }
       

    }
}
