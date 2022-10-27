using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  public float maxHealth;
  public float currentHealth;
  public HealthBar healthBar;

  // Start is called before the first frame update
  void Start()
  {
    currentHealth = maxHealth;
    healthBar.SetMaxHealth(maxHealth);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      TakeDamage(10);
    }

    if (currentHealth <= 0)
    {
      Debug.Log("Dead");
    }
  }

  void TakeDamage(float damage)
  {
    if (currentHealth - damage <= 0)
    {
      currentHealth = 0;
    }
    else
    {
      currentHealth -= damage;
    }


    healthBar.SetHealth(currentHealth);
  }

  public void RestoreHealth(float healAmount)
  {
    if (currentHealth + healAmount >= maxHealth)
    {
      currentHealth = maxHealth;
    }
    else
    {
      currentHealth += healAmount;
    }

    healthBar.SetHealth(currentHealth);
  }
}
