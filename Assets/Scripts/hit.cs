using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class hit : MonoBehaviour
{
    public Health health;
    public PhotonView view;
    private ParticleSystem bloodAura;
    public bool damageTaken;
    public bool isDead;
    Player sender;

    private CharacterController characterController;

    private float originalRadius;
    private StarterAssets.ThirdPersonController thirdPersonController;
    public GameObject playerHud;
    public Animator animator;
    private AbilitiesEffect abilitiesEffect;
    public bool isCurse;

    void Awake(){
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalRadius = health.originalRadius;
        abilitiesEffect = this.gameObject.GetComponent<AbilitiesEffect>();
        isCurse = false;

        bloodAura = this.gameObject.transform.Find("Geometry/BloodAura").GetComponent<ParticleSystem>();
        thirdPersonController = GetComponent<StarterAssets.ThirdPersonController>();
    }

    void Update() {
        Debug.Log("CURSED: " + isCurse);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Weapon")
        {

            Player player = other.gameObject.transform.root.gameObject.GetComponent<PhotonView>().Owner;

            if (player.CustomProperties != null)
            {
                // Get the value of a specific custom property
                object targetTeam = player.CustomProperties["team"];

                // Do something with the custom property value
                if((string)PhotonNetwork.LocalPlayer.CustomProperties["team"] != (string)targetTeam ) {

                    Damage damage = other.gameObject.GetComponent<Damage>();
                    StartCoroutine(EndDamageTaken());
                    PhotonView attackerView = other.transform.root.GetComponent<PhotonView>();
                    sender = attackerView.Owner;
                    
                    if (isCurse) {
                        applyDamage(damage.value * 2);
                    } else {
                        applyDamage(damage.value);
                    }
                }
            }

        }

        if (other.gameObject.tag == "revive")
        {
            Player otherPlayer = other.gameObject.transform.root.gameObject.GetComponent<PhotonView>().Owner;

            if (otherPlayer.CustomProperties != null)
            {
                string otherTeam = (string)otherPlayer.CustomProperties["team"];
                // Do something with the custom property value
                if((string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == (string)otherTeam) {
                    if(health.isDead) {
                        abilitiesEffect.RPC_Revive();
                    }
                }
            }
        }
    }


    private void applyDamage(float value)
    {
        view.RPC(nameof(RPC_applyDamage), PhotonNetwork.LocalPlayer, value, sender);
	}

    [PunRPC]
    public void RPC_applyDamage(float value, Player sender, PhotonMessageInfo info)
    {

        Debug.Log(health.currentHealth);
        view.RPC("emitAuraBlood",RpcTarget.All);

        health.TakeDamage(value);

        if (health.currentHealth <= 0)
        {
            if(!isDead && view.IsMine)
            {
                LeaderboardData data = LeaderboardManager.manager.GetPlayerLeaderboardData(sender.ActorNumber);
                data.killCount++;
                data.currentScore+=50;
                data.killStreak++;
                LeaderboardManager.manager.SetPlayerLeaderboardData(sender.ActorNumber, data);
                LeaderboardManager.manager.RefreshLeaderboardData();
                isDead = true;
            }
        }
	}

    IEnumerator EndDamageTaken()
    {
        damageTaken = true;
        yield return new WaitForSeconds(2.0f);
        damageTaken = false;
    }

    [PunRPC]
    private void emitBlood() {
        bloodAura.Play();
        StartCoroutine(StopAura());
    }

    IEnumerator StopAura() {
        yield return new WaitForSeconds(1.0f);
        bloodAura.Stop();
    }
}
