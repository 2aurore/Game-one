using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class Projectile : MonoBehaviour
    {
        public float damage = 1f;
        public float lifeTime = 5f;

        private void OnEnable()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.TryGetComponent(out IDamage damageInterface))
            {
                damageInterface.ApplyDamage(damage);
                Destroy(gameObject, 0.5f);
            }

        }

    }
}
