using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Tikbalang_Abilities : MonoBehaviour
{
    private ThirdPersonController player;
    private StarterAssetsInputs starterAssetsInputs;
    private ParticleSystem fireAura;
    private bool berserk = false;
    private CharacterController cc;
    private float dashSpeed = 3f;
    private float dashTime = 0.5f;

    void Start() {
        cc = GetComponent<CharacterController>();
        player = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        fireAura = GameObject.Find("FireAura").GetComponent<ParticleSystem>();
    }

    void Update() {
        Berserk();
        Dash();
    }

    void Berserk() {
        if(starterAssetsInputs.firstAbility) {
            berserk = true;
            player.MoveSpeed = 5.0f;
            player.SprintSpeed = 8.335f;
            fireAura.Play();
            StartCoroutine(ReturnMovementSpeed());
        }
    }

    void Dash() {
        if(starterAssetsInputs.secondAbility) {
            StartCoroutine(DashMovement());
        }
    }

    IEnumerator DashMovement()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            cc.Move(transform.forward * dashSpeed * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator ReturnMovementSpeed() {
        yield return new WaitForSeconds(5f);
        player.MoveSpeed = 2.0f;
        player.SprintSpeed = 5.335f;
        fireAura.Stop();
    }
}
