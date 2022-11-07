using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour
{

    private Animator animator;
    private int health;
    private bool isDead;

    private void Start() {
        health = 100;
        isDead = false;
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "bullet" && !isDead)
        {
            health -= 20;

            if (health < 0) {
                isDead = true;
                animator.SetTrigger("isDead");
            }
        }
        else if (other.gameObject.tag == "revive" && isDead)
        {
            health = 100;
            isDead = false;
            animator.SetTrigger("isRevive");
            animator.ResetTrigger("isDead");
        }
        Debug.Log(isDead);
    }
}
    