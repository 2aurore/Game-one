using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ONE
{
    [System.Serializable]
    public class EffectData
    {
        public string effectId;
        public GameObject effectPrefab;
        // 몇개를 미리 생성 해놓을건지
        public int count;
    }


    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance { get; private set; }
        public List<EffectData> effectContainer = new List<EffectData>();

        // 오브젝트풀 매니저 준비 완료표시
        public bool IsReady { get; private set; }

        // 생성할 오브젝트의 key값지정을 위한 변수
        private string objectName;


        // 오브젝트풀들을 관리할 딕셔너리
        public SerializableDictionary<string, IObjectPool<GameObject>>
        ojbectPoolDic = new SerializableDictionary<string, IObjectPool<GameObject>>();

        // 오브젝트풀에서 오브젝트를 새로 생성할때 사용할 딕셔너리
        private SerializableDictionary<string, GameObject> poolGoDic = new SerializableDictionary<string, GameObject>();


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

            Init();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Init()
        {
            IsReady = false;

            for (int idx = 0; idx < effectContainer.Count; idx++)
            {
                IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject, true, effectContainer[idx].count, effectContainer[idx].count);

                if (poolGoDic.ContainsKey(effectContainer[idx].effectId))
                {
                    Debug.LogFormat("{0} 이미 등록된 오브젝트입니다.", effectContainer[idx].effectId);
                    return;
                }

                poolGoDic.Add(effectContainer[idx].effectId, effectContainer[idx].effectPrefab);
                ojbectPoolDic.Add(effectContainer[idx].effectId, pool);

                // 미리 오브젝트 생성 해놓기
                for (int i = 0; i < effectContainer[idx].count; i++)
                {
                    objectName = effectContainer[idx].effectId;
                    PoolAble poolAbleGo = CreatePooledItem().GetComponent<PoolAble>();
                    poolAbleGo.Pool.Release(poolAbleGo.gameObject);
                }
            }

            Debug.Log("오브젝트풀링 준비 완료");
            IsReady = true;
        }

        private GameObject CreatePooledItem()
        {
            GameObject poolGo = Instantiate(poolGoDic[objectName]);
            poolGo.GetComponent<PoolAble>().Pool = ojbectPoolDic[objectName];
            return poolGo;
        }
        // 사용
        private void OnTakeFromPool(GameObject poolGo)
        {
            poolGo.SetActive(true);
        }

        // 반환
        private void OnReturnedToPool(GameObject poolGo)
        {
            poolGo.transform.parent = null;
            poolGo.SetActive(false);
        }

        // 삭제
        private void OnDestroyPoolObject(GameObject poolGo)
        {
            Destroy(poolGo);
        }

        public GameObject GetEffect(string goName)
        {
            objectName = goName;

            if (poolGoDic.ContainsKey(goName) == false)
            {
                Debug.LogFormat("{0} 오브젝트풀에 등록되지 않은 오브젝트입니다.", goName);
                return null;
            }

            return ojbectPoolDic[goName].Get();
        }
    }
}
