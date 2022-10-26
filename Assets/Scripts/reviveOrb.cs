using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reviveOrb : MonoBehaviour
{
    private Rigidbody orbRigidBody;
    public float force;
    public float torque;
    private void Awake() {
    
        orbRigidBody = GetComponent<Rigidbody>();
    }

    private void Start() {

        orbRigidBody.AddForce(transform.forward * force, ForceMode.Impulse);
        orbRigidBody.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
