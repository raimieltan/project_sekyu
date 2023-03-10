using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;

public class Heal : Ability
{
    public int healAmount = 20;
    public float range = 7f;
    public AudioClip healSound;
    public PhotonView view;
    AudioSource audioSource;
    private ParticleSystem healingAura; 
    private ThirdPersonController player;
    private StarterAssetsInputs starterAssetsInputs;
    void Awake()
    {
        cooldownTime = 3;
        nextFireTime = 0;
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        
        healingAura = transform.Find("Geometry/HealingAura").GetComponent<ParticleSystem>(); 
    }

    void Update()
    {
        if (Time.time > nextFireTime && starterAssetsInputs.firstAbility)
        {    
            if (view.IsMine) {
                view.RPC("HealNearbyAllies", RpcTarget.All);
            }   
        }
    }
    
    [PunRPC]
    private void HealNearbyAllies()
    {
        nextFireTime = Time.time + cooldownTime;
        TriggerFireEvent();
        audioSource.PlayOneShot(healSound);
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider c in colliders)
        {

            if (c.TryGetComponent<TeamTag>(out var allyTeamScript))
            {
                Team playerTeam = starterAssetsInputs.gameObject.GetComponent<TeamTag>().team;
                ParticleSystem otherPlayerHealingAura = c.transform.Find("Geometry/HealingAura").GetComponent<ParticleSystem>();

                // Debug.Log("OTHER TEAM: " + allyTeamScript.team);
                // Debug.Log("MY TEAM: " + playerTeam);

                if (allyTeamScript.team == playerTeam)
                {        
                    healingAura.Play();
                    StartCoroutine(StopAura(healingAura));
                    
                    if (c.GetComponent<Health>())
                    {
                        float maxHealth = c.GetComponent<Health>().maxHealth;
                        c.GetComponent<Health>().RestoreHealth(maxHealth * 0.3f);
                        otherPlayerHealingAura.Play();
                        StartCoroutine(StopAura(otherPlayerHealingAura));
                    }
                }
            }
        }
    }

    IEnumerator StopAura(ParticleSystem healingAura)
    {
        yield return new WaitForSeconds(2f);
        healingAura.Stop();
    }
}