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
    public float range = 9f;

    private StarterAssetsInputs starterAssetsInputs;
    public PhotonView view;
    private ParticleSystem whiteAura;
    private AbilitiesEffect abilitiesEffect;
    private ParticleSystem whiteAura;
    private AbilitiesEffect abilitiesEffect;
    private ParticleSystem whiteAura;
    private AbilitiesEffect abilitiesEffect;
    void Awake()
    {
        cooldownTime = 3f;
        nextFireTime = 0;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        abilitiesEffect = GetComponent<AbilitiesEffect>();
        whiteAura = this.gameObject.transform.Find("Geometry/WhiteAura").GetComponent<ParticleSystem>();
        abilitiesEffect = GetComponent<AbilitiesEffect>();
        whiteAura = this.gameObject.transform.Find("Geometry/WhiteAura").GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (Time.time > nextFireTime && starterAssetsInputs.secondAbility)
        {   
            if (view.IsMine){
                view.RPC("StunEnemies", RpcTarget.All);
            }      
        }
    }

    [PunRPC]
    private void StunEnemies()
    {
        nextFireTime = Time.time + cooldownTime;
        TriggerFireEvent();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        view.RPC("emitAura", RpcTarget.All);

        string ownerTeam = (string)this.gameObject.GetComponent<PhotonView>().Owner.CustomProperties["team"];
        //TODO: REFACTOR ANIMATION AND COLLIDERS
        foreach (Collider collider in colliders)
        {  
            if(collider.gameObject.GetComponent<PhotonView>()) {
                AbilitiesEffect effect = collider.gameObject.GetComponent<AbilitiesEffect>();
                effect.RPC_Stun(ownerTeam);
                foreach (Collider collider in colliders)
                {  
                    if(collider.gameObject.GetComponent<PhotonView>()) {
                        AbilitiesEffect effect = collider.gameObject.GetComponent<AbilitiesEffect>();
                        effect.RPC_Stun(ownerTeam);
                    }
                }
            }
        }
    }

    [PunRPC]
    private void emitAura() {
        whiteAura.Play();
        StartCoroutine(StopAura());
    }

    IEnumerator StopAura() {
        yield return new WaitForSeconds(3f);
        whiteAura.Stop();
    }
}   