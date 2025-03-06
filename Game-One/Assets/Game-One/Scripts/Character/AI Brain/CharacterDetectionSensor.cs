using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class CharacterDetectionSensor : MonoBehaviour
    {

        public event System.Action<CharacterBase> OnDetectedCharacter;
        public event System.Action<CharacterBase> OnLostCharacter;

        public List<CharacterBase> detectedCharacter = new List<CharacterBase>();

        private void OnTriggerEnter(Collider other)
        {

            // 최적화를 위해 사용할 수 있음
            // if (other.CompareTag("Player"))
            
            if (other.transform.root.TryGetComponent(out CharacterBase characterBase))
            {
                Debug.Log(characterBase.gameObject.name);
                if (false == detectedCharacter.Contains(characterBase))
                {
                    detectedCharacter.Add(characterBase);
                    OnDetectedCharacter?.Invoke(characterBase);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.root.TryGetComponent(out CharacterBase characterBase))
            {
                if (detectedCharacter.Contains(characterBase))
                {
                    detectedCharacter.Remove(characterBase);
                    OnLostCharacter?.Invoke(characterBase);
                }
            }
        }
    }
}
