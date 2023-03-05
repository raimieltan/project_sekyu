using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using Photon.Realtime;
// #if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using StarterAssets;
// #endif


// Ability class has variables such as cooldownTime and nextFireTime
// also declared an event which is triggered when pressing the button

public class Stun : Ability
{
    
    public float range = 7f;

    private StarterAssetsInputs starterAssetsInputs;
    public PhotonView view;
    private ParticleSystem WhiteAura; 
    private ThirdPersonController player;
    void Awake()
    {
        cooldownTime = 3;
        nextFireTime = 0;
        player = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        WhiteAura = transform.Find("Geometry/WhiteAura").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Time.time > nextFireTime && starterAssetsInputs.secondAbility)
        {   
            if (view.IsMine){
               
                view.RPC("StunNearbyEnemies", RpcTarget.All);
            }      
        }
    }

    [PunRPC]
    private void StunNearbyEnemies()
    {
        nextFireTime = Time.time + cooldownTime;
        TriggerFireEvent();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider c in colliders)
        {
            if (c.TryGetComponent<TeamTag>(out var allyTeamScript))
            {
                Team playerTeam = starterAssetsInputs.gameObject.GetComponent<TeamTag>().team;
                ParticleSystem otherSleepAura = c.transform.Find("Geometry/SleepAura").GetComponent<ParticleSystem>();
                    
                WhiteAura.Play();
                StartCoroutine(StopAura(WhiteAura));

                otherSleepAura.Play();
                StartCoroutine(StopAura(otherSleepAura));

                if (allyTeamScript.team != playerTeam)
                {
                    
                    if (c.GetComponent<CharacterController>())
                    {
                        StartCoroutine(StunEnemy(otherSleepAura, c.gameObject.GetComponent<PlayerInput>(), c.gameObject.GetComponent<CharacterController>())); 
                    }        
                }
            }
        }
    }

    // [PunRPC]
    IEnumerator StunEnemy(ParticleSystem sleepAura, PlayerInput player, CharacterController characterController)
    {  
        sleepAura.Play();
        player.enabled = false;
        characterController.enabled = false;
        yield return new WaitForSeconds(2f);
        sleepAura.Stop();
        player.enabled = true;
        characterController.enabled = true;
    }

    IEnumerator StopAura(ParticleSystem aura) {
        yield return new WaitForSeconds(2f);
        aura.Stop();
    }
}