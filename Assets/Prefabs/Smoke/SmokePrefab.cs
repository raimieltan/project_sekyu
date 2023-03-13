using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePrefab : MonoBehaviour
{
    public float timer; 
    // Update is called once per frame
    void Update()
    {
        timer += 1.0F * Time.deltaTime;
        if(timer >= 10) {
            GameObject.Destroy(gameObject);
        }
        
    }
}
