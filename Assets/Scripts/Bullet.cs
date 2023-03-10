using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPun
{
    private Rigidbody bulletRigidbody;
    public float force;
    public float torque;
    public PhotonView view;

    private void Awake() {
    
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {

        if( view.IsMine == false && PhotonNetwork.IsConnected == true )
	        {
	            return;
	        }

      
        // bulletRigidbody.velocity = transform.forward * speed;
        bulletRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
        bulletRigidbody.AddTorque(transform.right * torque);
        transform.SetParent(null);
        
    }


    private void OnTriggerEnter (Collider other) 
    {
        // Debug.Log("Hit");
        if (view.IsMine) {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}