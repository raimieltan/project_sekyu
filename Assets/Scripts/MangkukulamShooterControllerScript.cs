using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class MangkukulamShooterControllerScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderMask;
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProj;
    [SerializeField] private Transform spawnBulletPos;
    [SerializeField] private Transform reviveOrb;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private bool isReviving;

    private Transform currentBullet;
    private string animationTrigger;

    private Animator animator;
    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        isReviving = false;
        currentBullet = pfBulletProj;
        animator = GetComponent<Animator>();
        animationTrigger = "Shoot";
    }
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask)) {
          
            mouseWorldPosition = raycastHit.point;
        }

        if (Input.GetButtonDown("ToggleRevive")){
            if(!isReviving) {
                isReviving = true;
                currentBullet = reviveOrb;
                animationTrigger = "Revive";

            } else {
                isReviving = false;
                currentBullet = pfBulletProj;
                animationTrigger = "Shoot";
            }
        }

        if(starterAssetsInputs.aim){
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            if(starterAssetsInputs.shoot) {
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPos.position).normalized;
                Instantiate(currentBullet, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                starterAssetsInputs.shoot = false;
                animator.SetTrigger(animationTrigger);
            }
        }
        else {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
        }




    }
}
