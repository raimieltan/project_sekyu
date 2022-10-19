using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif


namespace StarterAssets
{
    public class SummonSkeletons : MonoBehaviour
    {
        [SerializeField]
        GameObject skeleton;

        private Animator anim;

        private StarterAssetsInputs _input;

        private Summoned summoned;

        private bool castingSpell = false;

        private float timer = 0;

        void Awake()
        {
            summoned = skeleton.GetComponent<Summoned>();
            anim = skeleton.GetComponent<Animator>();
            _input = GetComponent<StarterAssetsInputs>();
        }

        void Update()
        {
            if (_input.magic && !castingSpell)
            {
                castingSpell = true;
                anim.Play("cast spell");
                timer = 0;

                _input.magic = false;
            }

            if (timer > 2 && timer < 4 && castingSpell)
            {
                summonGameObject();
            }

            timer += Time.deltaTime;
        }

        void summonGameObject()
        {
            Vector3 randomSpawnPosition =
                new Vector3(transform.position.x + Random.Range(-3, 3),
                    2,
                    (transform.position.z + Random.Range(-3, 3)));

            summoned.PlayerTarget = transform;
            Instantiate(skeleton, randomSpawnPosition, Quaternion.identity);
            castingSpell = false;
        }
    }
}
