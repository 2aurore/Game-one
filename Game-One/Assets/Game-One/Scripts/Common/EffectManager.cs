using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    [System.Serializable]
    public class EffectData
    {
        public string effectId;
        public GameObject effectPrefab;
        public float lifeTime;
    }

    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance { get; private set; }
        public List<EffectData> effectContainer = new List<EffectData>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public GameObject GetEffect(string effectId)
        {
            var targetEffect = effectContainer.Find(x => x.effectId == effectId);
            var newEffect = Instantiate(targetEffect.effectPrefab);

            if (targetEffect.lifeTime > 0)
            {
                Destroy(newEffect, targetEffect.lifeTime);
            }

            return newEffect;
        }
    }
}
