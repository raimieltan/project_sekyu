using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using CartoonFX;

public class Traps : MonoBehaviour
{
    private float waitTime = 3;
    private float poisonDmg = 10f;
    private float poisonTime = 3f;
    private float explosiveDmg = 30f;
    private bool insideTrap;
    // float dmgInterval = 3;
    private ThirdPersonController player;
    [SerializeField] private ParticleSystem poisonAnim;
    [SerializeField] private ParticleSystem virusAnim;
    [SerializeField] private ParticleSystem slowAnim;
    [SerializeField] private ParticleSystem explosiveAnim;

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "PlayerArmature") {
            ThirdPersonController player = col.gameObject.GetComponent<ThirdPersonController>();
            if(transform.name == "SlowCol") {
                player.MoveSpeed = 0.5f;
                player.SprintSpeed = 0.5f;
                slowAnim.Play();
                StartCoroutine(ReturnMovementSpeed(waitTime, player, transform.parent.gameObject));
            } else if (transform.name == "PoisonCol"){
                poisonAnim.Play();
                virusAnim.Play();
                DamageOverTime(poisonDmg, poisonTime, player);
            } else if (transform.name == "ExplosiveCol") {
                Rigidbody rigg = col.GetComponent<Rigidbody>();
                rigg.AddForce(-transform.forward * 200, ForceMode.Acceleration);
                explosiveAnim.Play();
                player.currentHealth -= explosiveDmg;
                player.healthBar.UpdateHealthBar(player.maxHealth, player.currentHealth);
            }
        } else if (col.gameObject.name == "Bullet(Clone)") {
            transform.gameObject.SetActive(false);
            MonoBehaviour camMono = Camera.main.GetComponent<MonoBehaviour>();
            camMono.StartCoroutine(DisableTrap(3f, transform.gameObject));
            transform.gameObject.SetActive(false);
        }
    }

    public void DamageOverTime(float dmgAmount, float dmgTime, ThirdPersonController player) {
        StartCoroutine(DamageOverTimeCoroutine(dmgAmount, dmgTime, player));
    }

    IEnumerator DamageOverTimeCoroutine(float dmgAmount, float duration, ThirdPersonController player)  {
        float amountDamaged = 0;
        float damagePerLoop = dmgAmount / duration;
        while (amountDamaged < dmgAmount) {
            player.currentHealth -= damagePerLoop;
            player.healthBar.UpdateHealthBar(player.maxHealth, player.currentHealth);
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator DisableTrap(float waitTime, GameObject trap)
    {
        yield return new WaitForSeconds(waitTime);
        trap.SetActive(true);
    }

    IEnumerator ReturnMovementSpeed(float waitTime, ThirdPersonController player, GameObject trap) {
        yield return new WaitForSeconds(waitTime);
        Destroy(trap);
        player.MoveSpeed = 2f;
        player.SprintSpeed = 5.335f;
    }
}
