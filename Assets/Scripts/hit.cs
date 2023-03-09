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

    void Awake(){
        bloodAura = transform.Find("Geometry/BloodAura").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Weapon")
        {
            
            Debug.Log("Test");
            Debug.Log(other.gameObject.transform.root.gameObject.GetComponent<PhotonView>());


        Player player = other.gameObject.transform.root.gameObject.GetComponent<PhotonView>().Owner;

        if (player.CustomProperties != null)
        {
            // Get the value of a specific custom property
            object targetTeam = player.CustomProperties["team"];

            // Do something with the custom property value
            Debug.Log("Player has custom property " + targetTeam);
            if((string)PhotonNetwork.LocalPlayer.CustomProperties["team"] != (string)targetTeam ) {

            Damage damage = other.gameObject.GetComponent<Damage>();
        
            StartCoroutine(EndDamageTaken());
            PhotonView attackerView = other.transform.root.GetComponent<PhotonView>();
            sender = attackerView.Owner;
            Player player = other.gameObject.transform.root.gameObject.GetComponent<PhotonView>().Owner;

        if (player.CustomProperties != null)
        {
            // Get the value of a specific custom property
            object targetTeam = player.CustomProperties["team"];

            // Do something with the custom property value
            Debug.Log("Player has custom property " + targetTeam);
            if((string)PhotonNetwork.LocalPlayer.CustomProperties["team"] != (string)targetTeam ) {

            Damage damage = other.gameObject.GetComponent<Damage>();
        
            StartCoroutine(EndDamageTaken());
            applyDamage(damage.value);
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
        // view.RPC("emitAuraBlood",RpcTarget.All);
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
            PhotonNetwork.Destroy(this.gameObject);
        }

	}

    // [PunRPC]
    // void emitAuraBlood() {

    //         bloodAura.Play();
    //         StartCoroutine(EndBlood());

    // }

    // IEnumerator EndBlood()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     bloodAura.Stop();

    // }

    IEnumerator EndDamageTaken()
    {
        damageTaken = true;
        yield return new WaitForSeconds(2.0f);
        damageTaken = false;
    }
}
