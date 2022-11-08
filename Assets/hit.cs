using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public float health;


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Weapon") {

            Damage();
        }

    }

    private void Damage() {
        health -= 50;
        if(health <= 0) {
            Destroy(this.gameObject);
        }
        Debug.Log(health);
    }
}
