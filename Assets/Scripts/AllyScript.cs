using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour
{

    private Animator animator;
    private int health;
    private bool isDead;
    private Transform root;
    private CapsuleCollider collider;

    private Vector3 center;

    private void Start() {
        health = 100;
        isDead = false;
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        center = collider.center;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "bullet" && !isDead)
        {
            health -= 20;

            if (health < 0) {
                isDead = true;
                animator.SetTrigger("isDead");
                collider.direction = 2;
                Vector3 center = collider.center;
                center.y = 0.5f;
                center.z = -0.5f;
                
                collider.center = center;
            }
        }
        else if (other.gameObject.tag == "revive" && isDead)
        {
            health = 100;
            isDead = false;
            animator.SetTrigger("isRevive");
            animator.ResetTrigger("isDead");
            collider.direction = 1;

            center.y = 1f;
            center.z = 0f;
                
            collider.center = center;
        }
        Debug.Log(isDead);
    }
}
    