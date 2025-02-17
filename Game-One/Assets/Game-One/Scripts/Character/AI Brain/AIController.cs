using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ONE
{
    public class AIController : MonoBehaviour
    {
        private CharacterBase linkedCharactor;
        private NavMeshAgent navAgent;
        private bool isDestinationReached = false;

        public event System.Action OnDestinationReached;

        private void Awake()
        {
            linkedCharactor = GetComponent<CharacterBase>();
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            // Agent가 목적지에 도착했는지 확인하고, 도착했다면 ? => OnDestinationReached 이벤트 발생생
            if (navAgent.pathStatus == NavMeshPathStatus.PathComplete || navAgent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                // pathStatys : Agent가 Path 정보를 계산했을때, 제대로 된 경로를 추출했는지 여부 값
                // pathStatus == PathComplete : Agent가 목적지까지의 경로를 제대로 계산했을 때 
                //              (Destination 까지의 경로를 완전히 도달할 수 있을때)를 의미함
                // pathStatus == PathPartial : Agent가 목적지까지의 경로를 목적지에 완전히 도달할 수는 없지만 
                //              부분적으로라도 갈 수 있을때를 의미함

                // 조심해야하는 부분 : stoppingDistance이 0이면 안됨, 0.1f 이상으로 할 것것
                if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                {
                    if (isDestinationReached == false)
                    {
                        isDestinationReached = true;
                        OnDestinationReached?.Invoke();
                    }
                }
            }
        }


        public void SetDestination(Vector3 position)
        {
            linkedCharactor.SetDestination(position);
        }
    }
}
