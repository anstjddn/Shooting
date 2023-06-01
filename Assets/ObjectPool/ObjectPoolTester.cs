using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTester : MonoBehaviour
{
    private ObjectPool ObjectPool;

    private void Awake()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            poolable poolable = ObjectPool.Get();
            poolable.transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        }
    }
}
