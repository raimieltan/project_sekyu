    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using Photon.Pun;
using Photon.Realtime;


public class Revive : Ability
{
    [SerializeField] private LayerMask aimColliderMask;
    [SerializeField] private Transform reviveOrb;

    private ThirdPersonShooterController thirdPersonShooterController;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Transform spawnReviveOrb;
    private CinemachineVirtualCamera aimVirtualCamera;
    private float normalSensitivity;
    private float aimSensitivity;
    private ParticleSystem reviveAura;

    private Animator animator;
    public PhotonView view;


    void Awake()
    {
        thirdPersonShooterController = this.gameObject.GetComponent<ThirdPersonShooterController>();
        thirdPersonController = this.gameObject.GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        spawnReviveOrb = thirdPersonShooterController.spawnBulletPos;
        aimVirtualCamera = thirdPersonShooterController.aimVirtualCamera;
        normalSensitivity = thirdPersonShooterController.normalSensitivity;
        aimSensitivity = thirdPersonShooterController.aimSensitivity;
        animator = GetComponent<Animator>();
        cooldownTime = 5;
        nextFireTime = 0;
        reviveAura = this.gameObject.transform.Find("Geometry/DarknessAura").GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        if (Time.time > nextFireTime && starterAssetsInputs.secondAbility)
        {
            if (view.IsMine) {
                view.RPC("ReviveToggle", RpcTarget.All);
            }
        }

        Vector3 mouseWorldPosition = thirdPersonShooterController.mouseWorldPosition;

        // Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        // Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        // if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask)) {

        //     mouseWorldPosition = raycastHit.point;
        // }

        if (view.IsMine) {
            view.RPC("ReviveForm", RpcTarget.All, mouseWorldPosition);
        }
    }

    [PunRPC]
    public void ReviveToggle()
    {
        if (thirdPersonShooterController.inCombat)
        {
            view.RPC("emitAura", RpcTarget.All);
            thirdPersonShooterController.inCombat = false;
            starterAssetsInputs.secondAbility = false;
        }   
        else
        {
            view.RPC("stopAura", RpcTarget.All);
            thirdPersonShooterController.inCombat = true;
            starterAssetsInputs.secondAbility = false;
        }
    }

    [PunRPC]
    public void ReviveForm(Vector3 mouseWorldPosition)
    {
        if (!thirdPersonShooterController.inCombat)
        {
            if (starterAssetsInputs.aim)
            {
                aimVirtualCamera.gameObject.SetActive(true);
                thirdPersonController.SetSensitivity(aimSensitivity);

                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

                if (starterAssetsInputs.shoot)
                {
                    view.RPC("spellAttackAnim", RpcTarget.All);
                    nextFireTime = Time.time + cooldownTime;
                    TriggerFireEvent();

                    Vector3 aimDir = (mouseWorldPosition - spawnReviveOrb.position).normalized;
                    // Instantiate(reviveOrb, spawnReviveOrb.position, Quaternion.LookRotation(aimDir, Vector3.up));
                    PhotonNetwork.Instantiate(reviveOrb.name, spawnReviveOrb.position, Quaternion.LookRotation(aimDir, Vector3.up));
                    view.RPC("stopAura", RpcTarget.All);
                    
                    starterAssetsInputs.shoot = false;
                    thirdPersonShooterController.inCombat = true;
                    aimVirtualCamera.gameObject.SetActive(false);
                }

            }
            else
            {
                aimVirtualCamera.gameObject.SetActive(false);
                thirdPersonController.SetSensitivity(normalSensitivity);
            }
        }
    }

    [PunRPC]
    public void emitAura(){
        reviveAura.Play(); 
    }

    [PunRPC]
    public void stopAura(){
        reviveAura.Stop();
    }

    [PunRPC]

    public void castSpellAnim() 
    {
        animator.Play("SpellAttack", 0, 0.15f);
    }
}
