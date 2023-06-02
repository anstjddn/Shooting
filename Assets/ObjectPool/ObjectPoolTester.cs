using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTester : MonoBehaviour
{
    private ObjectPooling.ObjectPool ObjectPool;

    private void Awake()
    {
        ObjectPool = GetComponent<ObjectPooling.ObjectPool>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            poolable poolable = ObjectPool.Get();
            poolable.transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        }
    }
}
