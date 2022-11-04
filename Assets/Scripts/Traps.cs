using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using CartoonFX;

public class Traps : MonoBehaviour
{
    float interval = 5;
    float poisonDmg = 10f;
    float poisonTime = 3f;
    bool insideTrap;
    // float dmgInterval = 3;
    [SerializeField] private ThirdPersonController _player;
    // [SerializeField] private CFXR_Effect _effects;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("SlowTrap") == null) {
            returnMovementSpeed();
        }
    }

    void OnTriggerEnter(Collider col) {
        if(transform.name == "SlowTrap") {
            if(col.gameObject.name == "PlayerArmature") {
                _player.MoveSpeed = 0.5f;
                _player.SprintSpeed = 0.5f;
                Destroy(transform.gameObject, interval);
            }
        } else if (transform.name == "DarkMagic" || transform.name == "FireTrap"){
            if(col.gameObject.name == "PlayerArmature") {
                DamageOverTime(poisonDmg, poisonTime);
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if(transform.name == "SpikeTrapD") {
            if(col.gameObject.name == "PlayerArmature") {
                _player.MoveSpeed = 2f;
                _player.SprintSpeed = 5.335f;
            }
        } else if (transform.name == "SpikeTrapPoison" || transform.name == "DarkMagic"){
            if(col.gameObject.name == "PlayerArmature") {
                DamageOverTime(poisonDmg, poisonTime);
            }
        }
    }

    public void DamageOverTime(float dmgAmount, float dmgTime) {
        StartCoroutine(DamageOverTimeCoroutine(dmgAmount, dmgTime));
    }

    IEnumerator DamageOverTimeCoroutine(float dmgAmount, float duration)  {
        float amountDamaged = 0;
        float damagePerLoop = dmgAmount / duration;
        while (amountDamaged < dmgAmount) {
            _player.currentHealth -= damagePerLoop;
            _player.healthBar.UpdateHealthBar(_player.maxHealth, _player.currentHealth);
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
    }

    void returnMovementSpeed() {
        _player.MoveSpeed = 2f;
        _player.SprintSpeed = 5.335f;
    }
}
