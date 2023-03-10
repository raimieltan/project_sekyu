using System.Collections;
using Photon.Pun;
using StarterAssets;
using UnityEngine;

public class SummonSkeletons : Ability
{
    [SerializeField]
    GameObject skeletonPrefab;

    private Animator anim;

    private StarterAssetsInputs _input;

    private int skeletonCount = 0;

    private float cooldownTime = 8f;

    private bool isOnCooldown = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (_input.firstAbility && !isOnCooldown)
        {
            StartCoroutine(CastSummonSkeletons());
        }
    }

    private IEnumerator CastSummonSkeletons()
    {
        isOnCooldown = true;
        anim.Play("cast spell");
        yield return new WaitForSeconds(2f);
        if (skeletonCount < 4)
        {
            Vector3 randomSpawnPosition =
                new Vector3(transform.position.x + Random.Range(-3f, 3f),
                    transform.position.y,
                    transform.position.z + Random.Range(-3f, 3f));

            // Instantiate the object and set its properties
            GameObject skeletonObject =
                PhotonNetwork
                    .Instantiate(skeletonPrefab.name,
                    randomSpawnPosition,
                    Quaternion.identity);

            Summoned summoned = skeletonObject.GetComponent<Summoned>();

            summoned._playerTarget = transform;

            skeletonCount++;
            StartCoroutine(DestroySkeleton(skeletonObject));
        }
        yield return new WaitForSeconds(cooldownTime - 2f);
        isOnCooldown = false;
    }

    private IEnumerator DestroySkeleton(GameObject skeletonObject)
    {
        yield return new WaitForSeconds(10f);
        Destroy (skeletonObject);
        skeletonCount--;
    }
}
