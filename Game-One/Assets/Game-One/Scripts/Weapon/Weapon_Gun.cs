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


        public void Fire(float yAxisAngle)
        {
            // bullet을 마우스 포인터 방향으로 발사하기 위해 rotation 설정
            firePoint.rotation = Quaternion.Euler(0f, yAxisAngle, 0f);
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.gameObject.SetActive(true);

            Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(firePoint.forward * 15f, ForceMode.Impulse);
        }
    }
}
