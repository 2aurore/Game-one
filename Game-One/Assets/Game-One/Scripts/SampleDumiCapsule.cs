using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class SampleDumiCapsule : MonoBehaviour, IDamage
    {
        public float maxHealth = 100;
        public float curHealth = 0;

        private void Start()
        {
            curHealth = maxHealth;
        }
        public void ApplyDamage(float damage)
        {
            curHealth -= damage;

            if (curHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
