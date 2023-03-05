using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using UnityEngine;

public class Dash : Ability
{
    private StarterAssetsInputs starterAssetsInputs;

    private CharacterController cc;

    private float dashSpeed = 20f;

    private float dashTime = 0.5f;

    public PhotonView view;

    private ParticleSystem lightningAura;

    [SerializeField]
    private AudioClip dashSound;

    // AudioSource audioSource;

    void Awake()
    {
        cooldownTime = 5;
        nextFireTime = 0;
        cc = GetComponent<CharacterController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        // audioSource = GetComponent<AudioSource>();
        lightningAura =
            transform
                .Find("Geometry/LightningAura")
                .GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Time.time > nextFireTime && starterAssetsInputs.firstAbility)
        {
            if (view.IsMine)
            {
                view.RPC("dash", RpcTarget.All);
                // AudioSource.PlayOneShot ();
                AudioSource.PlayClipAtPoint(dashSound, transform.position);

            }
        }
    }

    [PunRPC]
    void dash()
    {
        nextFireTime = Time.time + cooldownTime;
        TriggerFireEvent();
        StartCoroutine(DashMovement());
    }

    IEnumerator DashMovement()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            lightningAura.Play();
            cc.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
        lightningAura.Stop();
    }
}
