using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public float force;
    public float torque;
    private void Awake() {

        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {

        // bulletRigidbody.velocity = transform.forward * speed;
        bulletRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
        bulletRigidbody.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "trap") {
            Destroy(gameObject);
        }
    }
}
