using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class InteractionSensor : MonoBehaviour
    {
        public float sensorRadius = 3f;
        public LayerMask sensorLayers;

        public List<IBox> detectedPickupItems = new List<IBox>();

        private void Update()
        {
            detectedPickupItems.Clear();
            Collider[] overlapped = Physics.OverlapSphere(transform.position, sensorRadius, sensorLayers);

            for (int i = 0; i < overlapped.Length; i++)
            {
                IBox pickupInterface = overlapped[i].transform.root.GetComponent<IBox>();
                if (pickupInterface != null)
                {
                    detectedPickupItems.Add(pickupInterface);
                }
            }

        }

    }
}
