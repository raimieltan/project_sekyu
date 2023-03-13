using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] private Transform pfSmokeProj;
    [SerializeField] private Transform spawnSmokePos;

 
    public float timer;

    void Update()
    {
        timer -= 1.0F * Time.deltaTime;
      
        if (Input.GetKeyDown("x") && timer <= 0)
            {
                Instantiate(pfSmokeProj, spawnSmokePos.position, spawnSmokePos.rotation);
                timer = 5;
                
            }   
    }
}
