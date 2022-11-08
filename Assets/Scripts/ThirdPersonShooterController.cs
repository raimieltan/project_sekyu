using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public enum AttackType {
    MELEE,
    MAGIC
}


public class ThirdPersonShooterController : MonoBehaviour
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
    bool canAttack;
    public AttackType type;
    private Animator animator;

    private bool isReviving;

    private Transform currentBullet;
    private string animationTrigger;
    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        isReviving = false;
        currentBullet = pfBulletProj;
        animator = GetComponent<Animator>();
        animationTrigger = "Shoot";
        canAttack = true;
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
            if(type == AttackType.MAGIC){
                if(starterAssetsInputs.aim){
                    aimVirtualCamera.gameObject.SetActive(true);
                    thirdPersonController.SetSensitivity(aimSensitivity);
                    Vector3 worldAimTarget = mouseWorldPosition;
                    worldAimTarget.y = transform.position.y;
                    Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
                    transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

                    Vector3 aimDir = (mouseWorldPosition - spawnBulletPos.position).normalized;

                    if(starterAssetsInputs.shoot) {
                        
                        if(starterAssetsInputs.shoot && canAttack) {
                            StartCoroutine(attackMagic(aimDir)); 
                        }
                    }
                    else {
                        aimVirtualCamera.gameObject.SetActive(false);
                        thirdPersonController.SetSensitivity(normalSensitivity);
                        Instantiate(currentBullet, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                        starterAssetsInputs.shoot = false;
                        animator.SetTrigger(animationTrigger);
                }

                }
            else {
                if(starterAssetsInputs.shoot && canAttack) {
                    StartCoroutine(attackMelee());
                }
            }

            }

        IEnumerator attackMelee() {
            canAttack = false;

            animator.Play("Attack", 0, 0.25f);
            yield return new WaitForSeconds(1.3f);

            canAttack = true;

        }

        IEnumerator attackMagic(Vector3 aimDir) {
            canAttack = false;

            animator.Play("SpellAttack", 0, 0.15f);
            yield return new WaitForSeconds(0.3f);

            Instantiate(pfBulletProj, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;

            canAttack = true;
        }
    }
}
