using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Summoned : MonoBehaviour
{
    private NavMeshAgent character;
    public Transform PlayerTarget;
    public float sightRange;
    public float attackRange;
    public float followDistance;
    public float attackCooldown;
    public float nextAttackTime;

    public bool

            enemyInSightRange,
            enemyInAttackRange;

    public LayerMask

            whatIsGround,
            whatIsPlayer;

    private Animator animator;

    public bool isAttacking;

    public AudioClip skeletonSound1;
    public AudioClip skeletonSound2;
    private bool toggleSound = true;
    private AudioClip idleSoundToPlay;

    void Start()
    {
        character = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sightRange = 20f;
        attackRange = 1.5f;
        followDistance = 3f;
        attackCooldown = 2f;
        nextAttackTime = 0f;
        InvokeRepeating("PlaySound", 0.001f, 10f);
    }

    void PlaySound()
    {
        if (toggleSound)
        {
            idleSoundToPlay = skeletonSound1;
        }
        else
        {
            idleSoundToPlay = skeletonSound2;
        }

        AudioSource.PlayClipAtPoint(idleSoundToPlay, transform.position);
        toggleSound = !toggleSound;
    }

    void Update()
    {
        Collider[] colliders =
            Physics.OverlapSphere(PlayerTarget.position, sightRange);
        foreach (Collider c in colliders)
        {
            if (c.TryGetComponent<Tags>(out var tags))
            {
                if (tags.HasTag("Enemy"))
                {
                    character.speed = 5f;

                    float distance =
                        Vector3
                            .Distance(transform.position, c.transform.position);

                    if (distance <= attackRange)
                    {
                        isAttacking = true;
                        AttackEnemy(c);
                        return;
                    }
                    else
                    {
                        isAttacking = false;
                        ChaseEnemy(c);
                        return;
                    }
                }
            }
        }

        character.speed = 3.5f;
        isAttacking = false;
        character.SetDestination(PlayerTarget.position);
        FollowPlayer();
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", character.velocity.magnitude);
        animator.SetBool("Attacking", isAttacking);
    }

    private void ChaseEnemy(Collider enemy)
    {
        character.SetDestination(enemy.transform.position);
    }

    private void AttackEnemy(Collider enemy)
    {
        character.SetDestination(transform.position);

        Health enemyHealth = enemy.gameObject.GetComponent<Health>();

        LookCharacter(enemy.transform);

        if (Time.time > nextAttackTime)
        {
            enemyHealth.TakeDamage(5);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void FollowPlayer()
    {
        float distance =
            Vector3
                .Distance(transform.position, PlayerTarget.position);

        if (distance < followDistance)
        {
            LookCharacter(PlayerTarget);
            character.SetDestination(transform.position);
        }
        else
        {
            isAttacking = false;
            character.SetDestination(PlayerTarget.position);
        }

    }

    private void LookCharacter(Transform charTransform)
    {
        Vector3 targetPosition = new Vector3(charTransform.position.x, this.transform.position.y, charTransform.position.z);
        transform.LookAt(targetPosition);
    }
}