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
            applyDamage(damage.value);
        }
    }

    private void applyDamage(float value)
    {

        Debug.Log(health.currentHealth);
        // view.RPC("emitAuraBlood",RpcTarget.All);
        health.TakeDamage(value);

        if (health.currentHealth <= 0)
        {
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
