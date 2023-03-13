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
    public float range = 7.0f;
    public AudioClip healSound;
    public PhotonView view;
    AudioSource audioSource;
    private ParticleSystem healthAura; 
    private StarterAssetsInputs starterAssetsInputs;
    private Health health;
    public AbilitiesEffect abilitiesEffect;
    void Awake()
    {
        cooldownTime = 3;
        nextFireTime = 0;
        audioSource = GetComponent<AudioSource>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        health = GetComponent<Health>();
        abilitiesEffect = GetComponent<AbilitiesEffect>();
        healthAura = this.gameObject.transform.Find("Geometry/HealingAura").GetComponent<ParticleSystem>(); 
    }

    void Update()
    {
        if (Time.time > nextFireTime && starterAssetsInputs.firstAbility)
        {    
            if (view.IsMine) {
                view.RPC("HealAllies", RpcTarget.All);
            }   
        }
    }
    
    [PunRPC]
    private void HealAllies()
    {
        nextFireTime = Time.time + cooldownTime;
        TriggerFireEvent();
        audioSource.PlayOneShot(healSound);
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        string ownerTeam = (string)this.gameObject.GetComponent<PhotonView>().Owner.CustomProperties["team"];

        foreach (Collider collider in colliders)
        {  
            if(collider.gameObject.GetComponent<PhotonView>()) {
                AbilitiesEffect effect = collider.gameObject.GetComponent<AbilitiesEffect>();
                effect.RPC_Heal(30f, ownerTeam);
            }
        }
    }
}