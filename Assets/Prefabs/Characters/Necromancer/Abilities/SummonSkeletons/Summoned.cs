using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Summoned : MonoBehaviour
{
  private NavMeshAgent character;

  public Transform PlayerTarget;
  // Start is called before the first frame update

  public float sightRange = 50f;
  public float attackRange = 1f;
  public bool enemyInSightRange, enemyInAttackRange;

  public LayerMask whatIsGround, whatIsPlayer;



  void Start()
  {
    character = GetComponent<NavMeshAgent>();
  }

  // Update is called once per frame
  void Update()
  {

    Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange);
    foreach (Collider c in colliders)
    {
      if (c.TryGetComponent<Tags>(out var tags))
      {
        if (tags.HasTag("Enemy"))
        {
          ChaseEnemy(c);
          return;
          //   if (c.GetComponent<Health>())
          //   {
          //     float maxHealth = c.GetComponent<Health>().maxHealth;
          //     c.GetComponent<Health>().RestoreHealth(maxHealth * 0.3f);
          //   }
        }
      }
    }

    FollowPlayer();


    // enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    // enemyInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

    // if (!enemyInSightRange && !enemyInAttackRange) FollowPlayer();
    // if (enemyInSightRange && !enemyInAttackRange) ChaseEnemy();
    // if (enemyInSightRange && enemyInAttackRange) AttackEnemy();
  }

  private void ChaseEnemy(Collider enemy)
  {
    Debug.Log("chase enemy");
    character.SetDestination(enemy.transform.position);
  }

  private void AttackEnemy()
  {
    Debug.Log("attack");
    // character.SetDestination(enemy.position);
    // transform.LookAt(enemy);

    // if (!)



  }

  private void FollowPlayer()
  {
    character.SetDestination(PlayerTarget.position);
  }
}


