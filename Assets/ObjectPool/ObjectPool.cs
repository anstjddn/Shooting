using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] poolable poolablePrefabs;

    [SerializeField] int poolsize;
    [SerializeField] int maxsize;   //최대 보관;

    private Stack<poolable> objectpool = new Stack<poolable>();

    private void Awake()
    {
        CreatePool();
    }
    private void CreatePool()
    {
       for(int i =0; i<poolsize; i++)
        {
            poolable poolable = Instantiate(poolablePrefabs);
            poolable.gameObject.SetActive(false);
            poolable.transform.SetParent(transform);
            poolable.Pool = this;
            objectpool.Push(poolable);
        }   
    }


    public poolable Get()
    {
        if (objectpool.Count > 0)
        {
            poolable poolable = objectpool.Pop();
            poolable.gameObject.SetActive(true);
            poolable.transform.parent = null;
            return poolable;
        }
        else
        {
            poolable poolable = Instantiate(poolablePrefabs);
            poolable.Pool = this;
            return poolable;
        }
    }

    public void Release(poolable poolable)
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
