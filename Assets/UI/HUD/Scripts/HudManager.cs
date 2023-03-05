using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    // player variables
    Health playerHealth;

    Ability ability1;

    Ability ability2;

    PlayerInventory playerInventory;

    PlayerHudVariables hudVariables;

    // canvas variables
    public GameObject player;

    public Image ability1BackgroundImg;

    public Image ability1FilledImg;

    public TMP_Text ability1CooldownText;

    public Image ability2BackgroundImg;

    public Image ability2FilledImg;

    public TMP_Text ability2CooldownText;

    public Image flashbangFilledImg;

    public TMP_Text flashbangCooldownText;

    public TMP_Text flashbangQuantityText;

    public Image smokeFilledImg;

    public TMP_Text smokeCooldownText;

    public TMP_Text smokeQuantityText;

    public Image explosiveTrapFilledImg;

    public TMP_Text explosiveTrapCooldownText;

    public TMP_Text explosiveQuantityText;

    public Image poisonTrapFilledImg;

    public TMP_Text poisonTrapCooldownText;

    public TMP_Text poisonQuantityText;

    public Image slowTrapFilledImg;

    public TMP_Text slowTrapCooldownText;

    public TMP_Text slowQuantityText;

    public Image playerImg;

    public HealthBar healthbar;

    public GameObject[] playerPrefab;

    // public HealthBar armorBar;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log (player);
        hudVariables = player.GetComponent<PlayerHudVariables>();

        // setup character icon
        playerImg.sprite = hudVariables.PlayerIcon;

        // setup ability 1
        ability1 = hudVariables.Ability1;
        ability1.TriggerAbilityFired += StartAbility1Cooldown;
        ability1BackgroundImg.sprite = hudVariables.Ability1Image;
        ability1FilledImg.sprite = hudVariables.Ability1Image;
        ability1FilledImg.fillAmount = 0;

        // setup ability 2
        ability2 = hudVariables.Ability2;
        ability2.TriggerAbilityFired += StartAbility2Cooldown;
        ability2BackgroundImg.sprite = hudVariables.Ability2Image;
        ability2FilledImg.sprite = hudVariables.Ability2Image;
        ability2FilledImg.fillAmount = 0;

        // setup healthbar
        playerHealth = player.GetComponent<Health>();
        healthbar.healthReference = playerHealth;

        // setup armor bar
        // armorBar.healthReference = playerHealth;
        // setup inventory variables
        playerInventory = player.GetComponent<PlayerInventory>();
        playerInventory.OnFlashbangFired += StartFlashbangCooldown;
        playerInventory.OnSmokeFired += StartSmokeCooldown;
        playerInventory.OnPlaceExplosiveTrap += StartPlaceExplosiveCooldown;
        playerInventory.OnPlacePoisonTrap += StartPlacePoisonCooldown;
        playerInventory.OnPlaceSlowTrap += StartPlaceSlowCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        // handle ability 1 cooldown
        handleImageCooldown(ability1.cooldownTime,
        ability1.nextFireTime,
        ability1FilledImg,
        ability1CooldownText);

        // handle ability 2 cooldown
        handleImageCooldown(ability2.cooldownTime,
        ability2.nextFireTime,
        ability2FilledImg,
        ability2CooldownText);

        // handle inventory items cooldowns
        handleImageCooldown(playerInventory.itemCooldown,
        playerInventory.nextFlashbangTime,
        flashbangFilledImg,
        flashbangCooldownText);
        handleImageCooldown(playerInventory.itemCooldown,
        playerInventory.nextSmokeTime,
        smokeFilledImg,
        smokeCooldownText);
        handleImageCooldown(playerInventory.itemCooldown,
        playerInventory.nextExplosiveTrapTime,
        explosiveTrapFilledImg,
        explosiveTrapCooldownText);
        handleImageCooldown(playerInventory.itemCooldown,
        playerInventory.nextPoisonTrapTime,
        poisonTrapFilledImg,
        poisonTrapCooldownText);
        handleImageCooldown(playerInventory.itemCooldown,
        playerInventory.nextSlowTrapTime,
        slowTrapFilledImg,
        slowTrapCooldownText);
    }

    private void handleImageCooldown(
        float cooldownTime,
        float nextFireTime,
        Image filledImg,
        TMP_Text cooldownText
    )
    {
        if (
            cooldownTime == 0f // check for division by zero
        )
        {
            return;
        }

        float fillAmount = (nextFireTime - Time.time) / cooldownTime;
        fillAmount = Mathf.Clamp(fillAmount, 0f, 1f); // clamp the fill amount between 0 and 1

        filledImg.fillAmount = fillAmount;

        if (
            cooldownText != null && fillAmount > 0f // check for null cooldownText
        )
        {
            cooldownText.text = ((nextFireTime - Time.time)).ToString("F1"); // simplify string formatting
        }
        else if (cooldownText != null)
        {
            cooldownText.text = "";
        }
    }

    private void StartAbility1Cooldown()
    {
        ability1FilledImg.fillAmount = 1;
    }

    private void StartAbility2Cooldown()
    {
        ability2FilledImg.fillAmount = 1;
    }

    private void StartFlashbangCooldown()
    {
        flashbangFilledImg.fillAmount = 1;
    }

    private void StartSmokeCooldown()
    {
        smokeFilledImg.fillAmount = 1;
    }

    private void StartPlaceExplosiveCooldown()
    {
        explosiveTrapFilledImg.fillAmount = 1;
    }

    private void StartPlacePoisonCooldown()
    {
        poisonTrapFilledImg.fillAmount = 1;
    }

    private void StartPlaceSlowCooldown()
    {
        slowTrapFilledImg.fillAmount = 1;
    }

    private void UpdateHealthBar(float newHealthValue)
    {
        healthbar.SetHealth (newHealthValue);
    }
}
