using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using CartoonFX;
using Photon.Pun;
using Photon.Realtime;

public class Traps : MonoBehaviour
{
    private float waitTime = 3;
    private float poisonDmg = 10f;
    private float poisonTime = 3f;
    private float explosiveDmg = 30f;
    private bool insideTrap;
    public string trapTeam;
    private ThirdPersonController player;
    private ParticleSystem poisonAura;
    private ParticleSystem meteorAura;
    [SerializeField] private ParticleSystem poisonAnim;
    [SerializeField] private ParticleSystem virusAnim;
    [SerializeField] private ParticleSystem slowAnim;
    [SerializeField] private ParticleSystem explosiveAnim;
    public PhotonView photonView;
    private string teamOwner;
    private Team playerTeam;


    void Awake() {
      
        trapTeam = (string)photonView.InstantiationData[1];
        Debug.Log(trapTeam);
    }
    

    void OnTriggerEnter(Collider col)
    {   
        PhotonView photonView = GetComponentInParent<PhotonView>();
        if (photonView != null)
        {
            
            string trapTeam = (string)photonView.InstantiationData[1]; 
            if (col.gameObject.tag == "Player")
            {
            
                ThirdPersonController player = col.gameObject.GetComponent<ThirdPersonController>();
                TeamTag playerTeamScipt = col.gameObject.GetComponent<TeamTag>();
                poisonAura = col.transform.Find("Geometry/PoisonAura").GetComponent<ParticleSystem>();
                meteorAura = col.transform.Find("Geometry/MeteorAura").GetComponent<ParticleSystem>();

                Player enemy = col.gameObject.GetComponent<PhotonView>().Owner;
                
                Debug.Log(enemy.CustomProperties);
                
        
                Debug.Log("trap team: " + trapTeam);
                Debug.Log("Player team: " + (string)enemy.CustomProperties["team"]);
                
            
                if (trapTeam != (string)enemy.CustomProperties["team"])
                {
                    Debug.Log("Activated");
                    if (transform.name == "SlowCol")
                    {
                        player.MoveSpeed = 0.5f;
                        player.SprintSpeed = 0.5f;
                        slowAnim.Play();
                        StartCoroutine(ReturnMovementSpeed(waitTime, player, transform.parent.gameObject));
                    }
                    else if (transform.name == "PoisonCol")
                    {
                        poisonAnim.Play();
                        virusAnim.Play();
                        poisonAura.Play();
                        StartCoroutine(PlayPoisonAnimation(poisonAura));
                        DamageOverTime(poisonDmg, poisonTime, player);
                    }
                    else if (transform.name == "ExplosiveCol")
                    {
                        Health healthRef = player.GetComponentInParent<Health>();
                        explosiveAnim.Play();
                        meteorAura.Play();
                        StartCoroutine(PlayPoisonAnimation(meteorAura));
                         healthRef.TakeDamage(explosiveDmg);
                        // player.currentHealth -= explosiveDmg;
                        // player.healthBar.UpdateHealthBar(player.maxHealth, player.currentHealth);
                    }
                }

            }
            else if (col.gameObject.name == "Bullet(Clone)")
            {
                transform.gameObject.SetActive(false);
                MonoBehaviour camMono = Camera.main.GetComponent<MonoBehaviour>();
                camMono.StartCoroutine(DisableTrap(3f, transform.gameObject));
                transform.gameObject.SetActive(false);
            }
        } else {
            Debug.Log(photonView);
        }


    }

    public void DamageOverTime(float dmgAmount, float dmgTime, ThirdPersonController player)
    {
        StartCoroutine(DamageOverTimeCoroutine(dmgAmount, dmgTime, player));
    }

    IEnumerator DamageOverTimeCoroutine(float dmgAmount, float duration, ThirdPersonController player)
    {
        float amountDamaged = 0;
        float damagePerLoop = dmgAmount / duration;
        Health healthRef = player.GetComponentInParent<Health>();
        while (amountDamaged < dmgAmount)
        {
            //healthRef.currentHealth -= damagePerLoop;
            healthRef.TakeDamage(damagePerLoop);
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }

    }

    IEnumerator DisableTrap(float waitTime, GameObject trap)
    {
        yield return new WaitForSeconds(waitTime);
        trap.SetActive(true);
    }

    IEnumerator ReturnMovementSpeed(float waitTime, ThirdPersonController player, GameObject trap)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(trap);
        player.MoveSpeed = 2f;
        player.SprintSpeed = 5.335f;
    }

    IEnumerator PlayPoisonAnimation(ParticleSystem trapAura)
    {
        Debug.Log(trapAura.name);
        yield return new WaitForSeconds(2f);
        trapAura.Stop();
    }
}
