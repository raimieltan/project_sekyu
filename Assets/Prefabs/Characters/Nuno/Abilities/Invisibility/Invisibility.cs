using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
  public int healAmount = 20;
  public float cooldownTime = 3;
  private float nextHealTime = 0;
//   public float range = 7f;
//   public Image abilityImage;
//   public AudioClip healSound;
//   AudioSource audioSource;

  void Start()
  {
    // abilityImage.fillAmount = 0;
    // audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    if (Time.time > nextHealTime)
    {
        // Debug.Log("INSIDE");
        // Debug.Log(Time.time);
      if (Input.GetKeyDown(KeyCode.I))
      {
        Debug.Log("I is pressed");
        // nextHealTime = Time.time + cooldownTime;
        // abilityImage.fillAmount = 1;
        // audioSource.PlayOneShot(healSound);
        GoInvisible();
      }
    }
    else
    {
        // Debug.Log("outSIDE");
        // Debug.Log(Time.time);
    //   abilityImage.fillAmount -= 1 / cooldownTime * Time.deltaTime;

    //   if (abilityImage.fillAmount <= 0)
    //   {
    //     abilityImage.fillAmount = 0;
    //   }
    }


  }

    private void GoInvisible() {

    }

//   private void HealNearbyAllies()
//   {
//     Collider[] colliders = Physics.OverlapSphere(transform.position, range);
//     foreach (Collider c in colliders)
//     {
//       if (c.TryGetComponent<Tags>(out var tags))
//       {
//         if (tags.HasTag("Ally"))
//         {
//           if (c.GetComponent<Health>())
//           {
//             float maxHealth = c.GetComponent<Health>().maxHealth;
//             c.GetComponent<Health>().RestoreHealth(maxHealth * 0.3f);
//           }
//         }
//       }
//     }
//   }
}
