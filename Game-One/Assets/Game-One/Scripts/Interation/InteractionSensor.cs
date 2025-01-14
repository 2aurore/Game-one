using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class IntractionSensor : MonoBehaviour
    {
        public float sensorRadius = 3f;
        public LayerMask sensorLayers;

        public List<GameObject> detectedObjects = new List<GameObject>();
        private List<IBox> detectedPickupItems = new List<IBox>();

        private void Update()
        {

            detectedObjects.Clear();
            detectedPickupItems.Clear();
            Collider[] overlapped = Physics.OverlapSphere(transform.position, sensorRadius, sensorLayers);

            for (int i = 0; i < overlapped.Length; i++)
            {
                IBox pickupInterface = overlapped[i].transform.root.GetComponent<IBox>();
                if (pickupInterface != null)
                {
                    detectedObjects.Add(overlapped[i].transform.root.gameObject);
                    detectedPickupItems.Add(pickupInterface);
                }
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                for (int i = 0; i < detectedPickupItems.Count; i++)
                {
                    detectedPickupItems[i].Open();
                }
            }
        }
    }
}
