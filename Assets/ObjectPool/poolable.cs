using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolable : MonoBehaviour
{           // �������¾�
    [SerializeField] float releaseTime;

    private ObjectPooling.ObjectPool pool;
    public ObjectPooling.ObjectPool Pool { get { return pool; } set { pool = value; } }


    private void OnEnable()
    {
        StartCoroutine(ReleaseRouine());
    }


    IEnumerator ReleaseRouine()
    {
        yield return new WaitForSeconds(releaseTime);
        pool.Release(this);
    }
}
