using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using UnityEngine;

public class Berserk : Ability
{
    public ThirdPersonController player;

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
        
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        player = this.gameObject.GetComponent<ThirdPersonController>();
        audioSource = GetComponent<AudioSource>();
        fireAura =
            this.gameObject.transform.Find("Geometry/FireAura").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Time.time > nextFireTime && starterAssetsInputs.secondAbility)
        {
            if (view.IsMine)
            {
                //call this method for all other players in the current room:
                view.RPC("GoBerserk", RpcTarget.All);
                audioSource.PlayOneShot(berserkSound);
            }
        }
    }

    [PunRPC]
    public void GoBerserk()
    {  
        nextFireTime = Time.time + cooldownTime;
        TriggerFireEvent();

        berserk = true;
        player.MoveSpeed = 200.0f;
        Debug.Log("Player MoveSpeed: " + player.MoveSpeed);
        
        player.SprintSpeed = 200f;

        Debug.Log("Player MoveSpeed: " + player.SprintSpeed);

        fireAura.Play();
        StartCoroutine(ApplyBerserk());
    }

    IEnumerator ApplyBerserk()
    {
        yield return new WaitForSeconds(5f);

        player.MoveSpeed = 2.0f;
        player.SprintSpeed = 5.335f;
        fireAura.Stop();
        berserk = false;
    }
}
