using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class reviveOrb : MonoBehaviourPun
{
    private Rigidbody reviveOrbRigidbody;
    public float force;
    public float torque;
    public PhotonView view;

    private void Awake() {
    
        reviveOrbRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {

        if( view.IsMine == false && PhotonNetwork.IsConnected == true )
	        {
	            return;
	        }

      
        // reviveOrbRigidbody.velocity = transform.forward * speed;
        reviveOrbRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
        reviveOrbRigidbody.AddTorque(transform.right * torque);
        transform.SetParent(null);
        
    }

    // private void OnTriggerEnter (Collider other) 
    // {
    //     // Debug.Log("Hit");
    
    //     PhotonView photonView = other.gameObject.GetComponent<PhotonView>();
    //     if (photonView != null && photonView.Owner != null) 
    //     {
    //         Player player = photonView.Owner;

    //         // Debug.Log(other.gameObject.name);
            
    //         if((string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == (string)player.CustomProperties["team"]) {
    //             Debug.Log("DEAD: " + other.gameObject.GetComponent<Health>().isDead);

    //             if(other.gameObject.GetComponent<Health>().isDead) {
    //                 other.gameObject.GetComponent<Health>().RestoreHealth(100);


    //                 Debug.Log("OTHER HEALTH: " + other.gameObject.GetComponent<Health>().currentHealth);
    //                 // other.gameObject.GetComponent<Health>().isDead = false;
                    
    //                 Debug.Log("ISREVIVE: " + !other.gameObject.GetComponent<Health>().isDead);
                    
    //                 // other.gameObject.GetComponent<Health>().isRevive = true;
    //                 // other.gameObject.GetComponent<StarterAssets.ThirdPersonController>().enabled = true;
    //                 // other.gameObject.GetComponent<Health>().playerHud.SetActive(true);
                                            
    //             }       
    //         }
    //     }
    //     PhotonNetwork.Destroy(gameObject);
        
    // }

    private void OnTriggerEnter (Collider other) 
    {
        // Debug.Log("Hit");
        if (view.IsMine) {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    // [PunRPC]
    // private void Revive(Collider other) {
    //     if(other.gameObject.GetComponent<Health>().isDead) {
            
    //         Debug.Log("TRUE");
            // Animator animator = other.GetComponent<Animator>();

            // other.gameObject.GetComponent<Health>().isDead = false;
            // other.gameObject.GetComponent<StarterAssets.ThirdPersonController>().enabled = true;
            // other.gameObject.GetComponent<Health>().playerHud.SetActive(true);
            // animator.SetBool("isRevive", true);                      
    //     }
    // }
    // public void Revive(Collider other)
    // {
    //     view.RPC(nameof(RPC_Revive), RpcTarget.All, other);
    // }
}