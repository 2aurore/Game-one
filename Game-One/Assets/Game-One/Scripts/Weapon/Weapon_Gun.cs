using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class Weapon_Gun : MonoBehaviour, IWeapon
    {
        public float fireRate = 1f;
        public GameObject bullet;
        public Transform firePoint;


        public void Fire()
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.gameObject.SetActive(true);

            Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(firePoint.forward * 10f, ForceMode.Impulse);
        }
    }
}
