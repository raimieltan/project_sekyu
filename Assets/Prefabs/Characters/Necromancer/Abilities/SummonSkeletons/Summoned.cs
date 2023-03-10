using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class Summoned : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _character;

    [SerializeField]
    public Transform _playerTarget;

    [SerializeField]
    private float _sightRange = 10f;

    [SerializeField]
    private float _attackRange = 3f;

    [SerializeField]
    private float _followDistance = 3f;

    [SerializeField]
    private float _attackCooldown = 2f;

    private float _nextAttackTime;

    [SerializeField]
    private LayerMask _whatIsGround;

    [SerializeField]
    private LayerMask _whatIsPlayer;

    [SerializeField]
    private Animator _animator;

    private bool _isAttacking;

    [SerializeField]
    private AudioClip _skeletonSound1;

    [SerializeField]
    private AudioClip _skeletonSound2;

    private void Start()
    {
        _character.speed = 3.5f;
        _animator.fireEvents = false;
        InvokeRepeating(nameof(PlaySound), 0.001f, 10f);
    }

    private void PlaySound()
    {
        AudioClip idleSoundToPlay = _skeletonSound1;
        if (_toggleSound)
        {
            idleSoundToPlay = _skeletonSound2;
        }
        AudioSource.PlayClipAtPoint(idleSoundToPlay, transform.position);
        _toggleSound = !_toggleSound;
    }

    private bool _toggleSound = true;

    private void Update()
    {
        // Calculate the AI's speed based on its movement
        float speed = GetComponent<NavMeshAgent>().velocity.magnitude;

        // Set the "speed" parameter in the Animator
        _animator.SetFloat("Speed", speed);

        Collider[] hitColliders =
            Physics.OverlapSphere(_playerTarget.position, _sightRange);

        if (hitColliders.Length > 0)
        {
            Collider nearestEnemyCollider =
                GetNearestEnemyCollider(hitColliders);

            if (nearestEnemyCollider != null)
            {
                _character.speed = 5f;

                // Attack the enemy
                if (!_isAttacking)
                {
                    StartCoroutine(AttackEnemy(nearestEnemyCollider
                        .gameObject));
                }

                return; // only attack one enemy at a time
            }
        }

        _character.speed = 3.5f;
        FollowPlayer();
    }

    private Collider GetNearestEnemyCollider(Collider[] colliders)
    {
        Collider nearestCollider = null;
        float nearestDistance = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            Debug.Log("Tag: " + collider.tag);

            if (collider.gameObject.CompareTag("Player"))
            {
                // Get the PhotonView component of the collided game object
                PhotonView photonView =
                    collider.gameObject.GetComponent<PhotonView>();

                // Check if the collided game object has a PhotonView component
                if (photonView != null)
                {
                    // Get the Player object corresponding to the collided game object
                    Photon.Realtime.Player player = photonView.Owner;

                    if ((bool)(player?.CustomProperties.ContainsKey("team")))
                    {
                        // Get the team name or identifier
                        string otherPlayerTeam =
                            player.CustomProperties["team"] as string;
                        string myTeam =
                            PhotonNetwork
                                .LocalPlayer
                                .CustomProperties["team"] as
                            string;

                        Debug.Log("Triggered Player team: " + otherPlayerTeam);
                        Debug.Log("My team: " + myTeam);

                        if (otherPlayerTeam != myTeam)
                        {
                            Debug.Log("NICE");

                            float distance =
                                Vector3
                                    .Distance(transform.position,
                                    collider.transform.position);
                            if (distance < nearestDistance)
                            {
                                nearestDistance = distance;
                                nearestCollider = collider;
                            }
                        }
                    }
                }
            }
        }
        return nearestCollider;
    }

    private void ChaseEnemy(GameObject enemy)
    {
        _character.SetDestination(enemy.transform.position);
    }

    private IEnumerator AttackEnemy(GameObject enemy)
    {
        _isAttacking = true;

        Debug.Log("Enemy found: " + enemy.name);

        LookCharacter(enemy.transform);

        float distance =
            Vector3.Distance(transform.position, enemy.transform.position);

        Debug.Log("Distance to enemy: " + distance);

        if (distance < _attackRange)
        {
            _animator.SetTrigger("Attack");
            Debug.Log("Enemy taking damage: " + enemy.name);
            _character.SetDestination(transform.position);
        }
        else
        {
            ChaseEnemy (enemy);
        }

        yield return new WaitForSeconds(_attackCooldown);

        _isAttacking = false;
    }

    private void FollowPlayer()
    {
        float distance =
            Vector3.Distance(transform.position, _playerTarget.position);

        if (distance < _followDistance)
        {
            LookCharacter (_playerTarget);
            _character.SetDestination(transform.position);
        }
        else
        {
            _character.SetDestination(_playerTarget.position);
        }
    }

    private void LookCharacter(Transform target)
    {
        if (target == null) return;
        Vector3 targetPosition =
            new Vector3(target.position.x,
                this.transform.position.y,
                target.position.z);

        transform.LookAt (targetPosition);
    }
}
