using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.UI.Image;


public class PoolManager : MonoBehaviour
{
    Dictionary<string, ObjectPool<GameObject>> pooldic;

    private void Awake()
    {
        pooldic = new Dictionary<string, ObjectPool<GameObject>>();
    }

    public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        if(original is GameObject)
        {
            GameObject prefab = original as GameObject;
            if (!pooldic.ContainsKey(prefab.name))
                CreatePool(prefab.name, prefab);

            ObjectPool<GameObject> pool = pooldic[prefab.name];
            GameObject go = pool.Get();
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go as T;
        }
        else if (original is Component)
        {
            Component component = original as Component;
            string key = component.gameObject.name;
            if (!pooldic.ContainsKey(key))
                CreatePool(key, component.gameObject);

            GameObject go = pooldic[key].Get();
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go.GetComponent<T>();

        }
        else
        {
            return null;
        }
    }
    /* public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
     {
         if (!pooldic.ContainsKey(prefab.name))
             CreatePool(prefab.name, prefab);

         ObjectPool<GameObject> pool = pooldic[prefab.name];
         GameObject go = pool.Get();
         go.transform.position = position;
         go.transform.rotation = rotation;
         return go;
     }*/
    public T Get<T>(T original) where T : Object
    {

        return Get<T>(original, Vector3.zero, Quaternion.identity);
    }

    public bool IsContain<T>(T origianl) where T : Object
    {
        if (origianl is GameObject)
        {
            GameObject prefab = origianl as GameObject;
            string key = prefab.name;

            if (pooldic.ContainsKey(key))
                return true;
            else
                return false;

        }
        else if (origianl is Component)
        {
            Component component = origianl as Component;
            string key = component.gameObject.name;

            if (pooldic.ContainsKey(key))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }
    public bool Release(GameObject go)     // Ǯ�Ŵ��� release
    {
        if (!pooldic.ContainsKey(go.name))
            return false;
        ObjectPool<GameObject> pool = pooldic[go.name];
        pool.Release(go);             // ����Ƽ ��ü���
        return true;
    }
    public void CreatePool(string key, GameObject prefab)
    {                                       // d������Ʈ Ǯ ���鶧 4���� �Լ� �ʿ���
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () =>                           // ����
            {
                GameObject go = Instantiate(prefab);
                go.name = key;
                return go;
            },
            actionOnGet: (GameObject go) =>             //�뿩
            {
                go.SetActive(true);
                go.transform.SetParent(null);
            },
            actionOnRelease: (GameObject go) =>           //�ݳ�
            {
                go.SetActive(false);
                go.transform.SetParent(transform);
            },
            actionOnDestroy: (GameObject go) =>             // ����
            {
                Destroy(go);
            }
            );

        pooldic.Add(key, pool);


    }
}
