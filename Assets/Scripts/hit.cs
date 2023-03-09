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

    private CharacterController characterController;

    private float originalRadius;
    private StarterAssets.ThirdPersonController thirdPersonController;
    public GameObject playerHud;

    public Copycat copyCat;

    public Animator animator;

    void Awake(){
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalRadius = health.originalRadius;
        copyCat = GetComponent<Copycat>();

        bloodAura = transform.Find("Geometry/BloodAura")?.GetComponent<ParticleSystem>();
        if (bloodAura == null) {
            Debug.LogError("Failed to find BloodAura particle system");
        }

        thirdPersonController = GetComponent<StarterAssets.ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Weapon")
        {
            Damage damage = other.gameObject.GetComponent<Damage>();
            StartCoroutine(EndDamageTaken());
            applyDamage(damage.value);

            if (copyCat) {
                copyCat.Revert();
            }
        }

        if (other.gameObject.tag == "revive")
        {
            if(health.isDead) {
                revivePlayer();
            }
        }
    }

    private void revivePlayer()
    {
        health.RestoreHealth(100);
        health.isDead = false;

        animator.SetBool("isDead", false);
        animator.SetBool("isRevive", true);
        thirdPersonController.enabled = true;
        playerHud.SetActive(true);

        // characterController.direction = 1;
            
        characterController.radius = originalRadius;
        // animator.SetBool("isDead", false);
        // animator.SetBool("isRevive", false);

    }

    private void applyDamage(float value)
    {

        Debug.Log(health.currentHealth);
        // view.RPC("emitAuraBlood",RpcTarget.All);
        health.TakeDamage(value);

        if (health.currentHealth <= 0)
        {
            // PhotonNetwork.Destroy(this.gameObject);
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
