using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class SphereEffect : MonoBehaviour
    {
        public GameObject spherePrefab; // 생성할 Sphere 프리팹
        public float spawnInterval = 0.01f; // Sphere 생성 주기
        public float minForce = 3f; // 최소 발사 힘
        public float maxForce = 6f; // 최대 발사 힘
        public float angleSpread = 30f; // 발사 각도 범위


        public void StartCreateSpheres()
        {
            InvokeRepeating(nameof(SpawnSphere), 0f, spawnInterval);
        }
        public void EndCreateSpheres()
        {
            CancelInvoke(nameof(SpawnSphere));
        }

        public void SpawnSphere()
        {
            // Sphere 생성
            GameObject sphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
            sphere.SetActive(true);

            // Rigidbody 추가 및 위로 힘 가하기
            Rigidbody rb = sphere.AddComponent<Rigidbody>();
            rb.useGravity = true; // 중력 활성화 (선택)

            // 랜덤 방향 계산
            Vector3 randomDirection = GetRandomDirection();

            // 랜덤 힘 계산
            float randomForce = Random.Range(minForce, maxForce);

            // 위로 랜덤한 힘 적용
            rb.AddForce(randomDirection * randomForce, ForceMode.Impulse);

            // 일정 시간 후 Sphere 삭제
            Destroy(sphere, 1f);
        }

        Vector3 GetRandomDirection()
        {
            // 기본 Up 방향 벡터
            Vector3 baseDirection = Vector3.up;

            // 랜덤한 각도로 벡터 회전
            float randomX = Random.Range(-angleSpread, angleSpread);
            float randomZ = Random.Range(-angleSpread, angleSpread);

            // XZ 축으로 회전 벡터 생성
            Quaternion randomRotation = Quaternion.Euler(randomX, 0f, randomZ);

            // 회전 적용
            return randomRotation * baseDirection;
        }
    }
}
