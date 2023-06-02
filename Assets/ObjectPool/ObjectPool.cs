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
        [SerializeField] int maxsize;   //�ִ� ����;

        private Stack<poolable> objectpool = new Stack<poolable>();         // ������
                                                                            // ť�ε� ����������� ��ȯ������ ������ ����Ѵ�.
                                                                            // ����Ʈ�ε� ����������� �Ǽ��Ҽ� �ֱ⶧���� ���û��

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
                poolable.transform.SetParent(transform); // �׷��� �ʿ��Ѱ� �ƴ����� ������ ���ü��� �����ϱ����� �������Ѵ�.
                                                         //������ ���� ������� �κ��̱��ϴ�.
                poolable.Pool = this;
                objectpool.Push(poolable);
            }
        }


        public poolable Get()
        {
            if (objectpool.Count > 0)           // �뿩 
            {
                poolable poolable = objectpool.Pop();
                poolable.gameObject.SetActive(true);
                poolable.transform.parent = null;
                return poolable;
            }
            else                         // �뿩�ϴ°��� ������ ���� �����.
            {
                poolable poolable = Instantiate(poolablePrefabs);
                poolable.Pool = this;
                return poolable;
            }
        }

        public void Release(poolable poolable)              // �ݳ�
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
