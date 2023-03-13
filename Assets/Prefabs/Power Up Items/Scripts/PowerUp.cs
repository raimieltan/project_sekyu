using System.Collections;
using UnityEngine;

namespace StarterAssets
{
    public class PowerUp : MonoBehaviour
    {
        [Header("Power Up Multiplier:")]
        [Range(1f, 3f)]
        [SerializeField]
        private float speed = 2f;

        [Range(1f, 2f)]
        [SerializeField]
        private float damage = 2f;

        [Header("Health Fill %")]
        [Range(0.0f, 100f)]
        [SerializeField]
        private float health = 100f;

        [Header("Effect Timeout")]
        [Range(0.0f, 30f)]
        [SerializeField]
        private float timeout = 5f;

        [SerializeField]
        private GameObject powerUpEffect;

        private bool isPickedUp = false;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameObject player = other.gameObject;
                ThirdPersonController playerController =
                    player.GetComponentInChildren<ThirdPersonController>();

                Health playerHealth = player.GetComponentInChildren<Health>();
                Damage playerDamage = player.GetComponentInChildren<Damage>();

                if (!isPickedUp)
                {
                    pickup();
                    StartCoroutine(applySpeedUp(playerController));
                    StartCoroutine(applyDoubleDamage(playerDamage));
                    applyHealth (playerHealth);
                    isPickedUp = true;
                }
            }
        }

        void pickup()
        {
            ParticleSystem[] powerUpEffectChildren =
                powerUpEffect.GetComponentsInChildren<ParticleSystem>();

            MeshRenderer[] meshRenderers =
                GetComponentsInChildren<MeshRenderer>();

            for (var i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].enabled = false;
            }

            for (var i = 0; i < powerUpEffectChildren.Length; i++)
            {
                disableEffectLooping(powerUpEffectChildren[i]);
            }

            GameObject effect =
                Instantiate(powerUpEffect,
                transform.position,
                transform.rotation);
        }

        IEnumerator applySpeedUp(ThirdPersonController controller)
        {
            float maxSpeed = 10;

            if (controller.MoveSpeed * this.speed <= maxSpeed)
            {
                float defaultMoveSpeed = controller.MoveSpeed;
                float defaultSprintSpeed = controller.SprintSpeed;

                controller.MoveSpeed = controller.MoveSpeed * this.speed;
                controller.SprintSpeed = controller.SprintSpeed * this.speed;

                yield return new WaitForSeconds(timeout);

                controller.MoveSpeed = controller.MoveSpeed / this.speed;
                controller.SprintSpeed = controller.SprintSpeed / this.speed;
                Destroy (gameObject);
            }
        }

        void applyHealth(Health health)
        {
            health.RestoreHealth(this.health);
        }

        IEnumerator applyDoubleDamage(Damage damage)
        {
            if (!damage.isDoubleDamage)
            {
                float defaultDamgeValue = damage.value;
                damage.value *= this.damage;
                damage.isDoubleDamage = true;

                yield return new WaitForSeconds(timeout);

                damage.value = defaultDamgeValue;
                damage.isDoubleDamage = false;

                Destroy (gameObject);
            }
        }

        void disableEffectLooping(ParticleSystem effect)
        {
            var main = effect.main;
            main.loop = false;
            main.stopAction = ParticleSystemStopAction.Destroy;
        }
    }
}
