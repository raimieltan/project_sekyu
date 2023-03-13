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
    public ParticleSystem healAura, stunAura, reviveAura;
    private PlayerInput player;
    private Animator animator;
    private CharacterController characterController;
    private ThirdPersonController thirdPersonController;
    private hit hit;

    void Start()
    {
        player = GetComponent<PlayerInput>();
        health = GetComponent<Health>();
        characterController = GetComponent<CharacterController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        healAura = this.gameObject.transform.Find("Geometry/HealingAura").GetComponent<ParticleSystem>();
        stunAura = this.gameObject.transform.Find("Geometry/SleepAura").GetComponent<ParticleSystem>();
        reviveAura = this.gameObject.transform.Find("Geometry/WaterAura").GetComponent<ParticleSystem>();
        animator = this.gameObject.GetComponent<Animator>();
        hit = GetComponent<hit>();
    }

    // HEAL EFFECTS
    [PunRPC]
    public void RPC_Heal(float amount, string playerTeam) 
    { 
        string ownerTeam = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"];
        
        // Debug.Log(ownerTeam == playerTeam);
        // Debug.Log("PLAYER TEAM: " + playerTeam + playerTeam.GetType());
        // Debug.Log("OWNER TEAM: " + ownerTeam + ownerTeam.GetType());

        if (!health.isDead) {
            if(view.IsMine && ownerTeam == playerTeam) {
                view.RPC("emitHealthAura", RpcTarget.All);
                health.RestoreHealth(amount);
                // Heal(30f);
            }
        }
    }

    //STUN EFFECTS
    [PunRPC]
    public void RPC_Stun(string playerTeam)
    {
        string ownerTeam = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"];
        
        if (!health.isDead) {
            if(view.IsMine && ownerTeam != playerTeam) {
                view.RPC("Stunned", RpcTarget.All);
            }
        }
    }

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

    //REVIVE EFFECTS
    [PunRPC]
    public void RPC_Revive()
    {
        string ownerTeam = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"];

        if(view.IsMine) 
        {
            view.RPC("Revived", RpcTarget.All);
        }
        
    }

    [PunRPC]
    public void Revived()
    {   

        animator.SetBool("isDead", false);
        animator.SetBool("isRevive", true);  

        health.isDead = false;
        hit.isDead = false;

        characterController.enabled = true;
        thirdPersonController.enabled = true;
        player.enabled = true;

        view.RPC("emitReviveAura", RpcTarget.All);

        health.RestoreHealth(100);

        // Debug.Log("Health IsDead: " + health.isDead);
        // Debug.Log("Hit IsDead: " + hit.isDead);
        // Debug.Log("CharacterController" + characterController.enabled);
        // Debug.Log("thirdPersonController" + thirdPersonController.enabled);
        // Debug.Log("Player" + player.enabled);

        // playerHud.SetActive(true);

        // characterController.direction = 1;
        // animator.SetBool("isDead", false);
        // animator.SetBool("isRevive", false);

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

    void emitReviveAura() {
        reviveAura.Play();
        StartCoroutine(StopAura(reviveAura));
    }

    [PunRPC]
    IEnumerator StopAura(ParticleSystem aura) {
        yield return new WaitForSeconds(3f);
        aura.Stop();
    }
}
