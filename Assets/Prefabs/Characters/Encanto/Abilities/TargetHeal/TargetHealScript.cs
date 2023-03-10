using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

// public enum AttackType {
//     MELEE,
//     MAGIC
// }


public class TargetHealScript : Ability
{
    [SerializeField] private LayerMask aimColliderMask;
    [SerializeField] private Transform healProjectile;

    private ThirdPersonShooterController thirdPersonShooterController;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Transform spawnHealProjectile;
    private CinemachineVirtualCamera aimVirtualCamera;
    private float normalSensitivity;
    private float aimSensitivity;
    // private ParticleSystem goldAura;
    // bool canAttack;
    // public AttackType type;
    // private Animator animator;

    private bool isHealing;
    private void Awake()
    {
        thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        spawnHealProjectile = thirdPersonShooterController.spawnBulletPos;
        aimVirtualCamera = thirdPersonShooterController.aimVirtualCamera;
        normalSensitivity = thirdPersonShooterController.normalSensitivity;
        aimSensitivity = thirdPersonShooterController.aimSensitivity;
        cooldownTime = 5;
        nextFireTime = 0;
        // goldAura = transform.Find("Skeleton/GoldAura").GetComponent<ParticleSystem>();
    }
    private void Update()
    {

        // Vector3 mouseWorldPosition = Vector3.zero;

        // Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        // Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        // if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        // {

        //     mouseWorldPosition = raycastHit.point;
        // }

        // if (Time.time > nextFireTime)
        // {
        //     if (starterAssetsInputs.secondAbility)
        //     {
        //         if (!isHealing)
        //         {
        //             isHealing = true;
        //         }
        //         else
        //         {
        //             isHealing = false;
        //         }

        //         nextFireTime = Time.time + cooldownTime;
        //         TriggerFireEvent();
        //     }
        // }

         if (Time.time > nextFireTime && starterAssetsInputs.secondAbility)
        {
            if (thirdPersonShooterController.inCombat)
            {
                // goldAura.Play();
                thirdPersonShooterController.inCombat = false;
                starterAssetsInputs.secondAbility = false;
            }
            else
            {
                // goldAura.Stop();
                thirdPersonShooterController.inCombat = true;
                starterAssetsInputs.secondAbility = false;
            }

            nextFireTime = Time.time + cooldownTime;
            TriggerFireEvent();
        }

        Vector3 mouseWorldPosition = thirdPersonShooterController.mouseWorldPosition;

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
                    Vector3 aimDir = (mouseWorldPosition - spawnHealProjectile.position).normalized;
                    Instantiate(healProjectile, spawnHealProjectile.position, Quaternion.LookRotation(aimDir, Vector3.up));
                    // goldAura2.Stop();
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

}
