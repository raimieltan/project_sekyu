using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;

    public float initialHealth;

    [SerializeField] public float currentHealth;

    public float armorAmount;
    
    public bool isDead;
    public bool isRevive;

    public delegate void UpdateHealth(float newHealth);

    public event UpdateHealth TriggerUpdateHealth;

    private PlayerInventory playerInventory;

    private ParticleSystem healthAura;

    private PhotonView view;

    private CharacterController characterController;
    private float radius;

    public float originalRadius;
    private Animator animator;
    private StarterAssets.ThirdPersonController thirdPersonController;
    [SerializeField] private GameObject playerHud;

    // private PlayerArmor playerArmor;
    // void Awake()
    // {
    //     armorAmount = 100;
    //     AddArmor (armorAmount);
    // }

    void Start()
    {
        view = GetComponent<PhotonView>();
        maxHealth = 200;
        initialHealth = 100;
        currentHealth = initialHealth;
        animator = GetComponent<Animator>();
        thirdPersonController = GetComponent<StarterAssets.ThirdPersonController>();
        view = GetComponent<PhotonView>();
        characterController = GetComponent<CharacterController>();
        radius = characterController.radius;
        originalRadius = characterController.radius;
        isDead = false;
        // healthAura = this.gameObject.transform.Find("Geometry/HealingAura").GetComponent<ParticleSystem>();
        // isRevive = false;

        // StartCoroutine(ApplyArmor());
        // playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isRevive", false);
            isDead = true;
            thirdPersonController.enabled = false;
            playerHud.SetActive(false);
            radius = 1.05f;
            
            characterController.radius = radius;
            // Debug.Log("ISDEAD: " + isDead);
        }
        // Debug.Log("CURRENT HEALTH: " + currentHealth);
    }

    [PunRPC]
    public void RPC_TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            currentHealth = 0;
            if(!isDead && view.IsMine)
            {
                LeaderboardData data = LeaderboardManager.manager.GetPlayerLeaderboardData(PhotonNetwork.LocalPlayer.ActorNumber);
                data.deathCount++;
                data.killStreak = 0;
                LeaderboardManager.manager.SetPlayerLeaderboardData(PhotonNetwork.LocalPlayer.ActorNumber, data);
                LeaderboardManager.manager.RefreshLeaderboardData();
                isDead = true;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        TriggerUpdateHealth(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        view.RPC(nameof(RPC_TakeDamage), PhotonNetwork.LocalPlayer, damage);
    }

    public void RestoreHealth(float healAmount)
    {
       if (currentHealth <= initialHealth) {
            if (currentHealth + healAmount >= initialHealth)
            {
                currentHealth = initialHealth;
            }
            else
            {
                currentHealth += healAmount;
            }
       }

        TriggerUpdateHealth(currentHealth);
        
    }

    public void AddArmor(float armorAmount)
    {
        if (currentHealth + armorAmount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += armorAmount;
        }

        TriggerUpdateHealth(currentHealth);
    }

    // public void PlayerRevive() {
    //     Debug.Log("ISREVIVE: " + isRevive);

    //     animator.SetBool("isRevive", true);
    //     thirdPersonController.enabled = true;
    //     playerHud.SetActive(true);
    //     // isDead = false;
    //     // isRevive = false;
        
    //     animator.SetBool("isDead", false);
    //     animator.SetBool("isRevive", false);
    // }
    

    // IEnumerator ApplyArmor() {
    //     yield return new WaitForSeconds(2f);
    //     AddArmor(100);
    // }

    // public void AddArmor(float armorAmount)
    // {
    //     view.RPC(nameof(RPC_AddArmor), RpcTarget.All, armorAmount);
    // }
}
