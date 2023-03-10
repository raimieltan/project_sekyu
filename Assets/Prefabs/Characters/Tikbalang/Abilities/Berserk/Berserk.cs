using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using UnityEngine;

public class Berserk : Ability
{
    private ThirdPersonController player;

    private StarterAssetsInputs starterAssetsInputs;

    private ParticleSystem fireAura;

    public PhotonView view;

    public bool berserk = false;

    [SerializeField]
    private AudioClip berserkSound;

    AudioSource audioSource;

    void Awake()
    {
        cooldownTime = 5;
        nextFireTime = 0;
        player = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        audioSource = GetComponent<AudioSource>();
        fireAura =
            transform.Find("Geometry/FireAura").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Time.time > nextFireTime && starterAssetsInputs.secondAbility)
        {
            if (view.IsMine)
            {
                //call this method for all other players in the current room:
                view.RPC("emitAura", RpcTarget.All);
                audioSource.PlayOneShot (berserkSound);
            }
        }
    }

    [PunRPC]
    void emitAura()
    {
        berserk = true;
        player.MoveSpeed = 5.0f;
        player.SprintSpeed = 8.335f;

        fireAura.Play();
        nextFireTime = Time.time + cooldownTime;
        TriggerFireEvent();
        StartCoroutine(ReturnMovementSpeed());
    }

    IEnumerator ReturnMovementSpeed()
    {
        yield return new WaitForSeconds(5f);
        player.MoveSpeed = 2.0f;
        player.SprintSpeed = 5.335f;
        fireAura.Stop();
        berserk = false;
    }
}
