using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolable : MonoBehaviour
{           // ºô·ÁÁö´Â¾Ö
    [SerializeField] float releaseTime;

    private ObjectPool pool;
    public ObjectPool Pool { get { return pool; } set { pool = value; } }


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
