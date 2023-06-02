using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] poolable poolablePrefabs;

        [SerializeField] int poolsize;
        [SerializeField] int maxsize;   //최대 보관;

        private Stack<poolable> objectpool = new Stack<poolable>();         // 보관소
                                                                            // 큐로도 만들수있지만 순환구조라서 스택을 사용한다.
                                                                            // 리스트로도 만들수잇지만 실수할수 있기때문에 스택사용

        private void Awake()
        {
            CreatePool();
        }
        private void CreatePool()
        {
            for (int i = 0; i < poolsize; i++)
            {
                poolable poolable = Instantiate(poolablePrefabs);
                poolable.gameObject.SetActive(false);
                poolable.transform.SetParent(transform); // 그렇게 필요한건 아니지만 관리및 가시성이 좋게하기위해 폴더링한다.
                                                         //위에는 뺴도 상관없는 부분이긴하다.
                poolable.Pool = this;
                objectpool.Push(poolable);
            }
        }


        public poolable Get()
        {
            if (objectpool.Count > 0)           // 대여 
            {
                poolable poolable = objectpool.Pop();
                poolable.gameObject.SetActive(true);
                poolable.transform.parent = null;
                return poolable;
            }
            else                         // 대여하는곳에 없으면 새로 만든다.
            {
                poolable poolable = Instantiate(poolablePrefabs);
                poolable.Pool = this;
                return poolable;
            }
        }

        public void Release(poolable poolable)              // 반납
        {
            if (objectpool.Count < maxsize)
            {


                poolable.gameObject.SetActive(false);
                poolable.transform.SetParent(transform);
                objectpool.Push(poolable);
            }
            else
            {
                Destroy(poolable.gameObject);
            }
        }
    }

}
