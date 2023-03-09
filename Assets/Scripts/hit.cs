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

    void Awake(){
        bloodAura = transform.Find("Geometry/BloodAura")?.GetComponent<ParticleSystem>();
        if (bloodAura == null) {
            Debug.LogError("Failed to find BloodAura particle system");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Weapon")
        {
            Damage damage = other.gameObject.GetComponent<Damage>();
            StartCoroutine(EndDamageTaken());
            PhotonView attackerView = other.transform.root.GetComponent<PhotonView>();
            sender = attackerView.Owner;
            applyDamage(damage.value);
        }
    }

    private void applyDamage(float value)
    {
        view.RPC(nameof(RPC_applyDamage), PhotonNetwork.LocalPlayer, value, sender);
	}

    [PunRPC]
    public void RPC_applyDamage(float value, Player sender, PhotonMessageInfo info)
    {
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
