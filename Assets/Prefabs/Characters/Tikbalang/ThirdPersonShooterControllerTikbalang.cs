using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public enum AttackTypeTikbalang
{
    MELEE,
    MAGIC
}

public class ThirdPersonShooterControllerTikbalang : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] public float normalSensitivity;
    [SerializeField] public float aimSensitivity;
    [SerializeField] private LayerMask aimColliderMask;
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProj;
    [SerializeField] public Transform spawnBulletPos;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    public bool inCombat;
    public Vector3 mouseWorldPosition;
    private bool canAttack;
    public AttackType type;
    private Animator animator;
    Berserk berserkScript;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        berserkScript = GetComponent<Berserk>();
        animator = GetComponent<Animator>();
        canAttack = true;
        inCombat = true;
    }
    private void Update()
    {
        mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {

            mouseWorldPosition = raycastHit.point;
        }

        if (type == AttackType.MAGIC)
        {
            if (starterAssetsInputs.aim)
            {
                aimVirtualCamera.gameObject.SetActive(true);
                thirdPersonController.SetSensitivity(aimSensitivity);

                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPos.position).normalized;

                if (starterAssetsInputs.shoot && canAttack && inCombat)
                {
                    StartCoroutine(attackMagic(aimDir));
                }
            }
            else
            {
                aimVirtualCamera.gameObject.SetActive(false);
                thirdPersonController.SetSensitivity(normalSensitivity);
            }

        }
        else
        {


            if (starterAssetsInputs.shoot && canAttack && inCombat)
            {
                if (berserkScript.berserk)
                {
                    StartCoroutine(attackMeleeIncreased());
                }
                else
                {
                    StartCoroutine(attackMelee());
                }
            }
        }

    }

    IEnumerator attackMelee()
    {
        canAttack = false;

        animator.Play("Attack", 0, 0.25f);
        yield return new WaitForSeconds(1.3f);
        starterAssetsInputs.shoot = false;

        canAttack = true;

    }

    IEnumerator attackMeleeIncreased()
    {
        canAttack = false;

        animator.Play("Attack", 0, 0.5f);
        yield return new WaitForSeconds(0.8f);
        starterAssetsInputs.shoot = false;

        canAttack = true;
    }


    IEnumerator attackMagic(Vector3 aimDir)
    {
        canAttack = false;

        animator.Play("SpellAttack", 0, 0.15f);
        yield return new WaitForSeconds(0.3f);

        Instantiate(pfBulletProj, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
        starterAssetsInputs.shoot = false;


        canAttack = true;

    }

}