using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    Dictionary<string, Object> resources = new Dictionary<string, Object>();


    public T Load<T>(string path) where T : Object
    {
        string key = $"{typeof(T)}.{path}";
        if (resources.ContainsKey(key))
            return resources[key] as T;

        T resource = Resources.Load<T>(path);
        resources.Add(key, resource);
        return resource;

    }
    public T Instantiate<T>(T original, Vector3 position, Quaternion rotation,bool pooling = false) where T : Object
    {
        if (pooling)
            return GameManager.Pool.Get(original, position, rotation);
        else
            return Object.Instantiate(original, position, rotation);
    }
    public void Destory(GameObject go)
    {
        if (GameManager.Pool.Release(go))
            return;

        GameManager.Destroy(go);
    }
}
