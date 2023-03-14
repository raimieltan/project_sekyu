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

public class Curse : Ability
{
    public float range = 7f;

    private StarterAssetsInputs starterAssetsInputs;
    public PhotonView view;
    private ParticleSystem curseAura;
    private AbilitiesEffect abilitiesEffect;
    void Awake()
    {
        cooldownTime = 12f;
        nextFireTime = 0;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        abilitiesEffect = GetComponent<AbilitiesEffect>();
        curseAura = this.gameObject.transform.Find("Geometry/DarknessAura").GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (Time.time > nextFireTime && starterAssetsInputs.firstAbility)
        {   
            if (view.IsMine){
                view.RPC("CurseEnemies", RpcTarget.All);
            }      
        }
    }

    [PunRPC]
    private void CurseEnemies()
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
                effect.RPC_Curse(ownerTeam);
            }
        }
    }

    [PunRPC]
    private void emitAura() {
        curseAura.Play();
        StartCoroutine(StopAura());
    }

    IEnumerator StopAura() {
        yield return new WaitForSeconds(3f);
        curseAura.Stop();
    }
}