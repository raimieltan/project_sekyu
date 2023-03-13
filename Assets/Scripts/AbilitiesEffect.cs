using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using UnityEngine.InputSystem;
public class AbilitiesEffect : MonoBehaviour
{
    public bool isHeal;
    private Health health;
    public PhotonView view;
    public ParticleSystem healAura, stunAura;
    private PlayerInput player;
    private Animator animator;
    private CharacterController characterController;

    void Start()
    {
        player = this.gameObject.GetComponent<PlayerInput>();
        health = this.gameObject.GetComponent<Health>();
        characterController = this.gameObject.GetComponent<CharacterController>();
        healAura = this.gameObject.transform.Find("Geometry/HealingAura").GetComponent<ParticleSystem>();
        stunAura = this.gameObject.transform.Find("Geometry/SleepAura").GetComponent<ParticleSystem>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    // HEAL EFFECTS
    [PunRPC]
    public void RPC_Heal(float amount, string playerTeam) 
    { 
        string ownerTeam = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"];
        
        // Debug.Log(ownerTeam == playerTeam);
        // Debug.Log("PLAYER TEAM: " + playerTeam + playerTeam.GetType());
        // Debug.Log("OWNER TEAM: " + ownerTeam + ownerTeam.GetType());

        if(view.IsMine && ownerTeam == playerTeam) {
            view.RPC("emitHealthAura", RpcTarget.All);
            health.RestoreHealth(amount);
            // Heal(30f);
        }
    }

    //STUN EFFECTS
    [PunRPC]
    public void RPC_Stun(string playerTeam)
    {
        string ownerTeam = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"];
        if(view.IsMine && ownerTeam != playerTeam) {
            view.RPC("Stunned", RpcTarget.All);
            // Heal(30f);
        }
    }

    // [PunRPC]
    // public void Heal(float amount) {
    //     health.RestoreHealth(amount);
    // }
    [PunRPC]
    public void Stunned() {
        animator.Play("Stun 0", 0, 3.0f);
        stunAura.Play();
        player.enabled = false;
        characterController.enabled = false;
        StartCoroutine(RemoveStun());
    }
    
    [PunRPC]
    IEnumerator RemoveStun() {
        yield return new WaitForSeconds(3f);
        player.enabled = true;
        characterController.enabled = true;
        stunAura.Stop();
    }

    //AURAS
    [PunRPC]
    void emitHealthAura() {
        healAura.Play();
        StartCoroutine(StopAura(healAura));
        StartCoroutine(RemoveStun());
    }

    [PunRPC]
    void emitStunAura() {
        stunAura.Play();
        StartCoroutine(StopAura(stunAura));
    }

    [PunRPC]
    IEnumerator StopAura(ParticleSystem aura) {
        yield return new WaitForSeconds(3f);
        aura.Stop();
    }
}
