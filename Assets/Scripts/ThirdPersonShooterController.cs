using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using Photon.Pun;
using Photon.Realtime;

public enum AttackType {
    MELEE,
    MAGIC
}

public class ThirdPersonShooterController : MonoBehaviour
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
    private int _animIDAttack;
    public PhotonView view;
    private bool _hasAnimator;
    public GameObject ui;

    private void Awake()
    {

        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        canAttack = true;
        inCombat = true;
        _animIDAttack = Animator.StringToHash("Attack");
        _hasAnimator = TryGetComponent(out animator);
    
    }
    private void Update()
    {
            animator = GetComponent<Animator>();
	        if( view.IsMine == false && PhotonNetwork.IsConnected == true )
	        {
                Destroy(ui);
	            
	        }

        if (view.IsMine) {
            if (_hasAnimator){
                if(canAttack){
                    animator.ResetTrigger(_animIDAttack);
                }
            }

            mouseWorldPosition = Vector3.zero;

            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask)) {
            
                mouseWorldPosition = raycastHit.point;
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

                    if(starterAssetsInputs.shoot && canAttack && inCombat) {
                        StartCoroutine(attackMagic(aimDir)); 
                    }
                }
                else {
                    aimVirtualCamera.gameObject.SetActive(false);
                    thirdPersonController.SetSensitivity(normalSensitivity);
                }

            }
            else {

                if(starterAssetsInputs.shoot && canAttack && inCombat && view.IsMine) {
                    if (_hasAnimator){
                        animator.SetTrigger(_animIDAttack);
                    }   
                
                    StartCoroutine(attackMelee());
                    
                }
            }
        }
 

    
    }


    IEnumerator attackMelee() {
        if (view.IsMine) {
            canAttack = false;
            yield return new WaitForSeconds(2.0f);
            starterAssetsInputs.shoot = false;
            canAttack = true;            
        }

    }

    IEnumerator attackMagic(Vector3 aimDir) {
        if (view.IsMine) {
        canAttack = false;

        animator.Play("SpellAttack", 0, 0.15f);
        yield return new WaitForSeconds(0.3f);

        PhotonNetwork.Instantiate(pfBulletProj.name, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
        starterAssetsInputs.shoot = false;
     

        canAttack = true;
        }

    }

    IEnumerator attackMeleeIncreased()
    {
        canAttack = false;

        animator.Play("Attack", 0, 0.5f);
        yield return new WaitForSeconds(0.8f);
        starterAssetsInputs.shoot = false;

        canAttack = true;
    }

}