using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealProjectile : MonoBehaviour
{
    private Rigidbody healRigidbody;
    public float force;
    public float torque;
    private void Awake() {
    
        healRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {

        // healRigidbody.velocity = transform.forward * speed;
        healRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
        healRigidbody.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Tags>(out var tags)) {
            if (tags.HasTag("Ally")){
                float maxHealth = other.GetComponent<Health>().maxHealth;
                other.GetComponent<Health>().RestoreHealth(maxHealth * 0.3f);
            }
        }

        Destroy(gameObject);
    }
}
