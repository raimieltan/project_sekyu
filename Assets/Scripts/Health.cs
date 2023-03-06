using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;

    public float initialHealth;

    public float currentHealth;

    public float armorAmount;

    public delegate void UpdateHealth(float newHealth);

    public event UpdateHealth TriggerUpdateHealth;

    private PlayerInventory playerInventory;

    private PhotonView view;

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

        // StartCoroutine(ApplyArmor());
        // playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            thirdPersonController.enabled = false;
            playerHud.SetActive(false);
        }
        // Debug.Log("CURRENT HEALTH: " + currentHealth);
    }

    [PunRPC]
    public void RPC_TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }

        Debug.Log("TAKE DAMAGE:" + currentHealth);

        TriggerUpdateHealth(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        view.RPC(nameof(RPC_TakeDamage), PhotonNetwork.LocalPlayer, damage);
    }

    public void RestoreHealth(float healAmount)
    {
        if (currentHealth + healAmount >= initialHealth)
        {
            currentHealth = initialHealth;
        }
        else
        {
            currentHealth += healAmount;
        }

        TriggerUpdateHealth(currentHealth);
    }

    // [PunRPC]
    public void AddArmor(float armorAmount)
    {
        Debug.Log("ARMOR AMOUNT: " + armorAmount);

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

    // IEnumerator ApplyArmor() {
    //     yield return new WaitForSeconds(2f);
    //     AddArmor(100);
    // }

    // public void AddArmor(float armorAmount)
    // {
    //     view.RPC(nameof(RPC_AddArmor), RpcTarget.All, armorAmount);
    // }
}
